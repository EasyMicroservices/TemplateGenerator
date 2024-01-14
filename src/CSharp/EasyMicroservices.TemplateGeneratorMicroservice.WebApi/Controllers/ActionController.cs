using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class ActionController : SimpleQueryServiceController<ActionEntity, ActionContract, ActionContract, ActionContract, long>
{
    public ActionController(IUnitOfWork uow) : base(uow)
    {
    }
}
