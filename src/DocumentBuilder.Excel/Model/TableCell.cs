namespace DocumentBuilder.Excel.Model;

public class TableCell
{
    public string Value { get; set; }
    public int ExcelRowIdentifier { get; }
    public string ExcelColumnIdentifier { get; }

    public TableCell(string value, int excelRowIdentifier, string excelColumnIdentifier)
    {
        Value = value;
        ExcelRowIdentifier = excelRowIdentifier;
        ExcelColumnIdentifier = excelColumnIdentifier;
    }
}
