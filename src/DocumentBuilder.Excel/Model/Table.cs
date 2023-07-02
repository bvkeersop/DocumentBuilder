using DocumentBuilder.Core.Model;
using DocumentBuilder.Excel.Extensions;
using DocumentBuilder.Excel.Model;
using DocumentBuilder.Excel.Options;

namespace DocumentBuilder.Model;

public class Table<TRow> : TableBase<TRow>, IExcelElement
{
    public Table(IEnumerable<TRow> tableRows) : base(tableRows)
    {
    }

    public IEnumerable<Excel.Model.TableCell> ToExcel(ExcelDocumentOptions options) => TableCells.Select(t => t.ToExcelTableCell());
}
