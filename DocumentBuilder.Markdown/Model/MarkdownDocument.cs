using DocumentBuilder.Utilities;

namespace DocumentBuilder.Markdown.Model;

public class MarkdownDocument
{
    public IList<IMarkdownElement> Elements { get; private set; }

    public MarkdownDocument(
        INewLineProvider newLineProvider, 
        IIndentationProvider indentationProvider)
    {
        Elements = new List<IMarkdownElement>();
    }

    public void AddElement(IMarkdownElement element) => Elements.Add(element);

    /// <summary>
    /// Writes the document to the provided stream
    /// </summary>
    /// <param name="filePath">The stream which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public Task SaveAsync(Stream outputStream)
    {
        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        return BuildInternalAsync(outputStream);
    }

    /// <summary>
    /// Writes the document to the provided path
    /// </summary>
    /// <param name="filePath">The path which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public async Task SaveAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("filePath cannot be null or empty");
        }

        using FileStream fileStream = File.Create(filePath);
        await SaveAsync(fileStream);
    }

    private async Task BuildInternalAsync(Stream outputStream)
    {
        var markdownDocumentWriter = new MarkdownDocumentWriter()
        await markdownDocumentWriter.WriteToStreamAsync(outputStream, Elements).ConfigureAwait(false);
    }
}
