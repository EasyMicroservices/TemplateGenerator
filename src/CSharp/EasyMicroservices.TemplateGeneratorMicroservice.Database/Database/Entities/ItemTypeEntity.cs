using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class ItemTypeEntity : ItemTypeSchema
    {
        public long Id { get; set; }

        public ICollection<FormItemEntity> FormItems { get; set; }
    }
}
