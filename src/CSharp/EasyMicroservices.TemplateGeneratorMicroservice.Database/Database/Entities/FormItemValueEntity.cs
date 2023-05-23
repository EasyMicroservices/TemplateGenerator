using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormItemValueEntity : FormItemValueSchema
    {
        public long Id { get; set; }

        public long FormFilledId { get; set; }
        public FormFilledEntity FormFilled { get; set; }

        public long FormItemId { get; set; }
        public FormItemEntity FormItem { get; set; }
    }
}
