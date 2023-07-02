using DocumentBuilder.Factories;
using DocumentBuilder.Markdown.Model;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Core.Utilities;

namespace DocumentBuilder.Markdown.Document
{
    public class MarkdownDocumentBuilder : IMarkdownDocumentBuilder
    {
        public MarkdownDocumentOptions Options { get; }
        private readonly MarkdownDocument _markdownDocument;
        private readonly IEnumerableRenderingStrategy _enumerableValidator;

        public MarkdownDocumentBuilder() : this(new MarkdownDocumentOptions()) { }

        public MarkdownDocumentBuilder(MarkdownDocumentOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            _enumerableValidator = EnumerableValidatorFactory.Create(options.NullOrEmptyEnumerableRenderingStrategy);
            _markdownDocument = new MarkdownDocument(options);
        }

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddHeader1(string header1)
        {
            _ = header1 ?? throw new ArgumentNullException(nameof(header1));
            _markdownDocument.AddElement(new Header1(header1));
            return this;
        }

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddHeader2(string header2)
        {
            _ = header2 ?? throw new ArgumentNullException(nameof(header2));
            _markdownDocument.AddElement(new Header2(header2));
            return this;
        }

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddHeader3(string header3)
        {
            _ = header3 ?? throw new ArgumentNullException(nameof(header3));
            _markdownDocument.AddElement(new Header3(header3));
            return this;
        }

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddHeader4(string header4)
        {
            _ = header4 ?? throw new ArgumentNullException(nameof(header4));
            _markdownDocument.AddElement(new Header4(header4));
            return this;
        }

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddParagraph(string paragraph)
        {
            _ = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
            _markdownDocument.AddElement(new Paragraph(paragraph));
            return this;
        }

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            _ = orderedList ?? throw new ArgumentNullException(nameof(orderedList));

            if (!_enumerableValidator.ShouldRender(orderedList))
            {
                return this;
            }

            _markdownDocument.AddElement(new OrderedList<T>(orderedList));
            return this;
        }

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            _ = unorderedList ?? throw new ArgumentNullException(nameof(unorderedList));

            if (!_enumerableValidator.ShouldRender(unorderedList))
            {
                return this;
            }

            _markdownDocument.AddElement(new UnorderedList<T>(unorderedList));
            return this;
        }

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddTable<T>(T tableRow)
        {
            _ = tableRow ?? throw new ArgumentNullException(nameof(tableRow));
            var tableRows = new T[] { tableRow };
            return AddTableInternal(tableRows);
        }

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            _ = tableRows ?? throw new ArgumentNullException(nameof(tableRows));
            return AddTableInternal(tableRows);
        }

        private IMarkdownDocumentBuilder AddTableInternal<T>(IEnumerable<T> tableRows)
        {
            if (!_enumerableValidator.ShouldRender(tableRows))
            {
                return this;
            }

            _markdownDocument.AddElement(new Table<T>(tableRows, Options));
            return this;
        }

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddImage(string name, string path, string? caption = null)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _markdownDocument.AddElement(new Image(name, path, caption));
            return this;
        }

        /// <summary>
        /// Adds a blockquote to the document
        /// </summary>
        /// <param name="quote">The quote</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddBlockquote(string quote)
        {
            _ = quote ?? throw new ArgumentNullException(nameof(quote));
            _markdownDocument.AddElement(new Blockquote(quote));
            return this;
        }

        /// <summary>
        /// Adds the provided content directly into the document
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddRaw(string content)
        {
            _ = content ?? throw new ArgumentNullException(nameof(content));
            _markdownDocument.AddElement(new Raw(content));
            return this;
        }

        /// <summary>
        /// Adds a codeblock to the document
        /// </summary>
        /// <param name="code">The codeblock</param>
        /// <param name="language">The programming language the codeblock is written in</param>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddFencedCodeblock(string code, string? language = null)
        {
            _ = code ?? throw new ArgumentNullException(nameof(code));
            _ = language ?? throw new ArgumentNullException(nameof(language));
            _markdownDocument.AddElement(new FencedCodeblock(code, language));
            return this;
        }

        /// <summary>
        /// Adds an horizontal rule
        /// </summary>
        /// <returns><see cref="IMarkdownDocumentBuilder"/></returns>
        public IMarkdownDocumentBuilder AddHorizontalRule()
        {
            _markdownDocument.AddElement(new HorizontalRule());
            return this;
        }

        /// <summary>
        /// Builds the markdown document
        /// </summary>
        /// <returns>The <see cref="MarkdownDocument"/></returns>
        public MarkdownDocument Build() => _markdownDocument;
    }
}
