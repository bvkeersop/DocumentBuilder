using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Model.Body;

namespace DocumentBuilder.Html.Document;

public interface IHtmlElementBuilder : IHtmlDocumentBuilder
{
    /// <summary>
    /// Adds a class attribute to the current html element
    /// </summary>
    /// <param name="class">The class to add</param>
    /// <returns><see cref="IHtmlElementBuilder"/></returns>
    IHtmlElementBuilder WithClass(string @class);

    /// <summary>
    /// Adds an id attribute to the current html element
    /// </summary>
    /// <param name="id">The unique id to add</param>
    /// <returns><see cref="IHtmlElementBuilder"/></returns>
    IHtmlElementBuilder WithId(string id);

    /// <summary>
    /// Adds an inline style to the current html element
    /// </summary>
    /// <param name="style">The inline style to add</param>
    /// <returns><see cref="IHtmlElementBuilder"/></returns>
    IHtmlElementBuilder WithStyle(string style);

    /// <summary>
    /// Adds an attribute to the current html element
    /// </summary>
    /// <param name="key">The name of the attribute to add</param>
    /// <param name="value">The value of the attribute to add</param>
    /// <returns><see cref="IHtmlElementBuilder"/></returns>
    IHtmlElementBuilder WithAttribute(string key, string value);
}

public class HtmlElementBuilder : HtmlDocumentBuilder, IHtmlElementBuilder
{
    // Can be null in case the rendering strategy is set to NullOrEmptyEnumerableRenderingStrategy.SkipRender
    public IHtmlElement? HtmlElement { get; }

    public HtmlElementBuilder(
        IHtmlElement? htmlElement,
        HtmlDocumentBuilder htmlDocumentBuilder) : base(htmlDocumentBuilder)
    {
        HtmlElement = htmlElement;
    }

    /// <summary>
    /// Adds a class attribute to the current html element
    /// </summary>
    /// <param name="class">The class to add</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder WithClass(string @class)
    {
        HtmlElement?.Attributes.Add(AttributeNames.Class, @class);
        return this;
    }

    /// <summary>
    /// Adds an id attribute to the current html element
    /// </summary>
    /// <param name="id">The unique id to add</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder WithId(string id)
    {
        HtmlElement?.Attributes.Add(AttributeNames.Id, id);
        return this;
    }

    /// <summary>
    /// Adds an inline style to the current html element
    /// </summary>
    /// <param name="style">The inline style to add</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder WithStyle(string style)
    {
        HtmlElement?.InlineStyles.SetValue(style);
        return this;
    }

    /// <summary>
    /// Adds an attribute to the current html element
    /// </summary>
    /// <param name="key">The name of the attribute to add</param>
    /// <param name="value">The value of the attribute to add</param>
    /// <returns><see cref="IHtmlDocumentBuilder"/></returns>
    public IHtmlElementBuilder WithAttribute(string key, string value)
    {
        HtmlElement?.Attributes.Add(key, value);
        return this;
    }
}
