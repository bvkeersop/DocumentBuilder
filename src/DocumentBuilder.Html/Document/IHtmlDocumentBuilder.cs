namespace DocumentBuilder.Html.Document;

public interface IHtmlDocumentBuilder
{
    IHtmlDocumentHeaderBuilder Head(Action<IHtmlDocumentHeaderBuilder> executeBuildSteps);
    IHtmlDocumentBodyBuilder Body(Action<IHtmlDocumentBodyBuilder> executeBuildSteps);
    IHtmlDocumentFooterBuilder Body(Action<IHtmlDocumentBodyBuilder> executeBuildSteps);
}
