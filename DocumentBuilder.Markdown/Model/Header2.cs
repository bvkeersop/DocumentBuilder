using DocumentBuilder.Constants;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;
internal class Header2 : Header, IMarkdownElement
{
    public Header2(string value) : base(Indicators.Header2, value)
    {
    }

    public string ToMarkdown(MarkdownDocumentOptions options) => CreateMarkdownHeader();
}
