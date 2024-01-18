using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class NoParentFormItemController : SimpleQueryServiceController<FormItemEntity, CreateFormItemRequestContract, UpdateFormItemRequestContract, FormItemContract, long>
{
    AppUnitOfWork _appUnitOfWork;
    public NoParentFormItemController(AppUnitOfWork uow) : base(uow)
    {
        _appUnitOfWork = uow;
    }

    public override async Task<MessageContract<FormItemContract>> GetById(GetByIdRequestContract<long> request, CancellationToken cancellationToken = default)
    {
        var uniqueIdentity = await UnitOfWork.GetCurrentUserUniqueIdentity();
        if (uniqueIdentity.HasValue())
            uniqueIdentity += "-";
        var result = await _appUnitOfWork.GetFormItemLogic().LoadAllFormItems(request.Id, uniqueIdentity, cancellationToken);
        return await UnitOfWork.GetMapper().MapAsync<FormItemContract>(result);
    }

    public override async Task<ListMessageContract<FormItemContract>> Filter(FilterRequestContract filterRequest, CancellationToken cancellationToken = default)
    {
        var uniqueIdentity = await UnitOfWork.GetCurrentUserUniqueIdentity();
        if (uniqueIdentity.HasValue())
            uniqueIdentity += "-";
        await using var context = UnitOfWork.GetDatabase();
        var readable = context.GetReadableOf<FormItemEntity>();
        var query = readable.Where(e => e.FormId == null && e.ParentFormItemId == null && !e.IsDeleted);
        if (uniqueIdentity.HasValue())
            query = query.Where(e => e.UniqueIdentity.StartsWith(uniqueIdentity));
        var count = await query.LongCountAsync();
        filterRequest.IsDeleted = false;
        if (filterRequest.Index.HasValue)
            query = query.Skip((int)filterRequest.Index.Value);
        if (filterRequest.Length.HasValue)
            query = query.Take((int)filterRequest.Length.Value);
        var result = await query.ToListAsync(cancellationToken);
        return new ListMessageContract<FormItemContract>()
        {
            TotalCount = count,
            IsSuccess = true,
            Result = await UnitOfWork.GetMapper().MapToListAsync<FormItemContract>(result)
        };
    }

    public override async Task<MessageContract<FormItemContract>> UpdateChangedValuesOnly(UpdateFormItemRequestContract request, CancellationToken cancellationToken = default)
    {
        var logic = _appUnitOfWork.GetFormItemLogic();
        await logic.CheckDeletedItems(request, cancellationToken);
        logic.ClearInnerItems(request);
        return await base.UpdateChangedValuesOnly(request, cancellationToken);
    }
}