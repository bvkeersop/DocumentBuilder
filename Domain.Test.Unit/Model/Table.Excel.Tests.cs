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
        private const string _worksheetName = $"{nameof(TableExcelTests)}-{nameof(WriteToExcel)}";
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
        public void WriteToExcel()
        {
            var options = new ExcelDocumentOptions
            {
                WorksheetName = _worksheetName,
                FilePath = _filePath,
            };

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
    }
}