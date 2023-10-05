using DocumentBuilder.Core.Model;
using DocumentBuilder.Html.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Utilities;
using System.Text;

namespace DocumentBuilder.Html.Model.Body;
public class Table<TRow> : TableBase<TRow>, IHtmlElement
{
    public HtmlDocumentOptions Options { get; }
    public INewLineProvider NewLineProvider { get; }
    public IIndentationProvider IndentationProvider { get; }
    public Attributes Attributes { get; } = new Attributes();
    public InlineStyles InlineStyles { get; } = new InlineStyles();

    public Table(IEnumerable<TRow> tableRows, HtmlDocumentOptions options) : base(tableRows)
    {
        Options = options;
        NewLineProvider = options.NewLineProvider;
        IndentationProvider = options.IndentationProvider;
        NewLineProvider = options.NewLineProvider;
    }

    public string ToHtml(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        var sb = new StringBuilder();

        sb.Append(IndentationProvider.GetIndentation(indentationLevel));
        sb.Append(Indicators.Table.ToHtmlStartTag());
        sb.Append(NewLineProvider.GetNewLine());
        AppendHtmlTableHeader(sb, indentationLevel + 1);
        AppendHtmlTableRows(sb, indentationLevel + 1);

        sb.Append(IndentationProvider.GetIndentation(indentationLevel));
        sb.Append(Indicators.Table.ToHtmlEndTag());
        var x = sb.ToString();
        return x;
    }

    private void AppendHtmlTableHeader(StringBuilder sb, int indentationLevel)
    {

        sb.Append(IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.TableRow.ToHtmlStartTag())
            .Append(NewLineProvider.GetNewLine());

        var numberOfColumns = TableValues.NumberOfColumns;
        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnName = OrderedColumnAttributes.ElementAt(i).Name.Value;
            AppendHtmlTableCell(sb, columnName, Indicators.TableHeader, indentationLevel + 1);
        }

        sb.Append(IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.TableRow.ToHtmlEndTag())
            .Append(NewLineProvider.GetNewLine());
    }

    private void AppendHtmlTableRows(StringBuilder sb, int indentationLevel)
    {
        var numberOfRows = TableValues.NumberOfRows;
        for (var i = 0; i < numberOfRows; i++)
        {
            var currentRow = TableValues.GetRow(i);
            AppendHtmlTableRow(sb, currentRow, indentationLevel);
        }
    }

    private void AppendHtmlTableRow(StringBuilder sb, TableCell[] tableRow, int indentationLevel)
    {
        sb.Append(IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.TableRow.ToHtmlStartTag())
            .Append(NewLineProvider.GetNewLine());

        for (var i = 0; i < tableRow.Length; i++)
        {
            var cellValue = tableRow[i].Value;
            AppendHtmlTableCell(sb, cellValue, Indicators.TableData, indentationLevel + 1);
        }

        sb.Append(IndentationProvider.GetIndentation(indentationLevel))
            .Append(Indicators.TableRow.ToHtmlEndTag())
            .Append(NewLineProvider.GetNewLine());
    }

    private void AppendHtmlTableCell(
        StringBuilder sb,
        string cellValue,
        string htmlIndicator,
        int indentationLevel)
        => sb.Append(IndentationProvider.GetIndentation(indentationLevel))
            .Append(htmlIndicator.ToHtmlStartTag())
            .Append(cellValue)
            .Append(htmlIndicator.ToHtmlEndTag())
            .Append(NewLineProvider.GetNewLine());
}