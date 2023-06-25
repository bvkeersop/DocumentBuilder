using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

public class MarkdownDocument
{
    private readonly MarkdownDocumentOptions _options;

    public IList<IMarkdownElement> Elements { get; private set; }

    public MarkdownDocument(MarkdownDocumentOptions options)
    {
        Elements = new List<IMarkdownElement>();
        _options = options;
    }

    public void AddElement(IMarkdownElement element) => Elements.Add(element);

    /// <summary>
    /// Writes the document to the provided stream
    /// </summary>
    /// <param name="filePath">The stream which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public Task SaveAsync(Stream outputStream, MarkdownDocumentOptions? options = null)
    {
        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        return SaveInternalAsync(outputStream);
    }

    /// <summary>
    /// Writes the document to the provided path
    /// </summary>
    /// <param name="filePath">The path which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public async Task SaveAsync(string filePath, MarkdownDocumentOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("filePath cannot be null or empty");
        }

        using FileStream fileStream = File.Create(filePath);
        await SaveAsync(fileStream);
    }

    private async Task SaveInternalAsync(Stream outputStream)
    {
        var markdownStreamWriter = MarkdownStreamWriterFactory.Create(outputStream, _options.NewLineProvider);
        var markdownDocumentWriter = new MarkdownDocumentWriter(markdownStreamWriter);
        await markdownDocumentWriter.WriteToStreamAsync(outputStream, Elements).ConfigureAwait(false);
    }
}
