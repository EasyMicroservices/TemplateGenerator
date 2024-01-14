using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
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
        await LoadAll(formReadable, getItemResult, new HashSet<long>());
        var mapped = unitOfWork.GetMapper().MapToList<FormItemContract>(getItemResult);
        var allItems = GetAllChilren(getItemResult);
        await CheckDeletedItems(unitOfWork, request.Items, mapped, allItems, cancellationToken);
    }

    public static async Task CheckDeletedItems(IBaseUnitOfWork unitOfWork, FormItemContract request, CancellationToken cancellationToken = default)
    {
        var formItemReadable = unitOfWork.GetReadableOf<FormItemEntity>();
        var getItemResult = await formItemReadable.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await LoadAll(formItemReadable, getItemResult, new HashSet<long>());
        var mapped = unitOfWork.GetMapper().Map<FormItemContract>(getItemResult);
        var allItems = GetAllChilren(getItemResult.Children);
        await CheckDeletedItems(unitOfWork, request.Items, mapped.Items, allItems, cancellationToken);
    }

    public static async Task CheckDeletedItems(IBaseUnitOfWork unitOfWork, List<FormItemContract> changedItems, List<FormItemContract> oldItems, List<FormItemEntity> allItems, CancellationToken cancellationToken = default)
    {
        //var logic = unitOfWork.GetLongLogic<FormItemEntity>();
        var deletedItems = FindDeletedItems(oldItems, changedItems);
        if (deletedItems.Count > 0)
        {
            foreach (var item in deletedItems)
            {
                await unitOfWork.GetLogic<FormItemEntity, long>().SoftDeleteById(new Cores.Contracts.Requests.SoftDeleteRequestContract<long>()
                {
                    Id = item,
                    IsDelete = true
                }).AsCheckedResult();
            }
            //List<FormItemEntity> updatedItems = new List<FormItemEntity>();
            //foreach (var item in allItems.Where(x => deletedItems.Contains(x.PrimaryFormItemId.GetValueOrDefault())))
            //{
            //    item.PrimaryFormItemId = null;
            //    updatedItems.Add(item);
            //}
            //foreach (var item in allItems.Where(x => deletedItems.Contains(x.ParentFormItemId.GetValueOrDefault())))
            //{
            //    item.ParentFormItemId = null;
            //    updatedItems.Add(item);
            //}
            //await logic.UpdateBulk(new UpdateBulkRequestContract<FormItemEntity>()
            //{
            //    Items = updatedItems.Distinct().ToList()
            //}, cancellationToken).AsCheckedResult();
        }
    }

    public static List<FormItemContract> GetAllChilren(List<FormItemContract> items)
    {
        List<FormItemContract> result = new List<FormItemContract>();
        foreach (var item in items)
        {
            result.Add(item);
            result.AddRange(GetAllChilren(item.Items));
        }
        return result;
    }

    public static List<FormItemEntity> GetAllChilren(ICollection<FormItemEntity> items)
    {
        List<FormItemEntity> result = new List<FormItemEntity>();
        foreach (var item in items)
        {
            result.Add(item);
            if (item.Children != null)
                result.AddRange(GetAllChilren(item.Children));
        }
        return result;
    }

    public static List<long> FindDeletedItems(ICollection<FormItemContract> realItems, ICollection<FormItemContract> newItems)
    {
        List<long> result = new List<long>();
        if (realItems == null || newItems == null)
            return result;
        foreach (var item in realItems)
        {
            var findItem = newItems.FirstOrDefault(x => x.Id == item.Id);
            if (findItem == null)
                result.Add(item.Id);
            else
                result.AddRange(FindDeletedItems(item.Items, findItem.Items));
        }
        return result;
    }

    public static async Task LoadAll(IEasyReadableQueryableAsync<FormItemEntity> readable, List<FormItemEntity> formItems, HashSet<long> loadedItemsCache)
    {
        foreach (var item in formItems.Where(x => !x.IsDeleted))
        {
            await LoadAll(readable, item, loadedItemsCache);
        }
    }

    public static async Task LoadAll(IEasyReadableQueryableAsync<FormItemEntity> readable, FormItemEntity formItem, HashSet<long> loadedItemsCache)
    {
        if (loadedItemsCache.Contains(formItem.Id))
            return;
        loadedItemsCache.Add(formItem.Id);
        await readable.Context.Entry(formItem).ReloadCollectionAsync(nameof(formItem.Children));
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
                await LoadAll(readable, formItem.PrimaryFormItem, loadedItemsCache);
            }
        }
        await LoadAll(readable, formItem.Children.ToList(), loadedItemsCache);
    }
}
