using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormController : SimpleQueryServiceController<FormEntity, CreateFormRequestContract, FormContract, FormContract, long>
    {
        public FormController(IContractLogic<FormEntity, CreateFormRequestContract, FormContract, FormContract, long> contractReadable) : base(contractReadable)
        {

        }

        protected override Func<IQueryable<FormEntity>, IQueryable<FormEntity>> OnGetQuery()
        {
            return query => query
            .Include(e => e.FormItems).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.Children)
            .Include(x => x.FormItems).ThenInclude(x => x.Children).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.Children).ThenInclude(x => x.ItemType);
        }
    }
}
