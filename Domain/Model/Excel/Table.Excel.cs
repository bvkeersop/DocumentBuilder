using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Model.Excel;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model
{
    public partial class Table<TValue>
    {
        private IEnumerable<ExcelTableCell> CreateExcelTable(ExcelDocumentOptions options)
        {
            return TableCells.Select(t => t.ToExcelTableCell());
        }
    }
}
