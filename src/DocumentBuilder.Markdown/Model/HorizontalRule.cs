using System.Text;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Markdown.Constants;

namespace DocumentBuilder.Markdown.Model;

internal class HorizontalRule : IMarkdownElement
{
    public string ToMarkdown(MarkdownDocumentOptions options)
        => new StringBuilder()
        .Append(Indicators.HorizontalRule)
        .ToString();
}
