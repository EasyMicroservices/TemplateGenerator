using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormEntity : FormSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public ICollection<FormItemEntity> FormItems { get; set; }
        public ICollection<FormDetailEntity> FormDetails { get; set; }
        public ICollection<FormFilledEntity> FormFills { get; set; }
    }
}

