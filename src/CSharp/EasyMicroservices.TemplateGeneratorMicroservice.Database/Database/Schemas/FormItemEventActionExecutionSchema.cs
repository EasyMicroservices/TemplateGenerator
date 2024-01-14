using EasyMicroservices.Cores.Database.Schemas;
using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
public class FormItemEventActionExecutionSchema : FullAbilitySchema
{
    public string RequestJson { get; set; }
    public string ResponseJson { get; set; }

    public ActionExecutionStatusType Status { get; set; }
}
