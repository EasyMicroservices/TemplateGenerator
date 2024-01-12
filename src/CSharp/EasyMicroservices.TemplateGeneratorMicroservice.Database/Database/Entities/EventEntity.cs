using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
public class EventEntity : EventSchema, IIdSchema<long>
{
    public long Id { get; set; }
    public ICollection<FormItemEventEntity> FormItemEvents { get; set; }
}