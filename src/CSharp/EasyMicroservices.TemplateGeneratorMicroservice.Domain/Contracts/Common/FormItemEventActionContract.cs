using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class FormItemEventActionContract
{
    public long Id { get; set; }
    public long FormItemEventId { get; set; }
    public long ActionId { get; set; }
    public long? FormItemId { get; set; }
    public long? ParentId { get; set; }
    public int OrderIndex { get; set; }

    public ActionContract Action { get; set; }
    public List<FormItemEventActionContract> Children { get; set; }
    public List<FormItemEventActionExecutionContract> FormItemEventActionCallHistories { get; set; }
}
