using EasyMicroservices.Cores.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;

/// <summary>
/// do the action on this formItem
/// </summary>
public class FormItemActionJobEntity : FullAbilityIdSchema<long>
{
    public long ActionId { get; set; }
    public ActionEntity Action { get; set; }

    public long FormItemId { get; set; }
    public FormItemEntity FormItem { get; set; }
}
