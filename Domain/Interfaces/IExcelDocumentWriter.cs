namespace NDocument.Domain.Interfaces
{
    public interface IExcelDocumentWriter
    {
        public void AddWorksheet(string worksheetName);
        public void Write(IExcelConvertable excelConvertable);
        public void Save(string filePath);
    }
}
