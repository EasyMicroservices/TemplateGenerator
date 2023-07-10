using EasyMicroservices.Configuration.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Interfaces
{
    public interface IDependencyManager
    {
        IDatabase GetDatabase();
        IMapperProvider GetMapper();
        IConfigProvider GetConfigProvider();
        IUniqueIdentityManager GetUniqueIdentityManager();
    }
}
