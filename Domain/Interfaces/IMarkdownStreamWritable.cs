using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    public interface IMarkdownStreamWritable
    {
        Task WriteAsMarkdownToStreamAsync(Stream outputStream, MarkdownDocumentOptions options);
    }
}
