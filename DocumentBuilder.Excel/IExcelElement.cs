using DocumentBuilder.Excel.Options;
using DocumentBuilder.Model.Excel;

namespace DocumentBuilder.Interfaces
{
    public interface IExcelElement
    {
        IEnumerable<TableCell> ToExcel(ExcelDocumentOptions options);
    }
}
