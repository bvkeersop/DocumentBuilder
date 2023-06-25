using DocumentBuilder.Constants;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Html;
using DocumentBuilder.Options;

namespace DocumentBuilder.Html;

public interface IHtmlDocumentWriter
{
    Task WriteToStreamAsync(Stream outputStream, IEnumerable<IHtmlElement> htmlConvertables, IEnumerable<Link> links);
}

internal class HtmlDocumentWriter
{
    private readonly Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> _htmlStreamWriterFactory;
    private readonly HtmlDocumentOptions _options;

    public HtmlDocumentWriter(Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> htmlStreamWriterFactory, HtmlDocumentOptions options)
    {
        _htmlStreamWriterFactory = htmlStreamWriterFactory;
        _options = options;
    }

    public async Task WriteToStreamAsync(Stream outputStream, IEnumerable<IHtmlElement> htmlConvertables, IEnumerable<Link> links)
    {
        using var htmlStreamWriter = _htmlStreamWriterFactory(outputStream, _options);

        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.DocType).ConfigureAwait(false);
        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag()).ConfigureAwait(false);

        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Head.ToHtmlStartTag()).ConfigureAwait(false);
        foreach (var link in links)
        {
            var linkAsHtmlElement = await link.ToHtmlAsync(_options);
            await htmlStreamWriter.WriteLineAsync(linkAsHtmlElement).ConfigureAwait(false);
        }

        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Head.ToHtmlEndTag()).ConfigureAwait(false);

        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1).ConfigureAwait(false);

        for (var i = 0; i < htmlConvertables.Count(); i++)
        {
            var html = await htmlConvertables.ElementAt(i).ToHtmlAsync(_options, 2);
            await htmlStreamWriter.WriteAsync(html).ConfigureAwait(false);
        }

        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1).ConfigureAwait(false);
        await htmlStreamWriter.WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag()).ConfigureAwait(false);

        await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
    }
}
