using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;
using DocumentBuilder.Model;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Validators;
using DocumentBuilder.Model.Shared;
using DocumentBuilder.Model.Html;
using DocumentBuilder.Extensions;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Html;

namespace DocumentBuilder.DocumentBuilders
{
    public class HtmlDocumentBuilder : IHtmlDocumentBuilder
    {
        public IEnumerable<Link> Links { get; private set; } = new List<Link>();
        public IEnumerable<IHtmlElement> HtmlConvertables { get; private set; } = new List<IHtmlElement>();
        private readonly HtmlDocumentWriter _htmlDocumentWriter;
        private readonly IEnumerableValidator _enumerableValidator;

        public HtmlDocumentBuilder() : this(new HtmlDocumentOptions()) { }

        public HtmlDocumentBuilder(HtmlDocumentOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            _enumerableValidator = EnumerableValidatorFactory.Create(options.BehaviorOnEmptyEnumerable);
            _htmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
        }

        /// <summary>
        /// Adds a class attribute to the current html element
        /// </summary>
        /// <param name="class">The class to add</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder WithClass(string @class)
        {
            if (HtmlConvertables.IsNullOrEmpty()) throw new DocumentBuilderException(DocumentBuilderErrorCode.NoHtmlElementAdded,
                $"Trying to add class '{@class}' to an html element, but no element has been added yet");
            HtmlConvertables.Last().Attributes.Add(Model.Html.Attributes.Class, @class);
            return this;
        }

        /// <summary>
        /// Adds an id attribute to the current html element
        /// </summary>
        /// <param name="id">The unique id to add</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder WithId(string id)
        {
            if (HtmlConvertables.IsNullOrEmpty()) throw new DocumentBuilderException(DocumentBuilderErrorCode.NoHtmlElementAdded,
                $"Trying to add id '{id}' to an html element, but no element has been added yet");
            HtmlConvertables.Last().Attributes.Add(Model.Html.Attributes.Id, id);
            return this;
        }

        /// <summary>
        /// Adds an inline style to the current html element
        /// </summary>
        /// <param name="style">The inline style to add</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder WithStyle(string style)
        {
            if (HtmlConvertables.IsNullOrEmpty()) throw new DocumentBuilderException(DocumentBuilderErrorCode.NoHtmlElementAdded,
                $"Trying to add style '{style}' to an html element, but no element has been added yet");
            HtmlConvertables.Last().Attributes.Add(Model.Html.Attributes.Style, style);
            return this;
        }

        /// <summary>
        /// Adds an attribute to the current html element
        /// </summary>
        /// <param name="key">The name of the attribute to add</param>
        /// <param name="value">The value of the attribute to add</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder WithAttribute(string key, string value)
        {
            if (HtmlConvertables.IsNullOrEmpty()) throw new DocumentBuilderException(DocumentBuilderErrorCode.NoHtmlElementAdded,
                $"Trying to add attribute '{key}' with value '{value}' to an html element, but no element has been added yet");
            HtmlConvertables.Last().Attributes.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds a header of type 1 to the document
        /// </summary>
        /// <param name="header1">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddHeader1(string header1)
        {
            _ = header1 ?? throw new ArgumentNullException(nameof(header1));
            HtmlConvertables = HtmlConvertables.Append(new Header1(header1));
            return this;
        }

        /// <summary>
        /// Adds a header of type 2 to the document
        /// </summary>
        /// <param name="header2">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddHeader2(string header2)
        {
            _ = header2 ?? throw new ArgumentNullException(nameof(header2));
            HtmlConvertables = HtmlConvertables.Append(new Header2(header2));
            return this;
        }

        /// <summary>
        /// Adds a header of type 3 to the document
        /// </summary>
        /// <param name="header3">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddHeader3(string header3)
        {
            _ = header3 ?? throw new ArgumentNullException(nameof(header3));
            HtmlConvertables = HtmlConvertables.Append(new Header3(header3));
            return this;
        }

        /// <summary>
        /// Adds a header of type 4 to the document
        /// </summary>
        /// <param name="header4">The value of the header</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></retu
        public IHtmlDocumentBuilder AddHeader4(string header4)
        {
            _ = header4 ?? throw new ArgumentNullException(nameof(header4));
            HtmlConvertables = HtmlConvertables.Append(new Header4(header4));
            return this;
        }

        /// <summary>
        /// Adds a paragraph to the document
        /// </summary>
        /// <param name="paragraph">The value of the paragraph</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddParagraph(string paragraph)
        {
            _ = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
            HtmlConvertables = HtmlConvertables.Append(new Paragraph(paragraph));
            return this;
        }

        /// <summary>
        /// Adds an ordered list to the document
        /// </summary>
        /// <param name="orderedList">The value of the ordered list</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddOrderedList<T>(IEnumerable<T> orderedList)
        {
            _ = orderedList ?? throw new ArgumentNullException(nameof(orderedList));

            if (!_enumerableValidator.ShouldRender(orderedList))
            {
                return this;
            }

            HtmlConvertables = HtmlConvertables.Append(new OrderedList<T>(orderedList));
            return this;
        }

        /// <summary>
        /// Adds an unordered list to the document
        /// </summary>
        /// <param name="unorderedList">The value of the unordered list</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedList)
        {
            _ = unorderedList ?? throw new ArgumentNullException(nameof(unorderedList));

            if (!_enumerableValidator.ShouldRender(unorderedList))
            {
                return this;
            }

            HtmlConvertables = HtmlConvertables.Append(new UnorderedList<T>(unorderedList));
            return this;
        }

        /// <summary>
        /// Adds a table to the document
        /// </summary>
        /// <typeparam name="TRow">The type of the row</typeparam>
        /// <param name="tableRows">The values of the table rows</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddTable<T>(IEnumerable<T> tableRows)
        {
            _ = tableRows ?? throw new ArgumentNullException(nameof(tableRows));

            if (!_enumerableValidator.ShouldRender(tableRows))
            {
                return this;
            }

            HtmlConvertables = HtmlConvertables.Append(new Table<T>(tableRows));
            return this;
        }

        /// <summary>
        /// Adds an image to the document
        /// </summary>
        /// <param name="name">The name of the image</param>
        /// <param name="path">The path to the image</param>
        /// <param name="caption">The caption of the image</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddImage(string name, string path, string? caption = null)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = path ?? throw new ArgumentNullException(nameof(path));
            HtmlConvertables = HtmlConvertables.Append(new Image(name, path, caption));
            return this;
        }

        /// <summary>
        /// Adds the provided content directly into the document
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddRaw(string content)
        {
            _ = content ?? throw new ArgumentNullException(nameof(content));
            HtmlConvertables = HtmlConvertables.Append(new Raw(content));
            return this;
        }

        /// <summary>
        /// Adds a stylesheet by reference
        /// </summary>
        /// <param name="href">The file path of the style sheet</param>
        /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
        public IHtmlDocumentBuilder AddStylesheetByRef(string href, string type = "text/css")
        {
            var link = new Link("stylesheet", href, type);
            Links = Links.Append(link);
            return this;
        }
    }
}
