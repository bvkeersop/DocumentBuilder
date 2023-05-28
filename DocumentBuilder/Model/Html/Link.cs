using DocumentBuilder.Constants;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Html
{
    public class Link : IHtmlConvertable
    {
        public string Rel { get; }
        public string Href { get; }
        public string Type { get; }

        public HtmlAttributes Attributes => throw new NotImplementedException();

        public Link(string rel, string href, string type)
        {
            Rel = rel;
            Href = href;
            Type = type;
        }

        public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            var value = $"<{HtmlIndicators.Link} rel=\"{Rel}\" type=\"{Type}\" href=\"{Href}\" />";
            return WrapWithIndentationAndNewLine(value, options, indentationLevel);
        }

        protected static ValueTask<string> WrapWithIndentationAndNewLine(string value, HtmlDocumentOptions options, int indentationLevel)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indenationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var html = $"{indenationProvider.GetIndentation(0)}{value}{newLineProvider.GetNewLine()}";
            return new ValueTask<string>(html);
        }
    }
}
