namespace NDocument.Domain.Options
{
    public class MarkdownDocumentOptions : DocumentOptions
    {
        /// <summary>
        /// Options related to table formatting
        /// </summary>
        public MarkdownTableOptions MarkdownTableOptions { get; set; } = new MarkdownTableOptions();
    }
}