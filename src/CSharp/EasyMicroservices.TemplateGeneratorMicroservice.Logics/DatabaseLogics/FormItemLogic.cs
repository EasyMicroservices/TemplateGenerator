using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
public static class FormItemLogic
{
    public static async Task CheckDeletedItems(IBaseUnitOfWork unitOfWork, FormContract request, CancellationToken cancellationToken = default)
    {
        var formReadable = unitOfWork.GetReadableOf<FormItemEntity>();
        var getItemResult = await formReadable
            .Where(x => x.FormId == request.Id)
            .ToListAsync(cancellationToken);
        await LoadAllFormItems(formReadable, getItemResult, new HashSet<long>());
        var mapped = unitOfWork.GetMapper().MapToList<FormItemContract>(getItemResult);
        await CheckDeletedItems<FormItemContract, FormItemEntity>(unitOfWork, request.Items, mapped, x => x.Id, x => x.Items);
    }

    public static async Task CheckDeletedItems(IBaseUnitOfWork unitOfWork, FormItemContract request, CancellationToken cancellationToken = default)
    {
        var formItemReadable = unitOfWork.GetReadableOf<FormItemEntity>();
        var getItemResult = await formItemReadable.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await LoadAllFormItems(formItemReadable, getItemResult, new HashSet<long>());
        var mapped = unitOfWork.GetMapper().Map<FormItemContract>(getItemResult);
        await CheckDeletedItems<FormItemContract, FormItemEntity>(unitOfWork, request.Items, mapped.Items, x => x.Id, x => x.Items);
        await CheckDeletedItems<FormItemEventContract, FormItemEventEntity>(unitOfWork, request.Events, mapped.Events, x => x.Id, null);
        if (request.Events != null)
        {
            foreach (var item in request.Events)
            {
                var findEvent = mapped.Events.FirstOrDefault(x => x.Id == item.Id);
                if (findEvent == null)
                    continue;
                await CheckDeletedItems<FormItemEventActionContract, FormItemEventActionEntity>(unitOfWork, item.FormItemEventActions, findEvent.FormItemEventActions, x => x.Id, x => x.Children);
            }
        }
    }

    public static async Task CheckDeletedItems<TContract, TEntity>(IBaseUnitOfWork unitOfWork, ICollection<TContract> changedItems, ICollection<TContract> oldItems, Func<TContract, long> getId, Func<TContract, ICollection<TContract>> getChildren)
        where TEntity : class
    {
        var deletedItems = FindDeletedItems(oldItems, changedItems, x => getId(x), getChildren == null ? null : x => getChildren(x));
        if (deletedItems.Count > 0)
        {
            await unitOfWork.GetLogic<TEntity, long>().SoftDeleteBulkByIds(new Cores.Contracts.Requests.SoftDeleteBulkRequestContract<long>
            {
                Ids = deletedItems,
                IsDelete = true
            }).AsCheckedResult();
        }
    }

    public static List<long> FindDeletedItems<TContract>(ICollection<TContract> realItems, ICollection<TContract> newItems,
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

    public static async Task LoadAllFormItems(IEasyReadableQueryableAsync<FormItemEntity> readable, List<FormItemEntity> formItems, HashSet<long> loadedItemsCache)
    {
        foreach (var item in formItems.Where(x => !x.IsDeleted))
        {
            await LoadAllFormItems(readable, item, loadedItemsCache);
        }
    }

    public static async Task LoadAllFormItems(IEasyReadableQueryableAsync<FormItemEntity> readable, FormItemEntity formItem, HashSet<long> loadedItemsCache)
    {
        if (loadedItemsCache.Contains(formItem.Id))
            return;
        loadedItemsCache.Add(formItem.Id);
        await readable.Context.Entry(formItem).ReloadCollectionAsync(nameof(formItem.Children));
        await readable.Context.Entry(formItem).ReloadCollectionAsync(nameof(formItem.FormItemEvents));
        await readable.Context.Entry(formItem).ReloadReferenceAsync(nameof(formItem.ItemType));
        await readable.Context.Entry(formItem).ReloadReferenceAsync(nameof(formItem.PrimaryFormItem));
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
                await readable.Context.Entry(formItem.PrimaryFormItem).ReloadReferenceAsync(nameof(formItem.ItemType));
                await LoadAllFormItems(readable, formItem.PrimaryFormItem, loadedItemsCache);
            }
        }
        formItem.FormItemEvents = formItem.FormItemEvents.Where(x => !x.IsDeleted).ToList();
        foreach (var item in formItem.FormItemEvents)
        {
            await readable.Context.Entry(item).ReloadCollectionAsync(nameof(item.FormItemEventActions));
            item.FormItemEventActions = item.FormItemEventActions.Where(x => !x.IsDeleted).ToList();
            foreach (var child in item.FormItemEventActions)
            {
                await readable.Context.Entry(child).ReloadCollectionAsync(nameof(child.Children));
                child.Children = child.Children.Where(x => !x.IsDeleted).ToList();
            }
        }
        await LoadAllFormItems(readable, formItem.Children.ToList(), loadedItemsCache);
    }

    public static async Task LoadAllEventActions(IEasyReadableQueryableAsync<FormItemEventActionEntity> readable, ICollection<FormItemEventActionEntity> eventItems, HashSet<long> loadedItemsCache)
    {
        foreach (var item in eventItems.Where(x => !x.IsDeleted))
        {
            if (loadedItemsCache.Contains(item.Id))
                continue;
            loadedItemsCache.Add(item.Id);
            await readable.Context.Entry(item).ReloadCollectionAsync(nameof(item.Children));
            await LoadAllEventActions(readable, item.Children, loadedItemsCache);
        }
    }

    public static async Task LoadAllEvents(IEasyReadableQueryableAsync<FormItemEventEntity> readable, IEasyReadableQueryableAsync<FormItemEventActionEntity> eventReadable, FormItemEventEntity eventItem, HashSet<long> loadedItemsCache)
    {
        if (loadedItemsCache.Contains(eventItem.Id))
            return;
        loadedItemsCache.Add(eventItem.Id);
        await readable.Context.Entry(eventItem).ReloadCollectionAsync(nameof(eventItem.FormItemEventActions));
        eventItem.FormItemEventActions = eventItem.FormItemEventActions.Where(x => !x.IsDeleted).ToList();
        foreach (var item in eventItem.FormItemEventActions)
        {
            await readable.Context.Entry(item).ReloadCollectionAsync(nameof(item.Children));
            await LoadAllEventActions(eventReadable, item.Children, loadedItemsCache);
        }
    }
}
