using EasyMicroservices.Cores.Interfaces;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormValuesContract : IUniqueIdentitySchema
    {
        public long Id { get; set; }
        public long FormId { get; set; }
        public string Name { get; set; }
        public string UniqueIdentity { get; set; }
        public List<FormItemValueContract> FormItemValues { get; set; }
    }
}
