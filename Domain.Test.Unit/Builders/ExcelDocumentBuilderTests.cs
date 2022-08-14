using ClosedXML.Excel;
using FluentAssertions;
using NDocument.Domain.Builders;
using NDocument.Domain.Model;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public class ExcelDocumentBuilderTests
    {
        private IEnumerable<ExcelTableRow> _excelTableRowsOne;
        private IEnumerable<ExcelTableRow> _excelTableRowsTwo;
        private Table<ExcelTableRow> _excelTableOne;
        private Table<ExcelTableRow> _excelTableTwo;

        [TestInitialize]
        public void TestInitialize()
        {
            _excelTableRowsOne = new List<ExcelTableRow>
            {
                new ExcelTableRow
                {
                    TextValue = "SheetOneTextValue1",
                    NumberValue = 1.5,
                    DateValue = new DateTime(1990, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1337)
                },
                new ExcelTableRow
                {
                    TextValue = "SheetOneTextValue2",
                    NumberValue = 1.6,
                    DateValue = new DateTime(1991, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1338)
                }
            };

            _excelTableRowsTwo = new List<ExcelTableRow>
            {
                new ExcelTableRow
                {
                    TextValue = "Sheet2TextValue2",
                    NumberValue = 1.7,
                    DateValue = new DateTime(1992, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1339)
                },
                new ExcelTableRow
                {
                    TextValue = "Sheet2TextValue2",
                    NumberValue = 1.8,
                    DateValue = new DateTime(1993, 8, 26),
                    TimeSpanValue = TimeSpan.FromSeconds(1340)
                }
            };

            _excelTableOne = new Table<ExcelTableRow>(_excelTableRowsOne);
            _excelTableTwo = new Table<ExcelTableRow>(_excelTableRowsTwo);
        }

        [TestMethod]
        public void Save_Success()
        {
            // Arrange
            var memoryStream = new MemoryStream();
            var firstWorksheetName = "worksheetOne";
            var secondWorksheetName = "worksheetTwo";
            var options = new ExcelDocumentOptions();
            var excelDocumentBuilder = new ExcelDocumentBuilder(options)
                .AddWorksheet(firstWorksheetName)
                .WithTable(_excelTableRowsOne)
                .AddWorksheet(secondWorksheetName)
                .WithTable(_excelTableRowsTwo);

            // Act
            excelDocumentBuilder.WriteToStream(memoryStream);

            // Assert
            var workbook = new XLWorkbook(memoryStream);

            //// First worksheet
            var worksheetOne = workbook.Worksheet(firstWorksheetName);
            var columnsOne = _excelTableOne.OrderedColumnAttributes.Select(o => o.Name);
            var valuesOne = _excelTableOne.TableValues;

            worksheetOne.Cell(1, "A").Value.Should().Be(columnsOne.ElementAt(0).Value);
            worksheetOne.Cell(1, "B").Value.Should().Be(columnsOne.ElementAt(1).Value);
            worksheetOne.Cell(1, "C").Value.Should().Be(columnsOne.ElementAt(2).Value);
            worksheetOne.Cell(1, "D").Value.Should().Be(columnsOne.ElementAt(3).Value);

            var row1Sheet1 = valuesOne.GetRow(0);
            worksheetOne.Cell(2, "A").Value.Should().Be(row1Sheet1.ElementAt(0).Value);
            worksheetOne.Cell(2, "B").Value.Should().Be(row1Sheet1.ElementAt(1).Value);
            worksheetOne.Cell(2, "C").Value.Should().Be(row1Sheet1.ElementAt(2).Value);
            worksheetOne.Cell(2, "D").Value.Should().Be(row1Sheet1.ElementAt(3).Value);

            var row2Sheet1 = valuesOne.GetRow(1);
            worksheetOne.Cell(3, "A").Value.Should().Be(row2Sheet1.ElementAt(0).Value);
            worksheetOne.Cell(3, "B").Value.Should().Be(row2Sheet1.ElementAt(1).Value);
            worksheetOne.Cell(3, "C").Value.Should().Be(row2Sheet1.ElementAt(2).Value);
            worksheetOne.Cell(3, "D").Value.Should().Be(row2Sheet1.ElementAt(3).Value);

            // Second worksheet
            var worksheetTwo = workbook.Worksheet(secondWorksheetName);
            var columnsTwo = _excelTableTwo.OrderedColumnAttributes.Select(o => o.Name);
            var valuesTwo = _excelTableTwo.TableValues;

            worksheetTwo.Cell(1, "A").Value.Should().Be(columnsTwo.ElementAt(0).Value);
            worksheetTwo.Cell(1, "B").Value.Should().Be(columnsTwo.ElementAt(1).Value);
            worksheetTwo.Cell(1, "C").Value.Should().Be(columnsTwo.ElementAt(2).Value);
            worksheetTwo.Cell(1, "D").Value.Should().Be(columnsTwo.ElementAt(3).Value);

            var row1Sheet2 = valuesTwo.GetRow(0);
            worksheetTwo.Cell(2, "A").Value.Should().Be(row1Sheet2.ElementAt(0).Value);
            worksheetTwo.Cell(2, "B").Value.Should().Be(row1Sheet2.ElementAt(1).Value);
            worksheetTwo.Cell(2, "C").Value.Should().Be(row1Sheet2.ElementAt(2).Value);
            worksheetTwo.Cell(2, "D").Value.Should().Be(row1Sheet2.ElementAt(3).Value);

            var row2Sheet2 = valuesTwo.GetRow(1);
            worksheetTwo.Cell(3, "A").Value.Should().Be(row2Sheet2.ElementAt(0).Value);
            worksheetTwo.Cell(3, "B").Value.Should().Be(row2Sheet2.ElementAt(1).Value);
            worksheetTwo.Cell(3, "C").Value.Should().Be(row2Sheet2.ElementAt(2).Value);
            worksheetTwo.Cell(3, "D").Value.Should().Be(row2Sheet2.ElementAt(3).Value);
        }
    }
}
