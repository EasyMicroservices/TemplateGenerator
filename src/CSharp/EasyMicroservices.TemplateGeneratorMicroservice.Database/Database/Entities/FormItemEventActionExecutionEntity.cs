using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
public class FormItemEventActionExecutionEntity : FormItemEventActionExecutionSchema, IIdSchema<long>
{
    public long Id { get; set; }
    public long FormItemEventActionId { get; set; }

    public FormItemEventActionEntity FormItemEventAction { get; set; }
}
