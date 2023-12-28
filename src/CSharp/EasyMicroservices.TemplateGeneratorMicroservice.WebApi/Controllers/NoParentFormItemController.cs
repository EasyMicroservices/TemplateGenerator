using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;

public class NoParentFormItemController : SimpleQueryServiceController<FormItemEntity, CreateFormItemRequestContract, UpdateFormItemRequestContract, FormItemContract, long>
{
    public NoParentFormItemController(IUnitOfWork uow) : base(uow)
    {
    }

    protected override Func<IQueryable<FormItemEntity>, IQueryable<FormItemEntity>> OnGetQuery()
    {
        return query => query
        .Where(e => e.FormId == null && e.ParentFormItemId == null && !e.IsDeleted).Include(x => x.ItemType)
        .Include(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted))
        .Include(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
        .Include(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
        .Include(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
        .Include(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType);
    }

    protected override Func<IQueryable<FormItemEntity>, IQueryable<FormItemEntity>> OnGetAllQuery()
    {
        return OnGetQuery();
    }

    public override async Task<MessageContract<FormItemContract>> UpdateChangedValuesOnly(UpdateFormItemRequestContract request, CancellationToken cancellationToken = default)
    {
        await CheckDeletedItems(request, cancellationToken);
        return await base.UpdateChangedValuesOnly(request, cancellationToken);
    }

    async Task CheckDeletedItems(FormItemContract request, CancellationToken cancellationToken = default)
    {
        var getItemResult = await GetById(request.Id, cancellationToken)
            .AsCheckedResult();

        var deletedItems = FindDeletedItems(getItemResult.Items, request.Items);
        if (deletedItems.Count > 0)
        {
            foreach (var item in deletedItems)
            {
                await UnitOfWork.GetLogic<FormItemEntity, long>().SoftDeleteById(new Cores.Contracts.Requests.SoftDeleteRequestContract<long>()
                {
                    Id = item,
                    IsDelete = true
                }).AsCheckedResult();
            }
        }
    }

    List<FormItemContract> GetAllChilren(List<FormItemContract> items)
    {
        List<FormItemContract> result = new List<FormItemContract>();
        foreach (var item in items)
        {
            result.Add(item);
            result.AddRange(GetAllChilren(item.Items));
        }
        return result;
    }

    private List<long> FindDeletedItems(ICollection<FormItemContract> realItems, ICollection<FormItemContract> newItems)
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
}