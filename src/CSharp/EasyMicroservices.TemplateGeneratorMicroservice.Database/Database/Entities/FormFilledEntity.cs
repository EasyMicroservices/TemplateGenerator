using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormFilledEntity
    {
        public long Id { get; set; }

        public long FormId { get; set; }
        public FormEntity Form { get; set; }

        public ICollection<FormItemValueEntity> FormItemValues { get; set; }
    }
}
