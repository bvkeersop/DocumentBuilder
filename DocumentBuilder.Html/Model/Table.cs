using DocumentBuilder.Constants;
using DocumentBuilder.Core.Attributes;
using DocumentBuilder.Core.Extensions;
using DocumentBuilder.Core.Model;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Utilities;
using System.Text;

namespace DocumentBuilder.Html.Model;
public class Table<TRow> : IHtmlElement
{
    public INewLineProvider NewLineProvider { get; }
    public IIndentationProvider IndentationProvider { get; }

    public HtmlDocumentOptions Options { get; }
    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<Core.Model.TableCell> TableCells { get; }
    public IIndentationProvider IdentationProvider { get; }
    public Attributes Attributes { get; } = new Attributes();

    public Table(IEnumerable<TRow> tableRows, HtmlDocumentOptions options)
    {
        ValidateRows(tableRows);
        NewLineProvider = options.NewLineProvider;
        IndentationProvider = options.IndentationProvider;
        Options = options;
        TableValues = new Matrix<TRow>(tableRows);
        OrderedColumnAttributes = ReflectionHelper<TRow>.GetOrderedColumnAttributes();
        TableCells = CreateEnumerableOfTableCells();
        IdentationProvider = options.IndentationProvider;
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

    private void AppendHtmlTableRow(StringBuilder sb, Core.Model.TableCell[] tableRow, int indentationLevel)
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

    private static void ValidateRows(IEnumerable<TRow> tableRows)
    {
        var genericType = typeof(TRow);
        foreach (var tableRow in tableRows)
        {
            var tableRowType = tableRow?.GetType();
            if (tableRowType != genericType)
            {
                var message = $"The type {tableRowType} does not equal the provided generic parameter {genericType}, base types are not supported";
                throw new DocumentBuilderException(DocumentBuilderErrorCode.ProvidedGenericTypeForTableDoesNotEqualRunTimeType, message);
            }
        }
    }

    private IEnumerable<Core.Model.TableCell> CreateEnumerableOfTableCells()
    {
        var columnTableCells = Enumerable.Empty<Core.Model.TableCell>();
        for (var i = 0; i < OrderedColumnAttributes.Count(); i++)
        {
            var currentOrderedColumnAttribute = OrderedColumnAttributes.ElementAt(i);
            var tableCell = new Core.Model.TableCell(
                currentOrderedColumnAttribute.Name.Value,
                currentOrderedColumnAttribute.Name.GetType(),
                0,
                i);
            columnTableCells = columnTableCells.Append(tableCell);
        }

        var shiftedTableCells = TableValues.TableCells.Select(t => t.ShiftRow());

        return columnTableCells.Concat(shiftedTableCells);
    }
}