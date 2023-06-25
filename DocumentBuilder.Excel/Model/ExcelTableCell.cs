namespace DocumentBuilder.Model.Excel;

public class ExcelTableCell
{
    public string Value { get; set; }
    public int ExcelRowIdentifier { get; }
    public string ExcelColumnIdentifier { get; }

    public ExcelTableCell(string value, int excelRowIdentifier, string excelColumnIdentifier)
    {
        Value = value;
        ExcelRowIdentifier = excelRowIdentifier;
        ExcelColumnIdentifier = excelColumnIdentifier;
    }
}
