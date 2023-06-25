using DocumentBuilder.Markdown.Model;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown;

public interface IMarkdownElement
{
    ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args);
}
