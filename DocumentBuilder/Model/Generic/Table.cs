using DocumentBuilder.Attributes;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Extensions;
using DocumentBuilder.Helpers;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model
{
    public partial class Table<TRow> : GenericElement, IExcelConvertable
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
            foreach(var tableRow in tableRows)
            {
                var tableRowType = tableRow?.GetType();
                if (tableRowType != genericType)
                {
                    var message = $"The type {tableRowType} does not equal the provided generic parameter {genericType}, base types are not supported";
                    throw new DocumentBuilderException(DocumentBuilderErrorCode.ProvidedGenericTypeForTableDoesNotEqualRunTimeType, message);
                }
            }
        }

        public override async ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0)
        {
            return await CreateHtmlTableAsync(options, indentationLevel).ConfigureAwait(false);
        }

        public override async ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return await CreateMarkdownTableAsync(options).ConfigureAwait(false);
        }

        public IEnumerable<ExcelTableCell> ToExcel(ExcelDocumentOptions options)
        {
            return CreateExcelTable(options);
        }

        private int GetLongestCellSizeForColumn(int columnIndex, bool isBold)
        {
            var longestTableValue = TableValues.GetLongestCellSizeOfColumn(columnIndex);
            var columnNameLength = OrderedColumnAttributes.ElementAt(columnIndex).Name.Value.Length;

            if (isBold)
            {
                columnNameLength += 4;
            }
            
            return Math.Max(longestTableValue, columnNameLength);
        }

        private string GetColumnName(int index, bool isBold)
        {
            var columnName = OrderedColumnAttributes.ElementAt(index).Name.Value;

            if (isBold)
            {
                return $"**{columnName}**";
            }

            return columnName;
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
    }
}
