using EasyMicroservices.Cores.Database.Schemas;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class FormDetailSchema : FullAbilitySchema
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
