using EasyMicroservices.Cores.Database.Interfaces;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class FormSchema : IUniqueIdentitySchema
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
