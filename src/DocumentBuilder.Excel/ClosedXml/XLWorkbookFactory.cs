using ClosedXML.Excel;

namespace DocumentBuilder.Excel.ClosedXml
{
    internal static class XLWorkbookFactory
    {
        public static IXLWorkbook Create()
        {
            return new XLWorkbook();
        }
    }
}
