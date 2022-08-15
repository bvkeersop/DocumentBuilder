using ClosedXML.Excel;

namespace DocumentBuilder.Domain.Factories
{
    internal static class XLWorkbookFactory
    {
        public static IXLWorkbook Create()
        {
            return new XLWorkbook();
        }
    }
}
