using DocumentBuilder.Markdown.Model;

namespace DocumentBuilder.Markdown;

public interface IMarkdownElement
{
    ValueTask<string> ToMarkdownAsync(ToMarkdownArgs args);
}
