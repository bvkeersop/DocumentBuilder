using DocumentBuilder.Markdown.Options;
using System.Text;

namespace DocumentBuilder.Markdown.Model;

public class Paragraph : IMarkdownElement
{
    public string Value { get; }

    public Paragraph(string value)
    {
        Value = value;
    }

    public string ToMarkdown(MarkdownDocumentOptions options) => new StringBuilder().Append(Value).ToString();
}
