using DocumentBuilder.Excel.Options;
using DocumentBuilder.Model.Excel;

namespace DocumentBuilder.Interfaces
{
    public interface IExcelConvertable
    {
        IEnumerable<ExcelTableCell> ToExcel(ExcelDocumentOptions options);
    }
}
