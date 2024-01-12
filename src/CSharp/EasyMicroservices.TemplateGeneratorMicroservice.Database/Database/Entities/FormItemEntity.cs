using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormItemEntity : ItemSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public long? FormId { get; set; }
        public FormEntity Form { get; set; }

        public long ItemTypeId { get; set; }
        public ItemTypeEntity ItemType { get; set; }

        public long? ParentFormItemId { get; set; }
        public FormItemEntity ParentFormItem { get; set; }
        public ICollection<FormItemEntity> Children { get; set; }

        public long? PrimaryFormItemId { get; set; }
        public FormItemEntity PrimaryFormItem { get; set; }
        public ICollection<FormItemEntity> PrimaryFormItems { get; set; }

        public ICollection<FormItemValueEntity> FormItemValues { get; set; }
        public ICollection<FormItemEventEntity> FormItemEvents { get; set; }
        public ICollection<FormItemActionJobEntity> FormItemActionJobs { get; set; }
    }
}
