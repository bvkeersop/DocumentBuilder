using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

public interface IMarkdownElement
{
    string ToMarkdown(MarkdownDocumentOptions options);
}
