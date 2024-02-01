using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Helpers;

public class AppUnitOfWork : UnitOfWork
{
    public AppUnitOfWork(IServiceProvider service) : base(service)
    {
    }

    public FormItemLogic GetFormItemLogic()
    {
        return ServiceProvider.GetService<FormItemLogic>();
    }
}
