using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormDetailController : SimpleQueryServiceController<FormDetailEntity, FormDetailContract, FormDetailContract, FormDetailContract, long>
    {
        public FormDetailController(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
