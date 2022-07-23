using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Builders
{
    public class HtmlDocumentBuilder
    {
        public IEnumerable<IHtmlConvertable> HtmlConvertables { get; private set; } = new List<IHtmlConvertable>();
        private IIndentationProvider _indentationProvider;
        private INewLineProvider _newLineProvider;
        private HtmlDocumentOptions _options;

        private const string _docType = "<!DOCTYPE html>";
        private const string _htmlIndicator = "html";
        private const string _bodyIndicator = "body";

        public HtmlDocumentBuilder(HtmlDocumentOptions options)
        {
            _indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        public async Task WriteToOutputStreamAsync(Stream outputStream)
        {
            var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
            using var htmlStreamWriter  = new HtmlStreamWriter(streamWriter, _newLineProvider, _indentationProvider);

            await htmlStreamWriter.WriteLineAsync(_docType).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(_htmlIndicator.ToHtmlStartTag()).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(_bodyIndicator.ToHtmlStartTag(), 1).ConfigureAwait(false);

            for (var i = 0; i < HtmlConvertables.Count(); i++)
            {
                var html = await HtmlConvertables.ElementAt(i).ToHtmlAsync(_options, 2);
                await htmlStreamWriter.WriteAsync(html).ConfigureAwait(false);
            }

            await htmlStreamWriter.WriteLineAsync(_bodyIndicator.ToHtmlEndTag(), 1).ConfigureAwait(false);
            await htmlStreamWriter.WriteLineAsync(_htmlIndicator.ToHtmlEndTag()).ConfigureAwait(false);

            await streamWriter.FlushAsync().ConfigureAwait(false);
        }

        public HtmlDocumentBuilder WithHeader1(string header1)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header1(header1));
            return this;
        }

        public HtmlDocumentBuilder WithHeader2(string header2)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header2(header2));
            return this;
        }

        public HtmlDocumentBuilder WithHeader3(string header3)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header3(header3));
            return this;
        }

        public HtmlDocumentBuilder WithHeader4(string header4)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header4(header4));
            return this;
        }

        public HtmlDocumentBuilder WithParagraph(string paragraph)
        {
            HtmlConvertables = HtmlConvertables.Append(new Paragraph(paragraph));
            return this;
        }

        public HtmlDocumentBuilder WithOrderedList<T>(IEnumerable<T> orderedList)
        {
            HtmlConvertables = HtmlConvertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public HtmlDocumentBuilder WithUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            HtmlConvertables = HtmlConvertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public HtmlDocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            HtmlConvertables = HtmlConvertables.Append(new Table<T>(tableRows));
            return this;
        }
    }
}
