using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormValuesController : SimpleQueryServiceController<FormFilledEntity, FormValuesContract, FormValuesContract, FormValuesContract, long>
    {
        public FormValuesController(IUnitOfWork uow) : base(uow)
        {
        }

        protected override Func<IQueryable<FormFilledEntity>, IQueryable<FormFilledEntity>> OnGetAllQuery()
        {
            return query => query
            .Include(e => e.FormItemValues);
        }

        protected override Func<IQueryable<FormFilledEntity>, IQueryable<FormFilledEntity>> OnGetQuery()
        {
            return query => query
            .Include(e => e.FormItemValues);
        }

        public override async Task<MessageContract<long>> Add(FormValuesContract request, CancellationToken cancellationToken = default)
        {
            var autoIndexFormItems = (await UnitOfWork.GetLongLogic<FormItemEntity>().GetAll(q => q.Where(x => x.FormId == request.FormId && x.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber)))
                .GetCheckedResult();

            var allValues = (await UnitOfWork.GetLongLogic<FormItemValueEntity>().GetAll(q => q.Where(x => x.FormFilled.FormId == request.FormId).Include(x => x.FormItem).ThenInclude(x => x.ItemType)))
                 .GetCheckedResult();

            var allAutoIncrementNumber = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).ToList();
            var globalFormItemValue = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber && x.FormItem?.ParentFormItem?.ItemType?.Type != DataTypes.ItemType.Table).Select(x=> long.TryParse(x.Value,out long num) ? num : 0).OrderByDescending(x => x).FirstOrDefault();
            //var globalFormItem = autoIndexFormItems.FirstOrDefault(x => x.Id == globalFormItemValue?.FormItemId);
            long number = 1;
            if (globalFormItemValue > 0)
                number = globalFormItemValue + 1;
            //else if (globalFormItem != null && long.TryParse(globalFormItem.DefaultValue, out long parsedInt))
            //    number = parsedInt;

            foreach (var formItemValue in request.FormItemValues)
            {
                if (autoIndexFormItems.Any(x => x.Id == formItemValue.FormItemId))
                {
                    if (!long.TryParse(formItemValue.Value, out _))
                    {
                        formItemValue.Value = number.ToString();
                        number++;
                    }
                }
            }
            return await base.Add(request, cancellationToken);
        }

        bool HasUniqueOdentity(GetByUniqueIdentityRequestContract request, FormItemValueEntity formItemValueEntity)
        {
            if (formItemValueEntity.UniqueIdentity.IsNullOrEmpty())
            {
                if (formItemValueEntity.FormFilled != null)
                    return formItemValueEntity.FormFilled.UniqueIdentity.StartsWith(request.UniqueIdentity);
                return false;
            }
            return formItemValueEntity.UniqueIdentity.StartsWith(request.UniqueIdentity);
        }

        [HttpPost]
        public async Task<MessageContract<long>> GetBiggestAutoIncrementNumber(GetByUniqueIdentityRequestContract request)
        {
            var allValues = (await UnitOfWork.GetLongLogic<FormItemValueEntity>()
                .GetAll(
                q => q.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).Include(x => x.FormFilled).Include(x => x.FormItem).ThenInclude(x => x.ItemType)))
                 .GetCheckedResult();
            allValues = allValues.Where(x => HasUniqueOdentity(request, x)).ToList();
            var globalFormItemValue = allValues.Where(x => x.FormItem?.ParentFormItem?.ItemType?.Type != DataTypes.ItemType.Table)
                .OrderByDescending(x => long.TryParse(x.Value, out long value) ? value : 0).FirstOrDefault();
            long number = 1;
            if (globalFormItemValue != null && globalFormItemValue.Value.HasValue() && long.TryParse(globalFormItemValue.Value, out long parsedInt))
                number = parsedInt + 1;
            else if (long.TryParse(globalFormItemValue?.FormItem?.DefaultValue, out long defaultValue))
                number += defaultValue;
            return number;
        }
    }
}
