using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Model.Excel;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Model
{
    public partial class Table<TValue>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Will most likely use in future, and don't want a breaking change")]
        private IEnumerable<ExcelTableCell> CreateExcelTable(ExcelDocumentOptions options)
        {
            return TableCells.Select(t => t.ToExcelTableCell());
        }
    }
}
