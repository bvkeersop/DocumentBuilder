using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

public class HtmlDocument
{
    private readonly IHtmlDocumentWriter _htmlDocumentWriter;
    private readonly HtmlDocumentOptions _options;

    public IList<Link> Links { get; } = new List<Link>();
    public IList<IHtmlElement> Elements { get; } = new List<IHtmlElement>();

    public HtmlDocument(HtmlDocumentOptions options, IHtmlDocumentWriter htmlDocumentWriter)
    {
        _options = options;
        _htmlDocumentWriter = htmlDocumentWriter;
    }

    public void AddHtmlElement(IHtmlElement element) => Elements.Add(element);
    public void AddLink(Link link) => Links.Add(link);

    /// <summary>
    /// Writes the document to the provided output stream
    /// </summary>
    /// <param name="outputStream">The stream to write to</param>
    /// <returns><see cref="Task"/></returns>
    public async Task SaveAsync(Stream outputStream)
    {
        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        await _htmlDocumentWriter.WriteToStreamAsync(outputStream, this).ConfigureAwait(false);
    }

    /// <summary>
    /// Writes the document to the provided path, will replace existing documents
    /// </summary>
    /// <param name="filePath">The path which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public Task SaveAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("filePath cannot be null or empty");
        }

        return BuildInternalAsync(filePath);
    }

    private async Task<FileStream> BuildInternalAsync(string filePath)
    {
        FileStream fileStream = File.Create(filePath);
        await SaveAsync(fileStream).ConfigureAwait(false);
        return fileStream;
    }
}
