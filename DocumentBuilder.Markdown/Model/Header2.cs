using DocumentBuilder.Constants;

namespace DocumentBuilder.Markdown.Model;
internal class Header2 : Header, IMarkdownElement
{
    public Header2(string value) : base(Indicators.Header2, value)
    {
    }

    public ValueTask<string> ToMarkdownAsync(MarkdownConversionArgs args) => CreateMarkdownHeader();
}
