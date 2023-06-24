using DocumentBuilder.Shared.Enumerations;

namespace DocumentBuilder.Markdown.Options;

public class MarkdownTableOptions
{
    /// <summary>
    /// The formatting which to render the table with
    /// </summary>
    public Formatting Formatting { get; set; } = Formatting.AlignColumns;

    /// <summary>
    /// Wheter the column names should be bold or not
    /// </summary>
    public bool BoldColumnNames { get; set; } = false;

    /// <summary>
    /// What the default aligment is
    /// </summary>
    public Alignment DefaultAlignment { get; set; } = Alignment.None;
}
