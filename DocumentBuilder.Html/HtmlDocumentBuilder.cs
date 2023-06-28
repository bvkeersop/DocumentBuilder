using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Validators;
using DocumentBuilder.Model.Shared;
using DocumentBuilder.Model.Html;
using DocumentBuilder.Extensions;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Html;
using DocumentBuilder.Core.Utilities;
using DocumentBuilder.Html.Model;
using DocumentBuilder.Html.Options;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentBuilder.DocumentBuilders
{
    public class HtmlDocumentBuilder : IHtmlDocumentBuilder
    {
        public IHtmlDocumentWriter HtmlDocumentWriter { get; }
        public IEnumerableRenderingStrategy EnumerableRenderingStrategy { get; }
        public HtmlDocument HtmlDocument { get; }

        protected HtmlDocumentBuilder(HtmlDocumentBuilder htmlDocumentBuilder)
        {
            HtmlDocument = htmlDocumentBuilder.HtmlDocument;
            HtmlDocumentWriter = htmlDocumentBuilder.HtmlDocumentWriter;
            EnumerableRenderingStrategy = htmlDocumentBuilder.EnumerableRenderingStrategy;
        }

        public HtmlDocumentBuilder() : this(new HtmlDocumentOptions()) { }

        public HtmlDocumentBuilder(HtmlDocumentOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            EnumerableRenderingStrategy = EnumerableValidatorFactory.Create(options.NullOrEmptyEnumerableRenderingStrategy);
            HtmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
            HtmlDocument = new HtmlDocument(options, HtmlDocumentWriter);
        }

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddHeader1(string header1)
        {
            _ = header1 ?? throw new ArgumentNullException(nameof(header1));
            var htmlElement = new Header1(header1);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddHeader2(string header2)
        {
            _ = header2 ?? throw new ArgumentNullException(nameof(header2));
            var htmlElement = new Header2(header2);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddHeader3(string header3)
        {
            _ = header3 ?? throw new ArgumentNullException(nameof(header3));
            var htmlElement = new Header3(header3);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></retu
        public IHtmlElementBuilder AddHeader4(string header4)
        {
            _ = header4 ?? throw new ArgumentNullException(nameof(header4));
            var htmlElement = new Header3(header4);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddParagraph(string paragraph)
        {
            _ = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
            var htmlElement = new Paragraph(paragraph);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            _ = orderedList ?? throw new ArgumentNullException(nameof(orderedList));

            if (!EnumerableRenderingStrategy.ShouldRender(orderedList))
            {
                return new HtmlElementBuilder(null, this);
            }

            var htmlElement = new OrderedList<T>(orderedList);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds an unordered list to the document
        /// </summary>
        /// <param name="unorderedList">The value of the unordered list</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            _ = unorderedList ?? throw new ArgumentNullException(nameof(unorderedList));

            if (!EnumerableRenderingStrategy.ShouldRender(unorderedList))
            {
                return new HtmlElementBuilder(null, this);
            }

            var htmlElement = new OrderedList<T>(unorderedList);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            _ = tableRows ?? throw new ArgumentNullException(nameof(tableRows));

            if (!EnumerableRenderingStrategy.ShouldRender(tableRows))
            {
                return new HtmlElementBuilder(null, this);
            }

            var htmlElement = new OrderedList<T>(tableRows);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddImage(string name, string path, string? caption = null)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = path ?? throw new ArgumentNullException(nameof(path));

            var htmlElement = new Image(name, path, caption);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds the provided content directly into the document
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlElementBuilder AddRaw(string content)
        {
            _ = content ?? throw new ArgumentNullException(nameof(content));

            var htmlElement = new Raw(content);
            HtmlDocument.AddHtmlElement(htmlElement);
            return new HtmlElementBuilder(htmlElement, this);
        }

        /// <summary>
        /// Adds a stylesheet by reference
        /// </summary>
        /// <param name="href">The file path of the style sheet</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddStylesheetByRef(string href, string type = "text/css")
        {
            var link = new Link(Links.Stylesheet, href, type);
            HtmlDocument.AddLink(link);
            return this;
        }
    }
}
