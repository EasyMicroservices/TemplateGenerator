using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class FormItemEventController : SimpleQueryServiceController<FormItemEventEntity, FormItemEventContract, FormItemEventContract, FormItemEventContract, long>
{
    public FormItemEventController(IUnitOfWork uow) : base(uow)
    {
    }

    protected override Func<IQueryable<FormItemEventEntity>, IQueryable<FormItemEventEntity>> OnGetAllQuery()
    {
        return q => q
        .Include(x => x.FormItem)
        .Include(x => x.FormItemEventActions)
        .ThenInclude(x => x.Action);
    }

    protected override Func<IQueryable<FormItemEventEntity>, IQueryable<FormItemEventEntity>> OnGetQuery()
    {
        return OnGetAllQuery();
    }
}