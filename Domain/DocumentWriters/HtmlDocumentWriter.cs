using NDocument.Domain.Constants;
using NDocument.Domain.Extensions;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;

namespace NDocument.Domain.DocumentWriters
{
    internal class HtmlDocumentWriter
    {
        private Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> _htmlStreamWriterFactory;
        private readonly HtmlDocumentOptions _options;

        public HtmlDocumentWriter(Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> htmlStreamWriterFactory, HtmlDocumentOptions options)
        {
            _htmlStreamWriterFactory = htmlStreamWriterFactory;
            _options = options;
        }

        public async Task WriteToStreamAsync(Stream outputStream, IEnumerable<IHtmlConvertable> htmlConvertables)
        {
            using var htmlStreamWriter = _htmlStreamWriterFactory(outputStream, _options);

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.DocType).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag()).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < htmlConvertables.Count(); i++)
            {
                var html = await htmlConvertables.ElementAt(i).ToHtmlAsync(_options, 2);
                await htmlStreamWriter.WriteAsync(html).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag()).ConfigureAwait(false);

            await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
        }
    }
}
