using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.StreamWriters;

namespace NDocument.Domain.Factories
{
    internal class HtmlStreamWriterFactory
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
