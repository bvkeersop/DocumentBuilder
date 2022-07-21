using NDocument.Domain.Interfaces;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class UnorderedList<T> : ListBase<T>, IMarkdownConvertable, IHtmlConvertable
    {
        private const string _markdownUnorderedListIndicator = "-";
        private const string _htmlUnorderedListIndicator = "ul";

        public UnorderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(_markdownUnorderedListIndicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options)
        {
            return CreateHtmlListAsync(_htmlUnorderedListIndicator, options);
        }
    }
}
