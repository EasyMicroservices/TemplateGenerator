namespace EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;
public enum ActionExecutionStatusType : byte
{
    None = 0,
    Default = 1,
    All = 2,
    Other = 3,
    Unknown = 4,
    Nothing = 5,
    Success = 6,
    Failed = 7
}