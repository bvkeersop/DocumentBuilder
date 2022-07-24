using ClosedXML.Excel;
using FluentAssertions;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class TableExcelTests : TableTestBase
    {
        private Table<ExcelTableRow> _excelTable;
        private const string _worksheetName = $"{nameof(TableExcelTests)}";
        private const string _filePath = $"./{_worksheetName}.xlsx";

        [TestInitialize]
        public void TestInitialize()
        {
            File.Delete(_filePath);

            var excelTableRows = new List<ExcelTableRow>
            {
                new ExcelTableRow
                {
                    TextValue = "TextValue1",
                    NumberValue = 1.5,
                    DateValue = new DateTime(1990, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1337)
                },
                new ExcelTableRow
                {
                    TextValue = "TextValue2",
                    NumberValue = 1.6,
                    DateValue = new DateTime(1991, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1338)
                }
            };
            _excelTable = new Table<ExcelTableRow>(excelTableRows);
        }

        [TestMethod]
        public void WriteToExcel_Success()
        {
            // Arrange
            var options = new ExcelDocumentOptions
            {
                WorksheetName = _worksheetName,
                FilePath = _filePath,
            };

            // Act
            _excelTable.WriteToExcel(options);

            // Assert
            var workbook = new XLWorkbook(options.FilePath);
            var worksheet = workbook.Worksheet(1);
            var columns = _excelTable.OrderedColumnAttributes.Select(o => o.Name);
            var values = _excelTable.TableValues;

            worksheet.Cell(1, "A").Value.Should().Be(columns.ElementAt(0));
            worksheet.Cell(1, "B").Value.Should().Be(columns.ElementAt(1));
            worksheet.Cell(1, "C").Value.Should().Be(columns.ElementAt(2));
            worksheet.Cell(1, "D").Value.Should().Be(columns.ElementAt(3));

            var row1 = values.GetRow(0);
            worksheet.Cell(2, "A").Value.Should().Be(row1.ElementAt(0).Value);
            worksheet.Cell(2, "B").Value.Should().Be(row1.ElementAt(1).Value);
            worksheet.Cell(2, "C").Value.Should().Be(row1.ElementAt(2).Value);
            worksheet.Cell(2, "D").Value.Should().Be(row1.ElementAt(3).Value);

            var row2 = values.GetRow(1);
            worksheet.Cell(3, "A").Value.Should().Be(row2.ElementAt(0).Value);
            worksheet.Cell(3, "B").Value.Should().Be(row2.ElementAt(1).Value);
            worksheet.Cell(3, "C").Value.Should().Be(row2.ElementAt(2).Value);
            worksheet.Cell(3, "D").Value.Should().Be(row2.ElementAt(3).Value);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void WriteToExcel_NoWorksheetNameProvided_ThrowsArgumentException(string worksheetName)
        {
            // Arrange
            var options = new ExcelDocumentOptions
            {
                WorksheetName = worksheetName,
                FilePath = _filePath,
            };

            // Act
            var action = () =>_excelTable.WriteToExcel(options);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void WriteToExcel_NoFilePathProvided_ThrowsArgumentException(string filePath)
        {
            // Arrange
            var options = new ExcelDocumentOptions
            {
                WorksheetName = _worksheetName,
                FilePath = filePath,
            };

            // Act
            var action = () => _excelTable.WriteToExcel(options);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}