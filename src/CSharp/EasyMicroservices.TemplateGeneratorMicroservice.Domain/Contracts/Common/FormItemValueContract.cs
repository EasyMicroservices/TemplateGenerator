using EasyMicroservices.Cores.Interfaces;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormItemValueContract : IUniqueIdentitySchema
    {
        public long FormItemId { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
