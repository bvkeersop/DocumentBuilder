using DocumentBuilder.Excel.Options;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Core.Extensions;
using DocumentBuilder.Excel.ClosedXml;
using DocumentBuilder.Excel.Document;

namespace DocumentBuilder.Excel.Model;

public class ExcelDocument
{
    private readonly ExcelDocumentOptions _options;
    private readonly IExcelDocumentWriter _excelDocumentWriter;

    public IList<Worksheet> Worksheets { get; } = new List<Worksheet>();
    public ExcelDocument() : this(options: new ExcelDocumentOptions(), ExcelDocumentWriterFactory.Create(new ExcelDocumentOptions()))
    {
    }

    public ExcelDocument(ExcelDocumentOptions options) : this(options, ExcelDocumentWriterFactory.Create(new ExcelDocumentOptions()))
    {
    }

    public ExcelDocument(ExcelDocumentOptions options, IExcelDocumentWriter excelDocumentWriter)
    {
        _options = options;
        _excelDocumentWriter = excelDocumentWriter;
    }

    /// <summary>
    /// Get a worksheet by its name
    /// </summary>
    /// <param name="sheetName">The name of the worksheet to get</param>
    /// <returns>The found worksheet</returns>
    /// <exception cref="ExcelDocumentBuilderException">If no or multiple worksheets are found</exception>
    public Worksheet GetWorksheet(string sheetName)
    {
        var matches = Worksheets.Where(w => w.Name == sheetName);

        if (matches.IsNullOrEmpty())
        {
            throw new ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode.WorksheetNotFound);
        }

        if (matches.Count() > 1)
        {
            throw new ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode.MultipleWorksheetsFound);
        }

        return matches.First();
    }

    /// <summary>
    /// Add a new worksheet
    /// </summary>
    /// <param name="worksheet">The worksheet to add</param>
    /// <exception cref="ExcelDocumentBuilderException">If a worksheet with the same name already exists</exception>
    public void AddWorksheet(Worksheet worksheet)
    {
        if (DoesWorksheetNameAlreadyExist(worksheet.Name))
        {
            throw new ExcelDocumentBuilderException(ExcelDocumentBuilderErrorCode.WorksheetNameAlreadyExists);
        }

        Worksheets.Add(worksheet);
    }

    private bool DoesWorksheetNameAlreadyExist(string worksheetName) => Worksheets.Any(w => w.Name == worksheetName);


    /// <summary>
    /// Writes the document to the provided output stream
    /// </summary>
    /// <param name="outputStream">The output stream</param>
    public void Save(Stream outputStream)
    {
        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        _excelDocumentWriter.WriteToStream(this, outputStream);
    }

    /// <summary>
    /// Writes the document to the provided path, will replace existing documents
    /// </summary>
    /// <param name="filePath">The path which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public void Save(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"{nameof(filePath)} cannot be null or empty");
        }

        using FileStream fileStream = File.Create(filePath);
        Save(fileStream);
    }
}
