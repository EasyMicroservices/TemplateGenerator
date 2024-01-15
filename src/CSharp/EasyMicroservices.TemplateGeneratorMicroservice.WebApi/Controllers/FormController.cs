using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormController : SimpleQueryServiceController<FormEntity, CreateFormRequestContract, FormContract, FormContract, long>
    {
        public FormController(IUnitOfWork uow) : base(uow)
        {
        }

        public override async Task<MessageContract<FormContract>> GetById(GetByIdRequestContract<long> request, CancellationToken cancellationToken = default)
        {
            await using var context = UnitOfWork.GetDatabase();
            var uniqueIdentity = await UnitOfWork.GetCurrentUserUniqueIdentity();
            uniqueIdentity += "-";
            var readable = context.GetReadableOf<FormEntity>();
            var query = readable.Where(e => e.Id == request.Id && !e.IsDeleted && e.UniqueIdentity.StartsWith(uniqueIdentity));
            var result = await query.Include(x => x.FormItems.Where(x => !x.IsDeleted)).FirstOrDefaultAsync(cancellationToken);
            var itemReadable = context.GetReadableOf<FormItemEntity>();
            await FormItemLogic.LoadAllFormItems(itemReadable, result.FormItems.ToList(), new HashSet<long>());
            return await UnitOfWork.GetMapper().MapAsync<FormContract>(result);
        }

        public override Task<ListMessageContract<FormContract>> Filter(FilterRequestContract filterRequest, CancellationToken cancellationToken = default)
        {
            return GetWithInclude(filterRequest, cancellationToken);
        }

        public override Task<ListMessageContract<FormContract>> GetAll(CancellationToken cancellationToken = default)
        {
            return GetWithInclude(null, cancellationToken);
        }

        async Task<ListMessageContract<FormContract>> GetWithInclude(FilterRequestContract filterRequest, CancellationToken cancellationToken)
        {
            await using var context = UnitOfWork.GetDatabase();
            var uniqueIdentity = await UnitOfWork.GetCurrentUserUniqueIdentity();
            uniqueIdentity += "-";
            var readable = context.GetReadableOf<FormEntity>();
            var query = readable.Where(e => !e.IsDeleted && e.UniqueIdentity.StartsWith(uniqueIdentity));
            var count = await query.LongCountAsync();
            if (filterRequest != null)
            {
                if (filterRequest.Index.HasValue)
                    query = query.Skip((int)filterRequest.Index.Value);
                if (filterRequest.Length.HasValue)
                    query = query.Take((int)filterRequest.Length.Value);
            }
            var result = await query.Include(x => x.FormItems.Where(x => !x.IsDeleted)).ToListAsync(cancellationToken);
            var itemReadable = context.GetReadableOf<FormItemEntity>();
            await FormItemLogic.LoadAllFormItems(itemReadable, result.SelectMany(x => x.FormItems).ToList(), new HashSet<long>());
            return new ListMessageContract<FormContract>()
            {
                TotalCount = count,
                IsSuccess = true,
                Result = await UnitOfWork.GetMapper().MapToListAsync<FormContract>(result)
            };
        }

        public override async Task<MessageContract<FormContract>> Update(FormContract request, CancellationToken cancellationToken = default)
        {
            await FormItemLogic.CheckDeletedItems(UnitOfWork, request, cancellationToken);
            return await base.Update(request, cancellationToken);
        }

        public override async Task<MessageContract<FormContract>> UpdateChangedValuesOnly(FormContract request, CancellationToken cancellationToken = default)
        {
            await FormItemLogic.CheckDeletedItems(UnitOfWork, request, cancellationToken);
            return await base.UpdateChangedValuesOnly(request, cancellationToken);
        }
    }
}
