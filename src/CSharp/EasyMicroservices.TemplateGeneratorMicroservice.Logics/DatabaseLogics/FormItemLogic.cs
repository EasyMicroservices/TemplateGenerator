using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
public class FormItemLogic
{
    //readonly IEasyReadableQueryableAsync<FormItemEventActionEntity> _formItemEventActionReadable;
    //readonly IEasyReadableQueryableAsync<FormItemEventEntity> _formItemEventReadable;
    readonly IEasyReadableQueryableAsync<FormItemEntity> _formItemReadable;
    readonly IEasyReadableQueryableAsync<FormEntity> _formReadable;
    readonly IUnitOfWork _unitOfWork;
    public FormItemLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        //_formItemEventActionReadable = unitOfWork.GetReadableOf<FormItemEventActionEntity>();
        //_formItemEventReadable = unitOfWork.GetReadableOf<FormItemEventEntity>();
        _formItemReadable = unitOfWork.GetReadableOf<FormItemEntity>();
        _formReadable = unitOfWork.GetReadableOf<FormEntity>();
    }

    public async Task<MessageContract<FormContract>> GetFormById(GetByIdRequestContract<long> request, CancellationToken cancellationToken = default)
    {
        await using var context = _unitOfWork.GetDatabase();
        var uniqueIdentity = await _unitOfWork.GetCurrentUserUniqueIdentity();
        uniqueIdentity += "-";
        var readable = context.GetReadableOf<FormEntity>();
        var query = readable.Where(e => e.Id == request.Id && !e.IsDeleted && e.UniqueIdentity.StartsWith(uniqueIdentity));
        var result = await query.Include(x => x.FormItems.Where(x => !x.IsDeleted)).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        List<FormItemEntity> itemEntities = new List<FormItemEntity>();
        foreach (var fi in result.FormItems)
        {
            itemEntities.Add(await LoadAllFormItems(fi.Id, uniqueIdentity, cancellationToken));
        }
        result.FormItems = itemEntities;
        return await _unitOfWork.GetMapper().MapAsync<FormContract>(result);
    }

    public async Task<ListMessageContract<FormContract>> GetWithInclude(FilterRequestContract filterRequest, CancellationToken cancellationToken)
    {
        var uniqueIdentity = await _unitOfWork.GetCurrentUserUniqueIdentity();
        uniqueIdentity += "-";
        var readable = _formReadable;
        var query = readable.Where(e => !e.IsDeleted && e.UniqueIdentity.StartsWith(uniqueIdentity));
        var count = await query.LongCountAsync();
        if (filterRequest != null)
        {
            if (filterRequest.Index.HasValue)
                query = query.Skip((int)filterRequest.Index.Value);
            if (filterRequest.Length.HasValue)
                query = query.Take((int)filterRequest.Length.Value);
        }
        var result = await query.Include(x => x.FormItems.Where(i => !i.IsDeleted)).AsNoTracking().ToListAsync(cancellationToken);
        foreach (var item in result)
        {
            List<FormItemEntity> itemEntities = new List<FormItemEntity>();
            foreach (var fi in item.FormItems)
            {
                itemEntities.Add(await LoadAllFormItems(fi.Id, uniqueIdentity, cancellationToken));
            }
            item.FormItems = itemEntities;
        }
        return new ListMessageContract<FormContract>()
        {
            TotalCount = count,
            IsSuccess = true,
            Result = await _unitOfWork.GetMapper().MapToListAsync<FormContract>(result)
        };
    }

    public async Task CheckDeletedItems(FormContract request, CancellationToken cancellationToken = default)
    {
        var getItemResult = await _formItemReadable
            .Where(x => x.FormId == request.Id)
            .ToListAsync(cancellationToken);
        await LoadAllFormItems(getItemResult, new HashSet<long>());
        var mapped = _unitOfWork.GetMapper().MapToList<FormItemContract>(getItemResult);
        await CheckDeletedItems<FormItemContract, FormItemEntity>(request.Items, mapped, x => x.Id, x => x.Items);
    }

    public async Task CheckDeletedItems(FormItemContract request, CancellationToken cancellationToken = default)
    {
        if (request.Id == 0)
            return;
        var getItemResult = await _formItemReadable.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await LoadAllFormItems(getItemResult, new HashSet<long>());
        var mapped = _unitOfWork.GetMapper().Map<FormItemContract>(getItemResult);
        await CheckDeletedItems<FormItemContract, FormItemEntity>(request.Items, mapped.Items, x => x.Id, x => x.Items);
        await CheckDeletedItems<FormItemEventContract, FormItemEventEntity>(request.Events, mapped.Events, x => x.Id, null);
        if (request.Events != null)
        {
            foreach (var item in request.Events)
            {
                var findEvent = mapped.Events.FirstOrDefault(x => x.Id == item.Id);
                if (findEvent == null)
                    continue;
                await CheckDeletedItems<FormItemEventActionContract, FormItemEventActionEntity>(item.FormItemEventActions, findEvent.FormItemEventActions, x => x.Id, x => x.Children);
            }
        }

        if (request.Items != null)
        {
            foreach (var item in request.Items)
            {
                await CheckDeletedItems(item, cancellationToken);
            }
        }
    }

    public async Task CheckDeletedItems<TContract, TEntity>(ICollection<TContract> changedItems, ICollection<TContract> oldItems, Func<TContract, long> getId, Func<TContract, ICollection<TContract>> getChildren)
        where TEntity : class
    {
        var deletedItems = FindDeletedItems(oldItems, changedItems, x => getId(x), getChildren == null ? null : x => getChildren(x));
        if (deletedItems.Count > 0)
        {
            await _unitOfWork.GetLogic<TEntity, long>().SoftDeleteBulkByIds(new Cores.Contracts.Requests.SoftDeleteBulkRequestContract<long>
            {
                Ids = deletedItems,
                IsDelete = true
            }).AsCheckedResult();
        }
    }

    public List<long> FindDeletedItems<TContract>(ICollection<TContract> realItems, ICollection<TContract> newItems,
        Func<TContract, long> getId, Func<TContract, ICollection<TContract>> getChildren)
    {
        List<long> result = new List<long>();
        if (realItems == null || newItems == null)
            return result;
        foreach (var item in realItems)
        {
            var findItem = newItems.FirstOrDefault(x => getId(x) == getId(item));
            if (findItem == null)
                result.Add(getId(item));
            else if (getChildren != null)
                result.AddRange(FindDeletedItems(getChildren(item), getChildren(findItem), getId, getChildren));
        }
        return result;
    }

    public async Task LoadAllFormItems(List<FormItemEntity> formItems, HashSet<long> loadedItemsCache)
    {
        foreach (var item in formItems.Where(x => !x.IsDeleted))
        {
            await LoadAllFormItems(item, loadedItemsCache);
        }
    }

    public async Task<FormItemEntity> LoadAllFormItems(long formItemId, string uniqueIdentity, CancellationToken cancellationToken)
    {
        var query = _formItemReadable.Where(e => e.Id == formItemId && !e.IsDeleted);
        if (uniqueIdentity.HasValue())
            query = query.Where(e => e.UniqueIdentity.StartsWith(uniqueIdentity));
        var result = await query.FirstAsync(cancellationToken);
        await LoadAllFormItems(result, new HashSet<long>());
        return result;
    }

    public async Task LoadAllFormItems(FormItemEntity formItem, HashSet<long> loadedItemsCache)
    {
        if (loadedItemsCache.Contains(formItem.Id))
            return;
        loadedItemsCache.Add(formItem.Id);
        await _formItemReadable.Context.Entry(formItem).ReloadCollectionAsync(nameof(formItem.Children));
        await _formItemReadable.Context.Entry(formItem).ReloadCollectionAsync(nameof(formItem.FormItemEvents));
        await _formItemReadable.Context.Entry(formItem).ReloadReferenceAsync(nameof(formItem.ItemType));
        await _formItemReadable.Context.Entry(formItem).ReloadReferenceAsync(nameof(formItem.PrimaryFormItem));
        formItem.Children = formItem.Children.Where(x => !x.IsDeleted).ToList();
        if (formItem.PrimaryFormItem != null)
        {
            if (formItem.PrimaryFormItem.IsDeleted)
            {
                formItem.PrimaryFormItem = null;
                formItem.PrimaryFormItemId = null;
            }
            else
            {
                await _formItemReadable.Context.Entry(formItem.PrimaryFormItem).ReloadReferenceAsync(nameof(formItem.ItemType));
                await LoadAllFormItems(formItem.PrimaryFormItem, loadedItemsCache);
            }
        }
        formItem.FormItemEvents = formItem.FormItemEvents.Where(x => !x.IsDeleted).ToList();
        foreach (var item in formItem.FormItemEvents)
        {
            await LoadAllEvents(item, new HashSet<long>());
        }
        await LoadAllFormItems(formItem.Children.ToList(), loadedItemsCache);
    }

    public async Task LoadAllEventActions(ICollection<FormItemEventActionEntity> eventItems, HashSet<long> loadedItemsCache)
    {
        foreach (var item in eventItems)
        {
            if (loadedItemsCache.Contains(item.Id))
                continue;
            loadedItemsCache.Add(item.Id);
            await _formItemReadable.Context.Entry(item).ReloadReferenceAsync(nameof(item.Action));
            await _formItemReadable.Context.Entry(item).ReloadCollectionAsync(nameof(item.Children));
            item.Children = item.Children.Where(x => !x.IsDeleted).ToList();
            await LoadAllEventActions(item.Children, loadedItemsCache);
        }
    }

    public async Task LoadAllEvents(FormItemEventEntity eventItem, HashSet<long> loadedItemsCache)
    {
        if (loadedItemsCache.Contains(eventItem.Id))
            return;
        loadedItemsCache.Add(eventItem.Id);
        await _formItemReadable.Context.Entry(eventItem).ReloadReferenceAsync(nameof(eventItem.Event));
        await _formItemReadable.Context.Entry(eventItem).ReloadCollectionAsync(nameof(eventItem.FormItemEventActions));
        eventItem.FormItemEventActions = eventItem.FormItemEventActions.Where(x => !x.IsDeleted).ToList();
        foreach (var item in eventItem.FormItemEventActions)
        {
            await _formItemReadable.Context.Entry(item).ReloadReferenceAsync(nameof(item.Action));
            await _formItemReadable.Context.Entry(item).ReloadCollectionAsync(nameof(item.Children));
            item.Children = item.Children.Where(x => !x.IsDeleted).ToList();
            await LoadAllEventActions(item.Children, new HashSet<long>());
        }
    }

    public void ClearInnerItems(UpdateFormItemRequestContract request)
    {
        ClearEvents(request.Events);
        ClearInnerItems(request.Items);
    }

    public void ClearInnerItems(List<FormItemContract> items)
    {
        if (items == null)
            return;
        foreach (var item in items)
        {
            ClearEvents(item.Events);
            ClearInnerItems(item.Items);
        }
    }

    public void ClearEvents(List<FormItemEventContract> events)
    {
        if (events == null)
            return;
        foreach (var item in events)
        {
            item.Event = null;
            ClearEventActions(item.FormItemEventActions);
        }
    }

    public void ClearEventActions(List<FormItemEventActionContract> actions)
    {
        if (actions == null)
            return;
        foreach (var item in actions)
        {
            item.Action = null;
            ClearEventActions(item.Children);
        }
    }
}
