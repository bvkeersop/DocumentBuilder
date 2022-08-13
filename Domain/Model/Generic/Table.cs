using NDocument.Domain.Attributes;
using NDocument.Domain.Extensions;
using NDocument.Domain.Helpers;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model.Excel;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public partial class Table<TValue> : GenericElement, IExcelConvertable
    {
        public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
        public Matrix<TValue> TableValues { get; }
        public IEnumerable<TableCell> TableCells { get; }

        public Table(IEnumerable<TValue> tableRows)
        {
            TableValues = new Matrix<TValue>(tableRows);
            OrderedColumnAttributes = ReflectionHelper<TValue>.GetOrderedColumnAttributes(tableRows);
            TableCells = CreateEnumerableOfTableCells();
        }

        public override async ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
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
