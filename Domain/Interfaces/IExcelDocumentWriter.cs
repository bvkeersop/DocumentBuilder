using DocumentBuilder.Model.Excel;

namespace DocumentBuilder.Interfaces
{
    public interface IExcelDocumentWriter
    {
        public void AddWorksheet(string worksheetName);
        public void Write(WorksheetExcelConvertable worksheetExcelConvertable);
        public void WriteToStream(Stream outputStream);
    }
}
