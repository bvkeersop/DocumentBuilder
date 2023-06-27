using DocumentBuilder.Core.Enumerations;

namespace DocumentBuilder.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    public ColumnName Name { get; init; }
    public int Order { get; init; }
    public Alignment Alignment { get; init; }

    public ColumnAttribute(string? name = null, Alignment alignment = Alignment.Default, int order = int.MaxValue)
    {
        Name = CreateColumnName(name);
        Alignment = alignment;
        Order = order;
    }

    private static ColumnName CreateColumnName(string? name)
    {
        if (name == null)
        {
            return new ColumnName(isSet: false, value: "");
        }

        return new ColumnName(isSet: true, name);
    }
}

public class ColumnName
{
    public bool IsSet { get; init; }
    public string Value { get; }

    public ColumnName(bool isSet, string value)
    {
        IsSet = isSet;
        Value = value;
    }
}