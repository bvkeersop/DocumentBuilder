using ClosedXML.Excel;
using NDocument.Domain.DocumentWriters;
using NDocument.Domain.Extensions;
using NDocument.Domain.Model;
using NDocument.Domain.Model.Excel;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;
using NSubstitute;

namespace NDocument.Domain.Test.Unit.DocumentWriters
{
    [TestClass]
    public class ClosedXmlDocumentWriterTests
    {
        [TestMethod]
        public void WriteToStream_CallsWorkbookSaveAs()
        {
            // Arrange
            var memoryStream = new MemoryStream();
            var options = new ExcelDocumentOptions();
            var workbookMock = Substitute.For<IXLWorkbook>();
            Func<IXLWorkbook> factory = () => workbookMock;
            var closedXmlDocumentWriter = new ClosedXmlDocumentWriter(factory, options);

            // Act
            closedXmlDocumentWriter.WriteToStream(memoryStream);

            // Assert
            workbookMock.Received(1).SaveAs(memoryStream);
        }

        [TestMethod]
        public void AddWorksheet_CallsWorkBookWorksheetsAdd()
        {
            // Arrange
            var worksheetName = "worksheetName";
            var options = new ExcelDocumentOptions();
            var workbookMock = Substitute.For<IXLWorkbook>();
            Func<IXLWorkbook> factory = () => workbookMock;
            var closedXmlDocumentWriter = new ClosedXmlDocumentWriter(factory, options);

            // Act
            closedXmlDocumentWriter.AddWorksheet(worksheetName);

            // Assert
            workbookMock.Worksheets.Received(1).Add(worksheetName);
        }

        [TestMethod]
        public void WriteExcelTableCeels_CallsWorkSheetCellSetValue()
        {
            // Arrange
            var worksheetName = "worksheetName";
            var excelTableRowOne = new ExcelTableRow
            {
                TextValue = "valueOne"
            };
            var excelTableRowTwo = new ExcelTableRow
            {
                TextValue = "valueTwo"
            };
            var excelTableRows = new List<ExcelTableRow>
            {
                excelTableRowOne,
                excelTableRowTwo
            };
            var table = new Table<ExcelTableRow>(excelTableRows);
            var worksheetExcelConvertable = new WorksheetExcelConvertable(worksheetName, table);
            var options = new ExcelDocumentOptions();
            var workbookMock = Substitute.For<IXLWorkbook>();
            Func<IXLWorkbook> factory = () => workbookMock;
            var closedXmlDocumentWriter = new ClosedXmlDocumentWriter(factory, options);

            // Act
            closedXmlDocumentWriter.AddWorksheet(worksheetName);
            closedXmlDocumentWriter.Write(worksheetExcelConvertable);

            // Assert
            foreach (var tableCell in table.TableCells)
            {
                var excelTableCell = tableCell.ToExcelTableCell();
                var currentCell = workbookMock.Worksheet(worksheetName).Received(1).Cell(excelTableCell.ExcelRowIdentifier, excelTableCell.ExcelColumnIdentifier);
                currentCell.Received(1).SetValue(excelTableCell.Value);
            }
        }
    }
}
