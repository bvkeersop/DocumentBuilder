using DocumentBuilder.Html.Document;

namespace DocumentBuilder.Html.Model.Head;

public class HtmlDocumentHeader
{
    private readonly Action<IHtmlDocumentHeaderBuilder> _executeBuildSteps;
    public string? Title { get; set; }
    public IList<IHtmlHeaderElement> Elements { get; } = new List<IHtmlHeaderElement>();
    public void AddHtmlHeaderElement(IHtmlHeaderElement element) => Elements.Add(element);
}
