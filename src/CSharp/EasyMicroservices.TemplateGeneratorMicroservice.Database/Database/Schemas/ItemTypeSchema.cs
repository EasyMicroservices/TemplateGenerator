using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas
{
    public class ItemTypeSchema
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
    }
}
