using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
public class ActionEntity : ActionSchema, IIdSchema<long>
{
    public long Id { get; set; }

    public long? ParentId { get; set; }
    public ActionEntity Parent { get; set; }

    public ICollection<ActionEntity> Children { get; set; }

    public ICollection<FormItemEventActionEntity> FormItemEventActions { get; set; }
}
