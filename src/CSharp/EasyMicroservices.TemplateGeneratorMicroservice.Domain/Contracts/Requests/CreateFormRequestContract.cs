using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests
{
    public class CreateFormRequestContract
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<FormItemContract> FormItems { get; set; }
    }
}
