using DocumentBuilder.Constants;
using System.Text;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class HorizontalRule : IMarkdownElement
{
    public string ToMarkdown(MarkdownDocumentOptions options)
        => new StringBuilder()
        .Append(Indicators.HorizontalRule)
        .ToString();
}
