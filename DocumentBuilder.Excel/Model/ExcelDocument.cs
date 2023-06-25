using DocumentBuilder.Excel.Options;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Shared.Extensions;

namespace DocumentBuilder.Excel.Model;

public class ExcelDocument
{
    private IExcelDocumentWriter _excelDocumentWriter;

    public IList<Worksheet> Worksheets { get; } = new List<Worksheet>();

    public ExcelDocument(ExcelDocumentOptions options)
    {
        _excelDocumentWriter = new ClosedXmlDocumentWriter(XLWorkbookFactory.Create, options);
    }

    public Worksheet GetWorksheet(string sheetName)
    {
        var matches = Worksheets.Where(w => w.Name == sheetName);

        if (matches.IsNullOrEmpty())
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.WorksheetNotFound);
        }

        if (matches.Count() > 1)
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.MultipleWorksheetsFound);
        }

        return matches.First();
    }

    public void AddWorksheet(Worksheet worksheet)
    {
        if (DoesWorksheetNameAlreadyExist(worksheet.Name))
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.WorksheetNameAlreadyExists);
        }

        Worksheets.Add(worksheet);
    }

    private bool DoesWorksheetNameAlreadyExist(string worksheetName) => Worksheets.Any(w => w.Name == worksheetName);
}
