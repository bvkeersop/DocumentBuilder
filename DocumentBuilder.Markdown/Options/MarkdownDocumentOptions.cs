using DocumentBuilder.Factories;
using DocumentBuilder.Shared.Enumerations;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Markdown.Options;

public class MarkdownDocumentOptions
{
    /// <summary>
    /// Options related to table formatting
    /// </summary>
    public MarkdownTableOptions MarkdownTableOptions { get; set; } = new MarkdownTableOptions();

    /// <summary>
    /// How an list or table will be rendered when it's empty
    /// </summary>
    public NullOrEmptyEnumerableRenderingStrategy NullOrEmptyEnumerableRenderingStrategy { get; set; }

    /// <summary>
    /// A provider for the new line character
    /// </summary>
    public INewLineProvider NewLineProvider { get; set; } = NewLineProviderFactory.Create(LineEndings.Environment);

    /// <summary>
    /// A provider for indentation
    /// </summary>
    public IIndentationProvider IndentationProvider { get; set; } = IndentationProviderFactory.Create(IndentationType.Spaces, 4);
}