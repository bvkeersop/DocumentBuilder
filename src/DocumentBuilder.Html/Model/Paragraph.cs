using DocumentBuilder.Html.Constants;

namespace DocumentBuilder.Html.Model;
public class Paragraph : HtmlElement
{

    public Paragraph(string value) : base(Indicators.Paragraph, value)
    {
    }
}
