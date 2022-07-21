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

        public HtmlDocumentBuilder(HtmlDocumentOptions options)
        {
            _indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            _newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            _options = options;
        }

        public async Task WriteToOutputStreamAsync(Stream outputStream)
        {
            using var streamWriter = new StreamWriter(outputStream, leaveOpen: true);

            for (var i = 0; i < HtmlConvertables.Count(); i++)
            {
                var html = await HtmlConvertables.ElementAt(i).ToHtmlAsync(_options);
                await streamWriter.WriteAsync(html).ConfigureAwait(false);

                if (i < HtmlConvertables.Count() - 1)
                {
                    await streamWriter.WriteAsync(_newLineProvider.GetNewLine()).ConfigureAwait(false);
                }
            }

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
