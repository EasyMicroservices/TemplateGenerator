using EasyMicroservices.Cores.Interfaces;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class FormFilledSchema : IDateTimeSchema, ISoftDeleteSchema, IUniqueIdentitySchema
    {
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
