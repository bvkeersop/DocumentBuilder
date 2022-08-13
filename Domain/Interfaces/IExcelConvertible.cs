using NDocument.Domain.Model;
using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    public interface IExcelConvertable
    {
        IEnumerable<ExcelTableCell> ToExcel(ExcelDocumentOptions excelDocumentOptions);
    }
}
