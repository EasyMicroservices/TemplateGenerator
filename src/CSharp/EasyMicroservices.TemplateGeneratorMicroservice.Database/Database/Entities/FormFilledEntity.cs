using EasyMicroservices.Cores.Database.Interfaces;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormFilledEntity : IIdSchema<long>
    {
        public long Id { get; set; }

        public long FormId { get; set; }
        public FormEntity Form { get; set; }

        public ICollection<FormItemValueEntity> FormItemValues { get; set; }
    }
}
