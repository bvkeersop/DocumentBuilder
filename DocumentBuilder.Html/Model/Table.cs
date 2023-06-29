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
    public IEnumerable<TableCell> TableCells { get; }

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
    }

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

    private IEnumerable<TableCell> CreateEnumerableOfTableCells()
    {
        var columnTableCells = Enumerable.Empty<TableCell>();
        for (var i = 0; i < OrderedColumnAttributes.Count(); i++)
        {
            var currentOrderedColumnAttribute = OrderedColumnAttributes.ElementAt(i);
            var tableCell = new TableCell(
                currentOrderedColumnAttribute.Name.Value,
                currentOrderedColumnAttribute.Name.GetType(),
                0,
                i);
            columnTableCells = columnTableCells.Append(tableCell);
        }

        var shiftedTableCells = TableValues.TableCells.Select(t => t.ShiftRow());

        return columnTableCells.Concat(shiftedTableCells);
    }

    private async Task<string> CreateHtmlTableAsync(HtmlDocumentOptions options, int indentationLevel)
    {
        var outputStream = new MemoryStream();
        await CreateHtmlTableAsync(outputStream, options, indentationLevel);
        return StreamHelper.GetStreamContents(outputStream);
    }

    private async Task CreateHtmlTableAsync(Stream outputStream, HtmlDocumentOptions options, int indentationLevel)
    {
        if (!outputStream.CanWrite)
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.StreamIsNotWriteable, nameof(outputStream));
        }

        var streamWriter = new StreamWriter(outputStream, leaveOpen: true);
        using var htmlStreamWriter = new HtmlStreamWriter(streamWriter, NewLineProvider, IndentationProvider);

        await htmlStreamWriter.WriteLineAsync(Indicators.Table.ToHtmlStartTag()).ConfigureAwait(false);

        await CreateHtmlTableHeaderAsync(htmlStreamWriter).ConfigureAwait(false);
        await CreateHtmlTableRowsAsync(htmlStreamWriter).ConfigureAwait(false);

        await htmlStreamWriter.WriteLineAsync(Indicators.Table.ToHtmlEndTag()).ConfigureAwait(false);
        await htmlStreamWriter.FlushAsync().ConfigureAwait(false);
    }

    private async Task CreateHtmlTableHeaderAsync(HtmlStreamWriter htmlStreamWriter)
    {
        var numberOfColumns = TableValues.NumberOfColumns;

        await htmlStreamWriter.WriteLineAsync(Indicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnName = OrderedColumnAttributes.ElementAt(i).Name.Value;
            await CreateHtmlTableCellAsync(htmlStreamWriter, columnName, Indicators.TableHeader, 2).ConfigureAwait(false);
        }

        await htmlStreamWriter.WriteLineAsync(Indicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
    }

    private async Task CreateHtmlTableRowsAsync(HtmlStreamWriter htmlStreamWriter)
    {
        var numberOfRows = TableValues.NumberOfRows;

        for (var i = 0; i < numberOfRows; i++)
        {
            var currentRow = TableValues.GetRow(i);
            await CreateHtmlTableRowAsync(htmlStreamWriter, currentRow).ConfigureAwait(false);
            await htmlStreamWriter.WriteNewLineAsync().ConfigureAwait(false);
        }
    }

    private static async Task CreateHtmlTableRowAsync(HtmlStreamWriter htmlStreamWriter, TableCell[] tableRow)
    {
        await htmlStreamWriter.WriteLineAsync(Indicators.TableRow.ToHtmlStartTag(), 1).ConfigureAwait(false);

        for (var i = 0; i < tableRow.Length; i++)
        {
            var cellValue = tableRow[i].Value;
            await CreateHtmlTableCellAsync(htmlStreamWriter, cellValue, Indicators.TableData, 2).ConfigureAwait(false);
        }

        await htmlStreamWriter.WriteAsync(Indicators.TableRow.ToHtmlEndTag(), 1).ConfigureAwait(false);
    }

    private static async Task CreateHtmlTableCellAsync(
        HtmlStreamWriter htmlStreamWriter,
        string cellValue,
        string htmlIndicator,
        int indentation)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder
            .Append(htmlIndicator.ToHtmlStartTag())
            .Append(cellValue)
            .Append(htmlIndicator.ToHtmlEndTag());
        await htmlStreamWriter.WriteLineAsync(stringBuilder.ToString(), indentation).ConfigureAwait(false);
    }

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
    {
        throw new NotImplementedException();
    }
}