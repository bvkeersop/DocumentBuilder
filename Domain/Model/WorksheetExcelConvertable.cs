using NDocument.Domain.Interfaces;

namespace NDocument.Domain.Model
{
    public class WorksheetExcelConvertable
    {
        public string WorksheetName { get; }
        public IExcelConvertable ExcelConvertable { get; }

        public WorksheetExcelConvertable(string worksheetName, IExcelConvertable excelConvertable)
        {
            WorksheetName = worksheetName;
            ExcelConvertable = excelConvertable;
        }
    }
}
