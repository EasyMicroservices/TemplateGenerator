using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormItemContract : FormItemBaseContract
    {
        public long Id { get; set; }
        public List<FormItemContract> Items { get; set; }
        public List<FormItemEventContract> Events { get; set; }
        
        public FormItemContract PrimaryFormItem { get; set; }
    }
}
