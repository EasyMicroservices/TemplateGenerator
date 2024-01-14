using EasyMicroservices.Cores.Database.Schemas;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;
public class ActionSchema : FullAbilitySchema
{
    public string JobName { get; set; }
    public int OrderIndex { get; set; }

    public ICollection<FormItemActionJobEntity> FormItemActionJobs { get; set; }
}
