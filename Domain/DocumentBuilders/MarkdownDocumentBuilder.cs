using DocumentBuilder.Domain.DocumentWriters;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.Model;
using DocumentBuilder.Domain.Model.Generic;

namespace DocumentBuilder.Domain.DocumentBuilders
{
    public class MarkdownDocumentBuilder
    {
        public IEnumerable<IMarkdownConvertable> MarkdownConvertables { get; private set; } = new List<IMarkdownConvertable>();
        private readonly MarkdownDocumentWriter _markdownDocumentWriter;

        public MarkdownDocumentBuilder(MarkdownDocumentOptions options)
        {
            _markdownDocumentWriter = new MarkdownDocumentWriter(MarkdownStreamWriterFactory.Create, options);
        }

        public async Task WriteToStreamAsync(Stream outputStream)
        {
            await _markdownDocumentWriter.WriteToStreamAsync(outputStream, MarkdownConvertables).ConfigureAwait(false);
        }

        public MarkdownDocumentBuilder AddHeader1(string header1)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header1(header1));
            return this;
        }

        public MarkdownDocumentBuilder AddHeader2(string header2)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header2(header2));
            return this;
        }

        public MarkdownDocumentBuilder AddHeader3(string header3)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header3(header3));
            return this;
        }

        public MarkdownDocumentBuilder AddHeader4(string header4)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Header4(header4));
            return this;
        }

        public MarkdownDocumentBuilder AddParagraph(string paragraph)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Paragraph(paragraph));
            return this;
        }

        public MarkdownDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public MarkdownDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public MarkdownDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            MarkdownConvertables = MarkdownConvertables.Append(new Table<T>(tableRows));
            return this;
        }
    }
}
