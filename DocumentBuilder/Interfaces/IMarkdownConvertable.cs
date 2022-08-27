using DocumentBuilder.Options;

namespace DocumentBuilder.Interfaces
{
    public interface IMarkdownConvertable
    {
        ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options);
    }
}
