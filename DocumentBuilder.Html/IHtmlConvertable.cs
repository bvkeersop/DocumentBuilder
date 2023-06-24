using DocumentBuilder.Model.Html;
using DocumentBuilder.Options;

namespace DocumentBuilder.Interfaces
{
    public interface IHtmlConvertable
    {
        HtmlAttributes Attributes { get; }
        ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0);
    }
}
