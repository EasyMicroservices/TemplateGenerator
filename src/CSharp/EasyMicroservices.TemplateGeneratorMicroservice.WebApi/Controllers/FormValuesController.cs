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
        public FormValuesController(IContractLogic<FormFilledEntity, FormValuesContract, FormValuesContract, FormValuesContract, long> contractReadable, IEasyReadableQueryableAsync<FormItemValueEntity> formValueContractReadable) : base(contractReadable)
        {
            _formValueContractReadable = formValueContractReadable;
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
            var allValues = await _formValueContractReadable.Where(x => x.FormFilled.FormId == request.FormId).Include(x => x.FormItem).ThenInclude(x => x.ItemType).ToListAsync();
            
            var allAutoIncrementNumber = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).ToList();
            var index = allValues.Where(x => x.FormItem.ItemType.Type == DataTypes.ItemType.AutoIncrementNumber).Select(x => x.Value).OrderByDescending(x => x).FirstOrDefault();
            int number = 1;
            if (index.HasValue() && int.TryParse(index, out int parsedInt))
                number = parsedInt + 1;
            foreach (var formItemValue in request.FormItemValues)
            {
                if (allAutoIncrementNumber.Any(x => x.FormItemId == formItemValue.FormItemId))
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
