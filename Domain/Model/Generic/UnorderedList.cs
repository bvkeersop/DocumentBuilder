using NDocument.Domain.Constants;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model.Generic
{
    public class UnorderedList<T> : ListBase<T>, IMarkdownConvertable, IHtmlConvertable
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
