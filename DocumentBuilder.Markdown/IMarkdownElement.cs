using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown;

public interface IMarkdownElement
{
    string ToMarkdown(MarkdownDocumentOptions options);
}
