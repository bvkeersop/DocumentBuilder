using DocumentBuilder.Excel.Model;

namespace DocumentBuilder.Excel.Document;

public interface IExcelDocumentBuilder
{
    /// <summary>
    /// Adds a new worksheet to the document
    /// </summary>
    /// <param name="worksheetName"></param>
    /// <returns><see cref="IExcelDocumentBuilder"/></returns>
    IExcelDocumentBuilder AddWorksheet(string worksheetName);

    /// <summary>
    /// Adds a table to the document at the current worksheet
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRows">The values of the table rows</param>
    /// <returns><see cref="IExcelDocumentBuilder"/></returns>
    IExcelDocumentBuilder AddTable<TRow>(IEnumerable<TRow> tableRows);

    /// <summary>
    /// Builds the excel document
    /// </summary>
    /// <returns><see cref="ExcelDocument"/></returns>
    ExcelDocument Build();
}
