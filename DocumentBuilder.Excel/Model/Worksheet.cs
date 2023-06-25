using DocumentBuilder.Interfaces;

namespace DocumentBuilder.Excel.Model;

public class Worksheet
{
    public string Name { get; }
    public IList<IExcelElement> ExcelElements { get; } = new List<IExcelElement>();

    public Worksheet(string name)
    {
        Name = name;
    }

    public void AddExcelElement(IExcelElement excelElement) => ExcelElements.Add(excelElement);
}
