using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Model;
using DocumentBuilder.Html.Model.Body;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Html.Streams;

namespace DocumentBuilder.Html.Document;

public interface IHtmlDocumentWriter
{
    Task WriteToStreamAsync(Stream outputStream, HtmlDocument htmlDocument);
}

internal class HtmlDocumentWriter : IHtmlDocumentWriter
{
    private readonly Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> _htmlStreamWriterFactory;
    private readonly HtmlDocumentOptions _options;

    public HtmlDocumentWriter(Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> htmlStreamWriterFactory, HtmlDocumentOptions options)
    {
        _htmlStreamWriterFactory = htmlStreamWriterFactory;
        _options = options;
    }

    public async Task WriteToStreamAsync(Stream outputStream, HtmlDocument htmlDocument)
    {
        using var htmlStreamWriter = _htmlStreamWriterFactory(outputStream, _options);

        await htmlStreamWriter.WriteLineAsync(Indicators.DocType).ConfigureAwait(false);
        await htmlStreamWriter.WriteLineAsync(Indicators.Html.ToHtmlStartTag()).ConfigureAwait(false);

        await WriteHeadElementAsync(htmlDocument.Header, htmlStreamWriter).ConfigureAwait(false);
        await WriteBodyElementAsync(htmlDocument, htmlStreamWriter).ConfigureAwait(false);

        await htmlStreamWriter.WriteLineAsync(Indicators.Html.ToHtmlEndTag()).ConfigureAwait(false);

        await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
    }

    private async Task WriteBodyElementAsync(HtmlDocument htmlDocument, IHtmlStreamWriter htmlStreamWriter)
    {
        var currentIndentationLevel = 1;
        await htmlStreamWriter.WriteLineAsync(Indicators.Body.ToHtmlStartTag(), currentIndentationLevel).ConfigureAwait(false);

        currentIndentationLevel++;
        foreach (var element in htmlDocument.Elements)
        {
            if (IsDivEnd(element))
            {
                currentIndentationLevel--;
            }

            var html = element.ToHtml(_options, currentIndentationLevel);
            await htmlStreamWriter.WriteLineAsync(html).ConfigureAwait(false);

            if (IsDivStart(element))
            {
                currentIndentationLevel++;
            }
        }

        await htmlStreamWriter.WriteLineAsync(Indicators.Body.ToHtmlEndTag(), 1).ConfigureAwait(false);
    }

    private static async Task WriteHeadElementAsync(HtmlDocumentHeader header, IHtmlStreamWriter htmlStreamWriter)
    {
        await htmlStreamWriter.WriteLineAsync(Indicators.Head.ToHtmlStartTag()).ConfigureAwait(false);
        await htmlStreamWriter.WriteAsync(Indicators.Title.ToHtmlStartTag(), 1).ConfigureAwait(false);
        await htmlStreamWriter.WriteAsync(Indicators.Title.ToHtmlEndTag()).ConfigureAwait(false);
        await htmlStreamWriter.WriteNewLineAsync().ConfigureAwait(false);

        foreach (var element in header.Elements)
        {
            var html = element.ToHtml();
            await htmlStreamWriter.WriteLineAsync(html).ConfigureAwait(false);
        }

        await htmlStreamWriter.WriteLineAsync(Indicators.Head.ToHtmlEndTag()).ConfigureAwait(false);
    }

    private static bool IsDivStart(IHtmlElement htmlElement) => htmlElement is DivStart;
    private static bool IsDivEnd(IHtmlElement htmlElement) => htmlElement is DivEnd;
}
