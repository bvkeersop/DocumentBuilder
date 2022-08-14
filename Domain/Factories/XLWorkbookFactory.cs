using ClosedXML.Excel;

namespace NDocument.Domain.Factories
{
    internal static class XLWorkbookFactory
    {
        public static IXLWorkbook Create()
        {
            return new XLWorkbook();
        } 
    }
}
