using DocumentBuilder.Excel.Extensions;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Shared.Attributes;
using DocumentBuilder.Shared.Extensions;
using DocumentBuilder.Shared.Model;

namespace DocumentBuilder.Model;

public class Table<TRow>
{
    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<TableCell> TableCells { get; }

    public Table(IEnumerable<TRow> tableRows)
    {
        ValidateRows(tableRows);
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Will most likely use in future, and don't want a breaking change")]
    private IEnumerable<ExcelTableCell> CreateExcelTable(ExcelDocumentOptions options) => TableCells.Select(t => t.ToExcelTableCell());
}
