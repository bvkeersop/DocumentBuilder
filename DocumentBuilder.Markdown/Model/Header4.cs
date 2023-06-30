using DocumentBuilder.Constants;

namespace DocumentBuilder.Markdown.Model;
internal class Header4 : Header, IMarkdownElement
{
    public Header4(string value) : base(Indicators.Header4, value)
    {
    }
}