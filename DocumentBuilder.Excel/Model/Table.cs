using DocumentBuilder.Excel.Extensions;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Core.Attributes;
using DocumentBuilder.Core.Extensions;
using DocumentBuilder.Core.Model;

namespace DocumentBuilder.Model;

public class Table<TRow> : IExcelElement
{
    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<Core.Model.TableCell> TableCells { get; }

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

    public IEnumerable<Excel.Model.TableCell> ToExcel(ExcelDocumentOptions options) => TableCells.Select(t => t.ToExcelTableCell());
}
