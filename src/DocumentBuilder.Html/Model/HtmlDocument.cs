using DocumentBuilder.Html.Document;
using DocumentBuilder.Html.Model.Body;
using DocumentBuilder.Html.Model.Head;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Html.Streams;

namespace DocumentBuilder.Html.Model;

public class HtmlDocument
{
    private HtmlDocumentHeader _htmlDocumentHeader = new();
    private HtmlDocumentBody _htmlDocumentBody = new();

    private readonly Action<IHtmlDocumentBuilder> _executeBuildSteps;
    private readonly HtmlDocumentOptions _options = new();
    private readonly IHtmlDocumentWriter _htmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, new());

    public static HtmlDocument Build(
        Action<IHtmlDocumentBuilder> executeBuildSteps,
        HtmlDocumentOptions? options = null,
        IHtmlDocumentWriter? htmlDocumentWriter = null) => new(executeBuildSteps, options, htmlDocumentWriter);

    protected HtmlDocument(
        Action<IHtmlDocumentBuilder> executeBuildSteps, 
        HtmlDocumentOptions? options = null, 
        IHtmlDocumentWriter? htmlDocumentWriter = null)
    {
        _executeBuildSteps = executeBuildSteps;

        if (options != null)
        {
            _options = options;
        }

        if (_htmlDocumentWriter != null)
        {
            _htmlDocumentWriter = htmlDocumentWriter; // Is not null here
            return;
        }
        
        _htmlDocumentWriter = new HtmlDocumentWriter(HtmlStreamWriterFactory.Create, _options);
    }

    /// <summary>
    /// Writes the document to the provided output stream
    /// </summary>
    /// <param name="outputStream">The stream to write to</param>
    /// <returns><see cref="Task"/></returns>
    public async Task SaveAsync(Stream outputStream)
    {
        var headerBuilder = new HtmlDocumentHeaderBuilder(_options);


        var documentBuilder = new HtmlDocumentBuilder();
        _executeBuildSteps(documentBuilder);
        var htmlDocument = documentBuilder.Build();

        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        await _htmlDocumentWriter.WriteToStreamAsync(outputStream, htmlDocument).ConfigureAwait(false);
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
