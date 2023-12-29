using EasyMicroservices.Cores.Database.Schemas;
using EasyMicroservices.Cores.Interfaces;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class ItemSchema : FullAbilitySchema
    {
        public string Title { get; set; }
        public string DefaultValue { get; set; }
        public int Index { get; set; }
    }
}
