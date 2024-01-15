using EasyMicroservices.Cores.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
public class FormItemEventEntity : FullAbilityIdSchema<long>
{
    public long FormItemId { get; set; }
    public long EventId { get; set; }

    public FormItemEntity FormItem { get; set; }
    public EventEntity Event { get; set; }

    public ICollection<FormItemEventActionEntity> FormItemEventActions { get; set; }
}
