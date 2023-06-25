using DocumentBuilder.Constants;
using DocumentBuilder.Markdown.Options;

namespace DocumentBuilder.Markdown.Model;

internal class Header1 : Header, IMarkdownElement
{
    public Header1(string value) : base(Indicators.Header1, value)
    {
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions args) => CreateMarkdownHeader();
}
