using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Factories
{
    internal static class MarkdownStreamWriterFactory
    {
        public static MarkdownStreamWriter Create(Stream outputStream, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new MarkdownStreamWriter(streamWriter, newLineProvider);
        }
    }
}
