using DocumentBuilder.Domain.Model.Excel;

namespace DocumentBuilder.Domain.Interfaces
{
    public interface IExcelDocumentWriter
    {
        public void AddWorksheet(string worksheetName);
        public void Write(WorksheetExcelConvertable worksheetExcelConvertable);
        public void WriteToStream(Stream outputStream);
    }
}
