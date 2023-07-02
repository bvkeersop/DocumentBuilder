using System.Text;

namespace DocumentBuilder.Html.Model;

public class InlineStyles
{
    public string? Value { get; private set; }
    public bool IsEmpty => Value is null;

    public void SetValue(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        if (Value == null)
        {
            return string.Empty;
        }

        return new StringBuilder()
            .Append("style=\"")
            .Append(Value)
            .Append('"')
            .ToString();
    }
}
