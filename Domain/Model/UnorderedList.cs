using NDocument.Domain.Constants;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class UnorderedList<T> : ListBase<T>, IMarkdownConvertable, IHtmlConvertable
    {
        public UnorderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(MarkdownIndicators.UnorderedList, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlListAsync(HtmlIndicators.UnorderedList, options, indentationLevel);
        }
    }
}
