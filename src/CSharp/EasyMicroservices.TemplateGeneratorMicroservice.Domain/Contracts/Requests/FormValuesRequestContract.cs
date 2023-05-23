using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests
{
    public class FormValuesRequestContract
    {
        public long FormId { get; set; }
        public List<FormItemValueContract> FormItemValues { get; set; }
    }
}
