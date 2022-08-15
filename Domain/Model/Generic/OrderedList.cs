using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model.Generic
{
    public class OrderedList<T> : ListBase<T>
    {
        public OrderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(MarkdownIndicators.OrderedListItem, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return CreateHtmlListAsync(HtmlIndicators.OrderedList, options, indentationLevel);
        }
    }
}
