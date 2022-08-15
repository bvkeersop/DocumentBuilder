using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Interfaces
{
    public interface IMarkdownConvertable
    {
        ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options);
    }
}
