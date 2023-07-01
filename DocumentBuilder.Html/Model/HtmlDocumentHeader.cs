namespace DocumentBuilder.Html.Model;

public class HtmlDocumentHeader
{
    public IList<IHtmlHeaderElement> Elements { get; } = new List<IHtmlHeaderElement>();
    public void AddHtmlHeaderElement(IHtmlHeaderElement element) => Elements.Add(element);
}
