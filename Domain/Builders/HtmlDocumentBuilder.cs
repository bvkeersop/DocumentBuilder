using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;
using NDocument.Domain.Writers;

namespace NDocument.Domain.Builders
{
    public class HtmlDocumentBuilder
    {
        public IEnumerable<IHtmlConvertable> HtmlConvertables { get; private set; } = new List<IHtmlConvertable>();
        private readonly HtmlDocumentWriter _htmlDocumentWriter;

        public HtmlDocumentBuilder(HtmlDocumentOptions options)
        {
            _htmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
        }

        public async Task WriteToStreamAsync(Stream outputStream)
        {
            await _htmlDocumentWriter.WriteToOutputStreamAsync(outputStream, HtmlConvertables).ConfigureAwait(false);
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
