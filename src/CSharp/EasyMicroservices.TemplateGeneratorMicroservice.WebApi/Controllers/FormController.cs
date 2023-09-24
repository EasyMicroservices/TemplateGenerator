using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormController : SimpleQueryServiceController<FormEntity, CreateFormRequestContract, FormContract, FormContract, long>
    {
        IContractLogic<FormItemEntity, FormItemEntity, FormItemEntity, FormItemEntity, long> _formItemContractReadable;
        public FormController(IContractLogic<FormEntity, CreateFormRequestContract, FormContract, FormContract, long> contractReadable,
            IContractLogic<FormItemEntity, FormItemEntity, FormItemEntity, FormItemEntity, long> formItemContractReadable) : base(contractReadable)
        {
            _formItemContractReadable = formItemContractReadable;
        }

        protected override Func<IQueryable<FormEntity>, IQueryable<FormEntity>> OnGetQuery()
        {
            return query => query
            .Include(e => e.FormItems.Where(x=>!x.IsDeleted)).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted))
            .Include(x => x.FormItems.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType)
            .Include(x => x.FormItems.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.Children.Where(x => !x.IsDeleted)).ThenInclude(x => x.ItemType);
        }

        public override async Task<MessageContract<FormContract>> Update(FormContract request, CancellationToken cancellationToken = default)
        {
            var getItemResult = await GetById(request.Id, cancellationToken);
            if (!getItemResult)
                return getItemResult;
            var deletedItems = FindDeletedItems(getItemResult.Result.Items, request.Items);
            if (deletedItems.Count > 0)
            {
                foreach (var item in deletedItems)
                {
                    var result = await _formItemContractReadable.SoftDeleteById(new Cores.Contracts.Requests.SoftDeleteRequestContract<long>()
                    {
                        Id = item,
                        IsDelete = true
                    });
                    if (!result)
                        return result.ToContract<FormContract>();
                }
            }
            return await base.Update(request, cancellationToken);
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
}
