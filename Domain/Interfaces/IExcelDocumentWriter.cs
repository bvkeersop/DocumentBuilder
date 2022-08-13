using NDocument.Domain.Builders;
using NDocument.Domain.Model;

namespace NDocument.Domain.Interfaces
{
    public interface IExcelDocumentWriter
    {
        public void AddWorksheet(string worksheetName);
        public void Write(WorksheetExcelConvertable excelConvertable);
        public void Save(string filePath);
    }
}
