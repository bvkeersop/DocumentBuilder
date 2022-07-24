using NDocument.Domain.Attributes;
using NDocument.Domain.Helpers;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public partial class Table<TValue> : GenericElement
    {
        public Matrix<TValue> TableValues { get; }
        public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }

        public Table(IEnumerable<TValue> tableRows)
        {
            TableValues = new Matrix<TValue>(tableRows);
            OrderedColumnAttributes = ReflectionHelper<TValue>.GetOrderedColumnAttributes(tableRows);
        }

        public override async ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return await CreateHtmlTableAsync(options, indentationLevel).ConfigureAwait(false);
        }

        public override async ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return await CreateMarkdownTableAsync(options).ConfigureAwait(false);
        }

        public void WriteToExcel(ExcelDocumentOptions options)
        {
            CreateExcelTable(options);
        }

        public async Task WriteAsMarkdownToStreamAsync(Stream outputStream, MarkdownDocumentOptions options)
        {
            await CreateMarkdownTableAsync(outputStream, options).ConfigureAwait(false);
        }

        public async Task WriteAsHtmlToStreamAsync(Stream outputStream, HtmlDocumentOptions options, int indentationLevel)
        {
            await CreateHtmlTableAsync(outputStream, options, indentationLevel).ConfigureAwait(false);
        }

        private int GetLongestCellSizeForColumn(int columnIndex, bool isBold)
        {
            var longestTableValue = TableValues.GetLongestCellSizeOfColumn(columnIndex);
            var columnNameLength = OrderedColumnAttributes.ElementAt(columnIndex).Name.Length;

            if (isBold)
            {
                columnNameLength = columnNameLength + 4;
            }
            
            return Math.Max(longestTableValue, columnNameLength);
        }

        private string GetColumnName(int index, bool isBold)
        {
            var columnName = OrderedColumnAttributes.ElementAt(index).Name;

            if (isBold)
            {
                return $"**{columnName}**";
            }

            return columnName;
        }
    }
}
