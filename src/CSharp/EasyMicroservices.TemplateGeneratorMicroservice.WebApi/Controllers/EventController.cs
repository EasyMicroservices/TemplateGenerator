using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class EventController : SimpleQueryServiceController<EventEntity, EventContract, EventContract, EventContract, long>
{
    public EventController(IUnitOfWork uow) : base(uow)
    {
    }
}