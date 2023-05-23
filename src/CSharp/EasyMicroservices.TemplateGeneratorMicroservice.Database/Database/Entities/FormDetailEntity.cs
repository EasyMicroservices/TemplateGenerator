using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormDetailEntity : ItemSchema
    {
        public long Id { get; set; }

        public long FormId { get; set; }
        public FormEntity Form { get; set; }
    }
}
