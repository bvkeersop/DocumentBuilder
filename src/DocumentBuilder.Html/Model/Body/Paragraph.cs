using DocumentBuilder.Html.Constants;

namespace DocumentBuilder.Html.Model.Body;
public class Paragraph : HtmlElement
{

    public Paragraph(string value) : base(Indicators.Paragraph, value)
    {
    }
}
