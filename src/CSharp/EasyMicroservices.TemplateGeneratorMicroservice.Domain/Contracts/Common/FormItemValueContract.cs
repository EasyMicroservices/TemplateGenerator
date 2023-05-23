using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common
{
    public class FormItemValueContract
    {
        public long FormItemId { get; set; }
        public string Value { get; set; }
    }
}
