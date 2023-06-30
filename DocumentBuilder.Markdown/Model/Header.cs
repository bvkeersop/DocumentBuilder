using DocumentBuilder.Markdown.Options;
using System.Text;

namespace DocumentBuilder.Markdown.Model;

internal abstract class Header : IMarkdownElement
{
    public string Indicator { get; }
    public string Value { get; }

    protected Header(string indicator, string value)
    {
        Indicator = indicator;
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options)
        => new StringBuilder()
            .Append(Indicator)
            .Append(' ')
            .Append(Value)
            .ToString();
}
