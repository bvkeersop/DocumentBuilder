using DocumentBuilder.Domain.Model.Excel;

namespace DocumentBuilder.Domain.Interfaces
{
    public interface IExcelDocumentWriter
    {
        public void AddWorksheet(string worksheetName);
        public void Write(WorksheetExcelConvertable excelConvertable);
        public void WriteToStream(Stream outputStream);
    }
}
