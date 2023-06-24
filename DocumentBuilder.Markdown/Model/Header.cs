using System.Text;

namespace DocumentBuilder.Markdown.Model;

internal abstract class Header
{
    public string Indicator { get; }
    public string Value { get; }

    protected Header(string indicator, string value)
    {
        Indicator = indicator;
        Value = value;
    }

    protected ValueTask<string> CreateMarkdownHeader()
    {
        var sb = new StringBuilder();
        sb.Append(Indicator);
        sb.Append(' ');
        sb.Append(Value);
        var markdown = sb.ToString();
        return new ValueTask<string>(markdown);
    }
}
