using DocumentBuilder.Excel.Options;

namespace DocumentBuilder.Interfaces
{
    public interface IExcelElement
    {
        IEnumerable<Excel.Model.TableCell> ToExcel(ExcelDocumentOptions options);
    }
}
