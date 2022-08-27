using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;
using DocumentBuilder.StreamWriters;

namespace DocumentBuilder.Factories
{
    internal static class MarkdownStreamWriterFactory
    {
        public static IMarkdownStreamWriter Create(Stream outputStream, MarkdownDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new MarkdownStreamWriter(streamWriter, newLineProvider);
        }
    }
}
