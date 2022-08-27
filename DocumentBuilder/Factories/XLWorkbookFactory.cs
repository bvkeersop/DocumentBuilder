using ClosedXML.Excel;

namespace DocumentBuilder.Factories
{
    internal static class XLWorkbookFactory
    {
        public static IXLWorkbook Create()
        {
            return new XLWorkbook();
        }
    }
}
