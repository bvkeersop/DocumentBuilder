using DocumentBuilder.Markdown.Constants;

namespace DocumentBuilder.Markdown.Model;
internal class Header3 : Header, IMarkdownElement
{
    public Header3(string value) : base(Indicators.Header3, value)
    {
    }
}