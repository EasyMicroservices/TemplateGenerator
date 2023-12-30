using EasyMicroservices.Cores.Database.Schemas;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class FormItemValueSchema : FullAbilitySchema
    {
        public string Value { get; set; }
        public int Index { get; set; }
    }
}
