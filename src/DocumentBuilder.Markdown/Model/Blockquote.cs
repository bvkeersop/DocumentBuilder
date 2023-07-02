using DocumentBuilder.Markdown.Constants;
using DocumentBuilder.Markdown.Options;
using System.Text;

namespace DocumentBuilder.Markdown.Model;

internal class Blockquote : IMarkdownElement
{
    private string Value { get; }

    public Blockquote(string value)
    {
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options) 
        => new StringBuilder()
        .Append(Indicators.Blockquote)
        .Append(Value)
        .ToString();
}
