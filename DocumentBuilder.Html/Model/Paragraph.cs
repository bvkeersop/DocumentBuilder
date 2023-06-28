using DocumentBuilder.Constants;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;
public class Paragraph : HtmlElement
{

    public Paragraph(string value) : base(Indicators.Paragraph, value)
    {
    }

    public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        throw new NotImplementedException();
    }
}
