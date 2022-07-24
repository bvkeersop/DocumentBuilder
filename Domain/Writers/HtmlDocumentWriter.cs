using NDocument.Domain.Constants;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Writers
{
    internal class HtmlDocumentWriter
    {
        private readonly INewLineProvider _newLineProvider;
        private readonly IIndentationProvider _indentationProvider;
        private readonly HtmlDocumentOptions _options;

        public HtmlDocumentWriter(HtmlDocumentOptions options)
        {
            _indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        public async Task WriteToOutputStreamAsync(Stream outputStream, IEnumerable<IHtmlConvertable> htmlConvertables)
        {
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            using var htmlStreamWriter = new HtmlStreamWriter(streamWriter, _newLineProvider, _indentationProvider);

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

            await streamWriter.FlushAsync().ConfigureAwait(false);
        }
    }
}
