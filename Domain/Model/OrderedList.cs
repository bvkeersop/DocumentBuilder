using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class OrderedList<T> : ListBase<T>
    {
        private const string _markdownOrderedListIndicator = "1.";
        private const string _htmlOrderedListIndicator = "ol";

        public OrderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(_markdownOrderedListIndicator, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options)
        {
            return CreateHtmlListAsync(_htmlOrderedListIndicator, options);
        }
    }
}
