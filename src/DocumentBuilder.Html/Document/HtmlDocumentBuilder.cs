using DocumentBuilder.Core.Utilities;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Factories;
using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Exceptions;
using DocumentBuilder.Html.Model;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Html.Streams;

namespace DocumentBuilder.Html.Document;

public class HtmlDocumentBuilder : IHtmlDocumentBuilder
{
    public IHtmlDocumentWriter HtmlDocumentWriter { get; }
    public HtmlDocumentOptions Options { get; }
    public IEnumerableRenderingStrategy EnumerableRenderingStrategy { get; }
    public HtmlDocument HtmlDocument { get; }

    public HtmlDocumentBuilder() : this(new HtmlDocumentOptions()) { }

    public HtmlDocumentBuilder(HtmlDocumentOptions options)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        EnumerableRenderingStrategy = EnumerableValidatorFactory.Create(options.NullOrEmptyEnumerableRenderingStrategy);
        HtmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, options);
        HtmlDocument = new HtmlDocument(options, HtmlDocumentWriter);
    }

    public IHtmlDocumentHeaderBuilder Head(Action<IHtmlDocumentHeaderBuilder> executeBuildSteps)
    {
        var builder = new HtmlDocumentHeaderBuilder(Options);
        executeBuildSteps(builder);
        var head = builder.Build();
        HtmlDocument._htmlDocumentHeader = head;
    }

    public IHtmlDocumentHeaderBuilder Body(Action<IHtmlDocumentBodyBuilder> executeBuildSteps)
    {
        var builder = new HtmlDocumentHeaderBuilder(Options);
        executeBuildSteps(builder);
        var head = builder.Build();
        HtmlDocument._htmlDocumentHeader = head;
    }
}
