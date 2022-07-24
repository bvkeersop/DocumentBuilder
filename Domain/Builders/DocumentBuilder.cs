using NDocument.Domain.Enumerations;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Writers;

namespace NDocument.Domain.Builders
{
    internal class DocumentBuilder
    {
        public IEnumerable<GenericElement> Convertables { get; private set; } = new List<GenericElement>();
        private readonly DocumentOptions _options;

        public DocumentBuilder(DocumentOptions options)
        {
            _options = options;
        }

        public async Task WriteToStreamAsync(Stream outputStream, DocumentType documentType)
        {
            switch (documentType)
            {
                case (DocumentType.Markdown):
                    await WriteToStreamAsMarkdownAsync(outputStream).ConfigureAwait(false);
                    break;
                case (DocumentType.Html):
                    await WriteToStreamAsHtmlAsync(outputStream).ConfigureAwait(false);
                    break;
                default:
                    throw new NotSupportedException($"{documentType} is not supported");
            };
        }

        public DocumentBuilder WithHeader1(string header1)
        {
            Convertables = Convertables.Append(new Header1(header1));
            return this;
        }

        public DocumentBuilder WithHeader2(string header2)
        {
            Convertables = Convertables.Append(new Header2(header2));
            return this;
        }

        public DocumentBuilder WithHeader3(string header3)
        {
            Convertables = Convertables.Append(new Header3(header3));
            return this;
        }

        public DocumentBuilder WithHeader4(string header4)
        {
            Convertables = Convertables.Append(new Header4(header4));
            return this;
        }

        public DocumentBuilder WithParagraph(string paragraph)
        {
            Convertables = Convertables.Append(new Paragraph(paragraph));
            return this;
        }

        public DocumentBuilder WithOrderedList<T>(IEnumerable<T> orderedList)
        {
            Convertables = Convertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        public DocumentBuilder WithUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            Convertables = Convertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        public DocumentBuilder WithTable<T>(IEnumerable<T> tableRows)
        {
            Convertables = Convertables.Append(new Table<T>(tableRows));
            return this;
        }

        private async Task WriteToStreamAsMarkdownAsync(Stream outputStream)
        {
            var options = _options.ToMarkdownDocumentOptions();
            var markdownDocumentWriter = new MarkdownDocumentWriter(options);
            await markdownDocumentWriter.WriteToStreamAsync(outputStream, Convertables);
        }

        private async Task WriteToStreamAsHtmlAsync(Stream outputStream)
        {
            var options = _options.ToHtmlDocumentOptions();
            var htmlDocumentWriter = new HtmlDocumentWriter(options);
            await htmlDocumentWriter.WriteToOutputStreamAsync(outputStream, Convertables);
        }
    }
}
