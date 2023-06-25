using DocumentBuilder.Constants;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;
internal class Header3 : Header, IMarkdownElement
{
    public Header3(string value) : base(Indicators.Header3, value)
    {
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args) => CreateMarkdownHeader();
}