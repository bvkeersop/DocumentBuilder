using NDocument.Domain.Options;

namespace NDocument.Domain.Interfaces
{
    public interface IExcelWritable
    {
        void WriteToExcel(ExcelDocumentOptions excelDocumentOptions);
    }
}
