using DocumentBuilder.Constants;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Html.Model;
{
    public class UnorderedList<T> : ListBase<T>, IHtmlElement
    {
        public UnorderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(MarkdownIndicators.UnorderedListItem, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlListAsync(Indicators.UnorderedList, options, indentationLevel);
        }
    }
}
