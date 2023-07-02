using DocumentBuilder.Excel.Options;

namespace DocumentBuilder.Excel.Model
{
    public interface IExcelElement
    {
        IEnumerable<TableCell> ToExcel(ExcelDocumentOptions options);
    }
}
