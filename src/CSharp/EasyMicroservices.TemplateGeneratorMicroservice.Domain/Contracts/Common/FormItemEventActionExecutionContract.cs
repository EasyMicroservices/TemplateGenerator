using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class FormItemEventActionExecutionContract
{
    public string RequestJson { get; set; }
    public string ResponseJson { get; set; }

    public ActionExecutionStatusType Status { get; set; }
}
