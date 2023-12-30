using EasyMicroservices.Cores.Interfaces;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormDetailContract : IUniqueIdentitySchema
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public long FormId { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
