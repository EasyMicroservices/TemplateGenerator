using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class NoParentFormItemController : SimpleQueryServiceController<FormItemEntity, CreateFormItemRequestContract, UpdateFormItemRequestContract, FormItemContract, long>
{
    public NoParentFormItemController(IUnitOfWork uow) : base(uow)
    {
    }

    public override async Task<ListMessageContract<FormItemContract>> Filter(FilterRequestContract filterRequest, CancellationToken cancellationToken = default)
    {
        await using var context = UnitOfWork.GetDatabase();
        var readable = context.GetReadableOf<FormItemEntity>();
        var query = readable.Where(e => e.FormId == null && e.ParentFormItemId == null && !e.IsDeleted);
        var count = await query.LongCountAsync();
        filterRequest.IsDeleted = false;
        if (filterRequest.Index.HasValue)
            query = query.Skip((int)filterRequest.Index.Value);
        if (filterRequest.Length.HasValue)
            query = query.Take((int)filterRequest.Length.Value);
        var result = await query.ToListAsync(cancellationToken);
        await FormItemLogic.LoadAll(readable, result);
        return new ListMessageContract<FormItemContract>()
        {
            TotalCount = count,
            IsSuccess = true,
            Result = await UnitOfWork.GetMapper().MapToListAsync<FormItemContract>(result)
        };
    }

    public override async Task<MessageContract<FormItemContract>> UpdateChangedValuesOnly(UpdateFormItemRequestContract request, CancellationToken cancellationToken = default)
    {
        await FormItemLogic.CheckDeletedItems(UnitOfWork, request, cancellationToken);
        return await base.UpdateChangedValuesOnly(request, cancellationToken);
    }
}