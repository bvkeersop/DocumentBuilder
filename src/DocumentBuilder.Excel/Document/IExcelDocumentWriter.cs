using DocumentBuilder.Excel.Model;

namespace DocumentBuilder.Excel.Document;
public interface IExcelDocumentWriter
{
    public void WriteToStream(ExcelDocument excelDocument, Stream outputStream);
}