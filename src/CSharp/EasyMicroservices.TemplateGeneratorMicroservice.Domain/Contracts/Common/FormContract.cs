using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FormItemContract> Items { get; set; }
    }
}
