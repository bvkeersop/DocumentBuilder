using DocumentBuilder.Constants;
using DocumentBuilder.Factories;
using DocumentBuilder.Html.Options;
using System.Text;

namespace DocumentBuilder.Html.Model
{
    public abstract class ListBase<TValue> : IHtmlElement
    {
        public string Indicator { get; }
        protected IEnumerable<TValue> Value { get; }

        protected ListBase(string indicator, IEnumerable<TValue> value)
        {
            Indicator = indicator;
            Value = value;
        }

        protected async ValueTask<string> CreateHtmlListAsync(string listIndicator, HtmlDocumentOptions options, int indentationLevel = 0)
        {
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(indentationProvider.GetIndentation(0));
            stringBuilder.Append(GetHtmlStartTagWithAttributes(listIndicator));
            stringBuilder.Append(newLineProvider.GetNewLine());

            foreach (var item in Value)
            {
                var listItem = await CreateHtmlListItem(item, options, indentationLevel + 1);
                stringBuilder.Append(listItem);
            }

            stringBuilder.Append(indentationProvider.GetIndentation(0));
            stringBuilder.Append(listIndicator.ToHtmlEndTag());
            stringBuilder.Append(newLineProvider.GetNewLine());
            return stringBuilder.ToString();
        }

        private ValueTask<string> CreateHtmlListItem(TValue? item, HtmlDocumentOptions options, int indentationLevel)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetHtmlStartTagWithAttributes(Indicators.ListItem));
            stringBuilder.Append(item);
            stringBuilder.Append(Indicators.ListItem.ToHtmlEndTag());
            var listItem = stringBuilder.ToString();
            return WrapWithIndentationAndNewLine(listItem, options, indentationLevel);
        }
    }
}
