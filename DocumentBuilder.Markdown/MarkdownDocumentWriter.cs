using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown;

public interface IMarkdownDocumentWriter
{
    Task WriteToStreamAsync(Stream outputStream, IEnumerable<IMarkdownElement> markdownConvertables, MarkdownDocumentOptions options);
}

internal class MarkdownDocumentWriter : IMarkdownDocumentWriter, IDisposable
{
    private bool _disposedValue;
    private readonly IMarkdownStreamWriter _markdownStreamWriter;

    public MarkdownDocumentWriter(IMarkdownStreamWriter markdownStreamWriter)
    {
        _markdownStreamWriter = markdownStreamWriter;
    }

    public async Task WriteToStreamAsync(
        Stream outputStream,
        IEnumerable<IMarkdownElement> markdownConvertables,
        MarkdownDocumentOptions options)
    {
        for (var i = 0; i < markdownConvertables.Count(); i++)
        {
            var markdown = markdownConvertables.ElementAt(i).ToMarkdown(options);

            if (i < markdownConvertables.Count() - 1)
            {
                await _markdownStreamWriter.WriteLineAsync(markdown).ConfigureAwait(false);
                await _markdownStreamWriter.WriteNewLineAsync().ConfigureAwait(false);
                continue;
            }

            await _markdownStreamWriter.WriteAsync(markdown).ConfigureAwait(false);
        }

        await _markdownStreamWriter.FlushAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _markdownStreamWriter.Dispose();
            }

            _disposedValue = true;
        }
    }

    ~MarkdownDocumentWriter()
    {
        Dispose(disposing: false);
    }
}