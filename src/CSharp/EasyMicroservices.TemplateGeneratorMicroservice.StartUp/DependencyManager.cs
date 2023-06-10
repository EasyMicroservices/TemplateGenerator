using EasyMicroservices.Configuration.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.TemplateGeneratorMicroservice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice
{
    public class DependencyManager : IDependencyManager
    {
        public virtual IConfigProvider GetConfigProvider()
        {
            throw new NotImplementedException();
        }

        public virtual IContractLogic<TEntity, TRequestContract, TResponseContract, long> GetContractLogic<TEntity, TRequestContract, TResponseContract>()
            where TEntity : class, IIdSchema<long>
            where TResponseContract : class
        {
            return new LongIdMappedDatabaseLogicBase<TEntity, TRequestContract, TResponseContract>(GetDatabase().GetReadableOf<TEntity>(), GetDatabase().GetWritableOf<TEntity>(), GetMapper());
        }

        public virtual IDatabase GetDatabase()
        {
            return new EntityFrameworkCoreDatabaseProvider(new TemplateGeneratorContext(new DatabaseBuilder()));
        }

        public virtual IMapperProvider GetMapper()
        {
            var mapper = new CompileTimeMapperProvider();
            foreach (var type in typeof(IDependencyManager).Assembly.GetTypes())
            {
                if (typeof(IMapper).IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type, mapper);
                    var returnTypes = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.ReturnType != typeof(object) && x.Name == "Map").Select(x => x.ReturnType).ToArray();
                    mapper.AddMapper(returnTypes[0], returnTypes[1], (IMapper)instance);
                }
            }
            return mapper;
        }
    }
}
