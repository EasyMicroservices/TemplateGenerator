using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormController : SimpleQueryServiceController<FormEntity, long, CreateFormRequestContract, FormContract, FormContract>
    {
        public FormController(IContractLogic<FormEntity, CreateFormRequestContract, FormContract, FormContract, long> contractReadable) : base(contractReadable)
        {

        }

        protected override Func<IQueryable<FormEntity>, IQueryable<FormEntity>> OnGetQuery()
        {
            return query => query.Include(e => e.FormItems);
        }
    }
}
