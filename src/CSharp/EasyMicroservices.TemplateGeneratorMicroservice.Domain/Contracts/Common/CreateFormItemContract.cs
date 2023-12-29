using System.Collections.Generic;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
public class CreateFormItemContract : FormItemBaseContract
{
    public FormItemContract PrimaryFormItem { get; set; }
    public List<CreateFormItemContract> Items { get; set; }
}