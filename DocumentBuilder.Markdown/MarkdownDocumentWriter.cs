using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Markdown;

public interface IMarkdownDocumentWriter
{
    Task WriteToStreamAsync(Stream outputStream, IEnumerable<IMarkdownElement> markdownConvertables);
}

internal class MarkdownDocumentWriter : IMarkdownDocumentWriter, IDisposable
{
    private readonly IMarkdownStreamWriter _markdownStreamWriter;

    public MarkdownDocumentWriter(IMarkdownStreamWriter markdownStreamWriter)
    {
        _markdownStreamWriter = markdownStreamWriter;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task WriteToStreamAsync(Stream outputStream, IEnumerable<IMarkdownElement> markdownConvertables)
    {
        for (var i = 0; i < markdownConvertables.Count(); i++)
        {
            var markdown = await markdownConvertables.ElementAt(i).ToMarkdownAsync();

            if (i < markdownConvertables.Count() - 1)
            {
                await _markdownStreamWriter.WriteLineAsync(markdown).ConfigureAwait(false);
                continue;
            }

            await _markdownStreamWriter.WriteAsync(markdown).ConfigureAwait(false);
        }

        await _markdownStreamWriter.FlushAsync().ConfigureAwait(false);
    }
}