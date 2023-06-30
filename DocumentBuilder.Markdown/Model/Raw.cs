using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class Raw : IMarkdownElement
{
    public string Value { get; }

    public Raw(string value)
    {
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options) => new StringBuilder().Append(Value).ToString();
}
