using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Model.Generic;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.DocumentWriters;
using DocumentBuilder.Domain.Model;

namespace DocumentBuilder.Domain.DocumentBuilders
{
    internal class GenericDocumentBuilder
    {
        public IEnumerable<GenericElement> Convertables { get; private set; } = new List<GenericElement>();
        private readonly DocumentOptions _options;

        public GenericDocumentBuilder(DocumentOptions options)
        {
            _options = options;
        }

        public async Task WriteToStreamAsync(Stream outputStream, DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.Markdown:
                    await WriteToStreamAsMarkdownAsync(outputStream).ConfigureAwait(false);
                    break;
                case DocumentType.Html:
                    await WriteToStreamAsHtmlAsync(outputStream).ConfigureAwait(false);
                    break;
                default:
                    throw new NotSupportedException($"{documentType} is not supported");
            }
        }

        public GenericDocumentBuilder AddHeader1(string header1)
        {
            Convertables = Convertables.Append(new Header1(header1));
            return this;
        }

        public GenericDocumentBuilder AddHeader2(string header2)
        {
            Convertables = Convertables.Append(new Header2(header2));
            return this;
        }

        public GenericDocumentBuilder AddHeader3(string header3)
        {
            Convertables = Convertables.Append(new Header3(header3));
            return this;
        }

        public GenericDocumentBuilder AddHeader4(string header4)
        {
            Convertables = Convertables.Append(new Header4(header4));
            return this;
        }

        public GenericDocumentBuilder AddParagraph(string paragraph)
        {
            Convertables = Convertables.Append(new Paragraph(paragraph));
            return this;
        }

        public GenericDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            Convertables = Convertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public GenericDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            Convertables = Convertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public GenericDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            Convertables = Convertables.Append(new Table<T>(tableRows));
            return this;
        }

        private async Task WriteToStreamAsMarkdownAsync(Stream outputStream)
        {
            var options = _options.ToMarkdownDocumentOptions();
            var markdownDocumentWriter = new MarkdownDocumentWriter(MarkdownStreamWriterFactory.Create, options);
            await markdownDocumentWriter.WriteToStreamAsync(outputStream, Convertables);
        }

        private async Task WriteToStreamAsHtmlAsync(Stream outputStream)
        {
            var options = _options.ToHtmlDocumentOptions();
            var htmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
            await htmlDocumentWriter.WriteToStreamAsync(outputStream, Convertables);
        }
    }
}
