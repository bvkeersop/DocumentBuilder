using DocumentBuilder.Interfaces;

namespace DocumentBuilder.Model.Excel;

public class WorksheetExcelConvertable
{
    public string WorksheetName { get; }
    public IExcelElement ExcelConvertable { get; }

    public WorksheetExcelConvertable(string worksheetName, IExcelElement excelConvertable)
    {
        WorksheetName = worksheetName;
        ExcelConvertable = excelConvertable;
    }
}
