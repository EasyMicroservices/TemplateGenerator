﻿using EasyMicroservices.Cores.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
public class FormItemEventActionEntity : FullAbilityIdSchema<long>
{
    public long? FormItemEventId { get; set; }
    public long ActionId { get; set; }
    public long? FormItemId { get; set; }
    public long? ParentId { get; set; }
    /// <summary>
    /// for example you action to load page and show form item id to another control with his variable name
    /// </summary>
    public string InfluencedToVariableName { get; set; }

    public int OrderIndex { get; set; }

    public FormItemEntity FormItem { get; set; }
    public FormItemEventEntity FormItemEvent { get; set; }
    public ActionEntity Action { get; set; }

    public ICollection<FormItemEventActionEntity> Children { get; set; }
    public ICollection<FormItemEventActionExecutionEntity> FormItemEventActionCallHistories { get; set; }
}
