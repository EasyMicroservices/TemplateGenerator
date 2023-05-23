using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormItemContract
    {
        public long Id { get; set; }
        public ItemType Type { get; set; }
    }
}
