using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;
using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormItemContract
    {
        public long Id { get; set; }
        public string DefaultValue { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public ItemType Type { get; set; }
        public List<FormItemContract> Items { get; set; }
    }
}
