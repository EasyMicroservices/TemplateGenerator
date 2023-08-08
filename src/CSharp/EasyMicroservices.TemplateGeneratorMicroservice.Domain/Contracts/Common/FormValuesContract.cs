using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormValuesContract
    {
        public long FormId { get; set; }
        public string Name { get; set; }
        public List<FormItemValueContract> FormItemValues { get; set; }
    }
}
