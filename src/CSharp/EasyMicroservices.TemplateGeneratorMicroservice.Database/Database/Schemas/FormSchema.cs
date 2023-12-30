using EasyMicroservices.Cores.Database.Schemas;
using EasyMicroservices.Cores.Interfaces;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class FormSchema : FullAbilitySchema
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
