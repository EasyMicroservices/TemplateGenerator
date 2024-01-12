using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class ActionContract
{
    public long Id { get; set; }
    public string JobName { get; set; }
    public int OrderIndex { get; set; }

    public long? ParentId { get; set; }

    public List<ActionContract> Children { get; set; }
}
