using DocumentBuilder.Domain.Model.Excel;
using DocumentBuilder.Domain.Options;

namespace DocumentBuilder.Domain.Interfaces
{
    public interface IExcelConvertable
    {
        IEnumerable<ExcelTableCell> ToExcel(ExcelDocumentOptions options);
    }
}
