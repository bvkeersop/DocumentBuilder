using DocumentBuilder.Shared.Enumerations;

namespace DocumentBuilder.Markdown.Options;

public class MarkdownDocumentBuilderOptions
{
    /// <summary>
    /// Options related to table formatting
    /// </summary>
    public MarkdownTableOptions MarkdownTableOptions { get; set; } = new MarkdownTableOptions();

    /// <summary>
    /// How an list or table will be rendered when it's empty
    /// </summary>
    public NullOrEmptyEnumerableRenderingStrategy NullOrEmptyEnumerableRenderingStrategy { get; set; }
}