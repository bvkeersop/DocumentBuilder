using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.StreamWriters;

namespace DocumentBuilder.Domain.Factories
{
    internal static class HtmlStreamWriterFactory
    {
        public static IHtmlStreamWriter Create(Stream outputStream, HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            return new HtmlStreamWriter(streamWriter, newLineProvider, indentationProvider);
        }
    }
}
