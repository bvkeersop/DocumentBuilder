using DocumentBuilder.Excel.ClosedXml;
using DocumentBuilder.Excel.Options;

namespace DocumentBuilder.Excel.Document;

public static class ExcelDocumentWriterFactory
{
    public static IExcelDocumentWriter Create(ExcelDocumentOptions options) => new ClosedXmlDocumentWriter(XLWorkbookFactory.Create, options);
}
