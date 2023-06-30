using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Schemas;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities
{
    public class FormDetailEntity : FormDetailSchema, IIdSchema<long>
    {
        public long Id { get; set; }

        public long FormId { get; set; }
        public FormEntity Form { get; set; }
    }
}
