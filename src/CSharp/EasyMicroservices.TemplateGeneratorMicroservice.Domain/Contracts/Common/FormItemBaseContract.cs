using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class FormItemBaseContract
{
    public string DefaultValue { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }
    public ItemType Type { get; set; }
}
