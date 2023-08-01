namespace EasyMicroservices.TemplateGeneratorMicroservice.DataTypes
{
    public enum ItemType : byte
    {
        None = 0,
        Default = 1,
        All = 2,
        Other = 3,
        Unknown = 4,
        Nothing = 5,
        TextBox = 6,
        CheckList = 7,
        CheckBox = 8,
        OptionList = 9,
        DateTime = 10,
        DateOnly = 11,
        TimeOnly = 12,
        Label = 13,
        Table = 14,
        Row = 15
    }
}
