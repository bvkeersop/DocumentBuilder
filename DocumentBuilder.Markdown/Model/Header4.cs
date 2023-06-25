using DocumentBuilder.Constants;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;
internal class Header4 : Header, IMarkdownElement
{
    public Header4(string value) : base(Indicators.Header4, value)
    {
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args) => CreateMarkdownHeader();
}