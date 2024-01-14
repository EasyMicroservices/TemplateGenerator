using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class FormItemEventContract
{
    public long Id { get; set; }
    public long? FormItemId { get; set; }
    public long EventId { get; set; }

    public FormItemContract FormItem { get; set; }
    public EventContract Event { get; set; }

    public List<FormItemEventActionContract> FormItemEventActions { get; set; }
}
