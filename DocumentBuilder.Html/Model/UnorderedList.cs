using DocumentBuilder.Constants;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model.Generic
{
    public class UnorderedList<T> : ListBase<T>, IMarkdownConvertable, IHtmlElement
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
            return CreateHtmlListAsync(HtmlIndicators.UnorderedList, options, indentationLevel);
        }
    }
}
