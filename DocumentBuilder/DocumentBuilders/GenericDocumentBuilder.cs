using DocumentBuilder.Enumerations;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Options;
using DocumentBuilder.DocumentWriters;
using DocumentBuilder.Model;
using DocumentBuilder.Validators;
using DocumentBuilder.Interfaces;

namespace DocumentBuilder.DocumentBuilders
{
    public class GenericDocumentBuilder : IGenericDocumentBuilder
    {
        public IEnumerable<GenericElement> Convertables { get; private set; } = new List<GenericElement>();
        private readonly GenericDocumentOptions _options;
        private readonly IEnumerableValidator _enumerableValidator;

        public GenericDocumentBuilder() : this(new GenericDocumentOptions()) { }

        public GenericDocumentBuilder(GenericDocumentOptions options)
        {
            _options = options;
            _enumerableValidator = EnumerableValidatorFactory.Create(options.BehaviorOnEmptyEnumerable);
        }

        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The stream to write to</param>
        /// <param name="documentType">In what format the document should be written</param>
        /// <returns><see cref="Task"/></returns>
        public async Task BuildAsync(Stream outputStream, DocumentType documentType)
        {
            _options.DocumentType = documentType;
            switch (_options.DocumentType)
            {
                case DocumentType.Markdown:
                    await WriteToStreamAsMarkdownAsync(outputStream).ConfigureAwait(false);
                    break;
                case DocumentType.Html:
                    await WriteToStreamAsHtmlAsync(outputStream).ConfigureAwait(false);
                    break;
                default:
                    throw new NotSupportedException($"{_options.DocumentType} is not supported");
            }
        }

        /// <summary>
        /// Writes the document to the provided path, will replace existing documents
        /// </summary>
        /// <param name="filePath">The path which the file should be written to</param>
        /// <param name="documentType">In what format the document should be written</param>
        /// <returns><see cref="Task"/></returns>
        public async Task BuildAsync(string filePath, DocumentType documentType)
        {
            using FileStream fileStream = File.Create(filePath);
            await BuildAsync(fileStream, documentType);
        }

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddHeader1(string header1)
        {
            Convertables = Convertables.Append(new Header1(header1));
            return this;
        }

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddHeader2(string header2)
        {
            Convertables = Convertables.Append(new Header2(header2));
            return this;
        }

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddHeader3(string header3)
        {
            Convertables = Convertables.Append(new Header3(header3));
            return this;
        }

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddHeader4(string header4)
        {
            Convertables = Convertables.Append(new Header4(header4));
            return this;
        }

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddParagraph(string paragraph)
        {
            Convertables = Convertables.Append(new Paragraph(paragraph));
            return this;
        }

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            if (!_enumerableValidator.ShouldRender(orderedList))
            {
                return this;
            }

            Convertables = Convertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        /// <summary>
        /// Adds an unordered list to the document
        /// </summary>
        /// <param name="unorderedList">The value of the unordered list</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            if (!_enumerableValidator.ShouldRender(unorderedList))
            {
                return this;
            }

            Convertables = Convertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            if (!_enumerableValidator.ShouldRender(tableRows))
            {
                return this;
            }

            Convertables = Convertables.Append(new Table<T>(tableRows));
            return this;
        }

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IGenericDocumentBuilder"/></returns>
        public IGenericDocumentBuilder AddImage(string name, string path, string? caption = null)
        {
            Convertables = Convertables.Append(new Image(name, path, caption));
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
