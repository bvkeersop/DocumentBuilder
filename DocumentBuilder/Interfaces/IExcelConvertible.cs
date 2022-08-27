using DocumentBuilder.Model.Excel;
using DocumentBuilder.Options;

namespace DocumentBuilder.Interfaces
{
    public interface IExcelConvertable
    {
        IEnumerable<ExcelTableCell> ToExcel(ExcelDocumentOptions options);
    }
}
