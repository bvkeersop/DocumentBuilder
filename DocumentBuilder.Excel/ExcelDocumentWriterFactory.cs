using DocumentBuilder.Excel.Options;

namespace DocumentBuilder.Excel;

public static class ExcelDocumentWriterFactory
{
    public static IExcelDocumentWriter Create(ExcelDocumentOptions options) => new ClosedXmlDocumentWriter(XLWorkbookFactory.Create, options);
}
