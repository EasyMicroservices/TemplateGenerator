using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;
using Identity.GeneratedServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateGenerators.GeneratedServices;
using Xunit;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Tests.Fixtures;
public class TestFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; }
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        ServiceProvider = await StartAsync();
    }

    static IServiceProvider DefaultServiceProvider;
    static SemaphoreSlim Semaphore { get; set; } = new SemaphoreSlim(1);
    public static async Task<IServiceProvider> StartAsync()
    {
        try
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            await Semaphore.WaitAsync();
            if (DefaultServiceProvider != null)
                return DefaultServiceProvider;
            var app = Program.CreateBuilder(null);
            UnitOfWork.MapperTypeAssembly = typeof(ItemTypeDatabaseLogic);
            string generatorBaseUrl = config.GetSection("Urls").Get<string>().Replace("*", "localhost");
            string authBaseUrl = config.GetSection("ServiceAddresses").GetChildren().FirstOrDefault(x=>x.GetValue<string>("Name") == "Authentication").GetValue<string>("Address");
            string identityBaseUrl = config.GetSection("ServiceAddresses").GetChildren().FirstOrDefault(x=>x.GetValue<string>("Name") == "Identity").GetValue<string>("Address");
            app.Services.AddSingleton(s => new HttpClient());
            app.Services.AddTransient(s => new AuthenticationClient(authBaseUrl, s.GetService<HttpClient>()));
            app.Services.AddTransient(s => new NoParentFormItemClient(generatorBaseUrl, s.GetService<HttpClient>()));
            app.Services.AddTransient(s => new EventClient(generatorBaseUrl, s.GetService<HttpClient>()));
            app.Services.AddTransient(s => new ActionClient(generatorBaseUrl, s.GetService<HttpClient>()));
            app.Services.AddMvc().AddApplicationPart(typeof(FormController).Assembly);

            var build = await app.BuildWithUseCors<TemplateGeneratorContext>(default, true);
            build.MapControllers();
            var scope = build.Services.CreateScope();
            ApplicationManager.Instance.DependencyManager = scope.ServiceProvider.GetService<IUnitOfWork>();
            DefaultServiceProvider = app.Services.BuildServiceProvider();
            _ = build.RunAsync();
            return DefaultServiceProvider;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}