using EasyMicroservices.Cores.Database.Managers;
using EasyMicroservices.TemplateGeneratorMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice
{
    public class WhiteLabelManager
    {
        public WhiteLabelManager(IServiceProvider serviceProvider, IDependencyManager dependencyManager)
        {
            _serviceProvider = serviceProvider;
            _dependencyManager = dependencyManager;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly IDependencyManager _dependencyManager;
        public static HttpClient HttpClient { get; set; } = new HttpClient();
        public async Task Initialize(string microserviceName, string whiteLableRoute, params Type[] dbContextTypes)
        {
            if (dbContextTypes.IsNullOrEmpty())
                return;
            var uniqueIdentityManager = _dependencyManager.GetUniqueIdentityManager() as DefaultUniqueIdentityManager;

            var microserviceClient = new WhiteLables.GeneratedServices.MicroserviceClient(whiteLableRoute, HttpClient);
            var microservices = await microserviceClient.GetAllAsync();
            var foundMicroservice = microservices.Result.FirstOrDefault(x => x.Name.Equals(microserviceName, StringComparison.OrdinalIgnoreCase));
            if (foundMicroservice == null)
            {
                foundMicroservice = new WhiteLables.GeneratedServices.MicroserviceContract()
                {
                    InstanceIndex = 1,
                    Name = microserviceName,
                    Description = "Automatically added"
                };
                var addMicroservice = await microserviceClient.AddAsync(foundMicroservice);
                foundMicroservice.Id = addMicroservice.Result;
            }
            var microserviceContextTableClient = new WhiteLables.GeneratedServices.MicroserviceContextTableClient(whiteLableRoute, HttpClient);
            var microserviceContextTables = await microserviceContextTableClient.GetAllAsync();

            HashSet<string> addedInWhitLabels = new HashSet<string>();
            foreach (var contextTableContract in microserviceContextTables.Result)
            {
                uniqueIdentityManager.InitializeTables(contextTableContract.MicroserviceId, contextTableContract.ContextName, contextTableContract.TableName, contextTableContract.ContextTableId);
                addedInWhitLabels.Add(uniqueIdentityManager.GetTableName(contextTableContract.MicroserviceId, contextTableContract.ContextName, contextTableContract.TableName));
            }

            foreach (var contextType in dbContextTypes)
            {
                var contextTableClient = new WhiteLables.GeneratedServices.ContextTableClient(whiteLableRoute, HttpClient);
                var contextTables = await contextTableClient.GetAllAsync();
                var insctanceOfContext = _serviceProvider.GetService(contextType) as DbContext;
                string contextName = contextType.Name;
                foreach (var entityType in insctanceOfContext.Model.GetEntityTypes())
                {
                    string tableName = entityType.BaseType.ClrType.Name;
                    var tableFullName = uniqueIdentityManager.GetTableName(foundMicroservice.Id, contextType.Name, tableName);
                    if (addedInWhitLabels.Contains(tableFullName))
                    {
                        if (microserviceContextTables.Result.Any(x => x.ContextName == contextName && x.TableName == tableName && x.MicroserviceId == foundMicroservice.Id))
                            continue;
                        var contextTable = contextTables.Result.FirstOrDefault(x => x.ContextName == contextName && x.TableName == tableName);
                        if (contextTable == null)
                        {
                            contextTable = new WhiteLables.GeneratedServices.ContextTableContract()
                            {
                                ContextName = contextName,
                                TableName = tableName,
                            };
                            var contextTableResult = await contextTableClient.AddAsync(contextTable);
                            contextTable.Id = contextTableResult.Result;
                        }
                        var addedMicroserviceContextTable = await microserviceContextTableClient.AddAsync(new WhiteLables.GeneratedServices.MicroserviceContextTableContract()
                        {
                            ContextName = contextName,
                            TableName = tableName,
                            MicroserviceName = microserviceName,
                            MicroserviceId = foundMicroservice.Id,
                            ContextTableId = contextTable.Id
                        });
                        uniqueIdentityManager.InitializeTables(foundMicroservice.Id, contextName, tableName, contextTable.Id);
                    }
                }
            }
        }
    }
}
