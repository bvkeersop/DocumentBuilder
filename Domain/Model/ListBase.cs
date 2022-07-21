using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using System.Text;

namespace NDocument.Domain.Model
{
    public abstract class ListBase<TValue> : GenericElement
    {
        public IEnumerable<TValue> Value { get; }
        public const string _listItemIndicator = "li";

        public ListBase(IEnumerable<TValue> value)
        {
            Value = value;
        }

        protected ValueTask<string> CreateMarkdownList(string prepend, MarkdownDocumentOptions options)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in Value)
            {
                var value = $"{prepend} {item}";
                var listItem = ConvertToMarkdown(value, options);
                stringBuilder.Append(listItem);
            }

            var list = stringBuilder.ToString();
            return new ValueTask<string>(list); // Already has a new line
        }

        protected async ValueTask<string> CreateHtmlListAsync(string listIndicator, HtmlDocumentOptions options)
        {
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"<{listIndicator}>");
            stringBuilder.Append(newLineProvider.GetNewLine());

            foreach (var item in Value)
            {
                var listItem = await CreateHtmlListItem(item, options);
                stringBuilder.Append(listItem);
            }

            stringBuilder.Append($"</{listIndicator}>");
            var list = stringBuilder.ToString();
            return await ConvertToHtml(list, options);
        }

        private static ValueTask<string> CreateHtmlListItem(TValue? item, HtmlDocumentOptions options)
        {
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(indentationProvider.GetIndentation(1));
            stringBuilder.Append($"<{_listItemIndicator}>");
            stringBuilder.Append(item);
            stringBuilder.Append($"</{_listItemIndicator}>");
            var listItem = stringBuilder.ToString();
            return ConvertToHtml(listItem, options);
        }
    }
}
