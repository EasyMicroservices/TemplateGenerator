using EasyMicroservices.Cores.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class ItemSchema : FullAbilitySchema
    {
        public string Title { get; set; }
        public string DefaultValue { get; set; }
        public int Index { get; set; }
        public string LocalVariableName { get; set; }
    }
}
