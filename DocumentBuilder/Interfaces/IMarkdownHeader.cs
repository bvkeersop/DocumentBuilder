using DocumentBuilder.Options;

namespace DocumentBuilder.Interfaces
{
    internal interface IMarkdownHeader
    {
        public ValueTask<string> ToMarkdownTableOfContentsEntry(bool isNumbered, MarkdownDocumentOptions options);
    }
}
