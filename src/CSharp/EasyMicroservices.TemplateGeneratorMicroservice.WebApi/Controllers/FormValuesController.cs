using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormValuesController : SimpleQueryServiceController<FormFilledEntity, FormValuesContract, FormValuesContract, FormValuesContract, long>
    {
        IEasyReadableQueryableAsync<FormItemValueEntity> _formValueContractReadable;
        IEasyReadableQueryableAsync<FormItemEntity> _formItemContractReadable;
        public FormValuesController(IContractLogic<FormFilledEntity, FormValuesContract, FormValuesContract, FormValuesContract, long> contractReadable, IEasyReadableQueryableAsync<FormItemValueEntity> formValueContractReadable, IEasyReadableQueryableAsync<FormItemEntity> formItemContractReadable) : base(contractReadable)
        {
            _formValueContractReadable = formValueContractReadable;
            _formItemContractReadable = formItemContractReadable;
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
            var autoIndexFormItems = await _formItemContractReadable.Where(x => x.FormId == request.FormId && x.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).ToListAsync();
            var allValues = await _formValueContractReadable.Where(x => x.FormFilled.FormId == request.FormId).Include(x => x.FormItem).ThenInclude(x => x.ItemType).ToListAsync();

            var allAutoIncrementNumber = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).ToList();
            var globalFormItemValue = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber && x.FormItem?.ParentFormItem?.ItemType?.Type != DataTypes.ItemType.Table).OrderByDescending(x => x.Value).FirstOrDefault();
            var globalFormItem = autoIndexFormItems.FirstOrDefault(x => x.Id == globalFormItemValue.FormItemId);
            int number = 1;
            if (globalFormItemValue != null && globalFormItemValue.Value.HasValue() && int.TryParse(globalFormItemValue.Value, out int parsedInt))
                number = parsedInt + 1;
            else if (globalFormItem != null && int.TryParse(globalFormItem.DefaultValue, out parsedInt))
                number = parsedInt;

            foreach (var formItemValue in request.FormItemValues)
            {
                if (autoIndexFormItems.Any(x => x.Id == formItemValue.FormItemId))
                {
                    if (!int.TryParse(formItemValue.Value, out _))
                    {
                        formItemValue.Value = number.ToString();
                        number++;
                    }
                }
            }
            return await base.Add(request, cancellationToken);
        }
    }
}
