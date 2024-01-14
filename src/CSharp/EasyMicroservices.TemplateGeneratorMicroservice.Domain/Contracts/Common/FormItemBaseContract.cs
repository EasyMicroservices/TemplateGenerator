using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class FormItemBaseContract : IUniqueIdentitySchema
{
    public string DefaultValue { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }
    public string LocalVariableName { get; set; }
    public ItemType Type { get; set; }
    public long? PrimaryFormItemId { get; set; }
    public long? FormId { get; set; }
    public long? ParentFormItemId { get; set; }
    public string UniqueIdentity { get; set; }
}
