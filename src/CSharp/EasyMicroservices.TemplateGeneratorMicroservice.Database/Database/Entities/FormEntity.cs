using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormEntity
    {
        public long Id { get; set; }

        public ICollection<FormItemEntity> FormItems { get; set; }
        public ICollection<FormDetailEntity> FormDetails { get; set; }
        public ICollection<FormFilledEntity> FormFills { get; set; }
    }
}

