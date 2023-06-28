namespace DocumentBuilder.Html.Model;
public abstract class Header : HtmlElement
{
    protected Header(string indicator, string value) : base(indicator, value)
    {
    }
}