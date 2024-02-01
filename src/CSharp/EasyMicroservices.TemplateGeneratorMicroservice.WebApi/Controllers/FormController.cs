using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers
{
    public class FormController : SimpleQueryServiceController<FormEntity, CreateFormRequestContract, FormContract, FormContract, long>
    {
        AppUnitOfWork _appUnitOfWork;
        public FormController(AppUnitOfWork uow) : base(uow)
        {
            _appUnitOfWork = uow;
        }

        public override Task<MessageContract<FormContract>> GetById(GetByIdRequestContract<long> request, CancellationToken cancellationToken = default)
        {
            return _appUnitOfWork.GetFormItemLogic().GetFormById(request, cancellationToken);
        }

        public override Task<ListMessageContract<FormContract>> Filter(FilterRequestContract filterRequest, CancellationToken cancellationToken = default)
        {
            return _appUnitOfWork.GetFormItemLogic().GetWithInclude(filterRequest, cancellationToken);
        }

        public override Task<ListMessageContract<FormContract>> GetAll(CancellationToken cancellationToken = default)
        {
            return _appUnitOfWork.GetFormItemLogic().GetWithInclude(null, cancellationToken);
        }

        public override async Task<MessageContract<FormContract>> Update(FormContract request, CancellationToken cancellationToken = default)
        {
            await _appUnitOfWork.GetFormItemLogic().CheckDeletedItems(request, cancellationToken);
            return await base.Update(request, cancellationToken);
        }

        public override async Task<MessageContract<FormContract>> UpdateChangedValuesOnly(FormContract request, CancellationToken cancellationToken = default)
        {
            await _appUnitOfWork.GetFormItemLogic().CheckDeletedItems(request, cancellationToken);
            return await base.UpdateChangedValuesOnly(request, cancellationToken);
        }
    }
}