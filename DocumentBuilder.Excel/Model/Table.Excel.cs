using DocumentBuilder.Extensions;
using DocumentBuilder.Model.Excel;
using DocumentBuilder.Options;

namespace DocumentBuilder.Model
{
    public partial class Table<TRow>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Will most likely use in future, and don't want a breaking change")]
        private IEnumerable<ExcelTableCell> CreateExcelTable(ExcelDocumentOptions options)
        {
            return TableCells.Select(t => t.ToExcelTableCell());
        }
    }
}
