using NDocument.Domain.Extensions;
using NDocument.Domain.Model.Excel;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public partial class Table<TValue>
    {
        private IEnumerable<ExcelTableCell> CreateExcelTable(ExcelDocumentOptions options)
        {
            return TableCells.Select(t => t.ToExcelTableCell());
        }
    }
}
