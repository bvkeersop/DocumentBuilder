using DocumentBuilder.Domain.DocumentWriters;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.Model;
using DocumentBuilder.Domain.Model.Generic;

namespace DocumentBuilder.Domain.DocumentBuilders
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
            await _htmlDocumentWriter.WriteToStreamAsync(outputStream, HtmlConvertables).ConfigureAwait(false);
        }

        public HtmlDocumentBuilder AddHeader1(string header1)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header1(header1));
            return this;
        }

        public HtmlDocumentBuilder AddHeader2(string header2)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header2(header2));
            return this;
        }

        public HtmlDocumentBuilder AddHeader3(string header3)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header3(header3));
            return this;
        }

        public HtmlDocumentBuilder AddHeader4(string header4)
        {
            HtmlConvertables = HtmlConvertables.Append(new Header4(header4));
            return this;
        }

        public HtmlDocumentBuilder AddParagraph(string paragraph)
        {
            HtmlConvertables = HtmlConvertables.Append(new Paragraph(paragraph));
            return this;
        }

        public HtmlDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            HtmlConvertables = HtmlConvertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public HtmlDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            HtmlConvertables = HtmlConvertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public HtmlDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            HtmlConvertables = HtmlConvertables.Append(new Table<T>(tableRows));
            return this;
        }

        public HtmlDocumentBuilder AddImage(string name, string path, string? caption = null)
        {
            HtmlConvertables = HtmlConvertables.Append(new Image(name, path, caption));
            return this;
        }
    }
}
