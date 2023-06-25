using ClosedXML.Excel;

namespace DocumentBuilder.Excel
{
    internal static class XLWorkbookFactory
    {
        public static IXLWorkbook Create()
        {
            return new XLWorkbook();
        }
    }
}
