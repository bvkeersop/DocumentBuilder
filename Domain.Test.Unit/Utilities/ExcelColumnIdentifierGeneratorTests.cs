using DocumentBuilder.Utilities;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Utilities
{
    [TestClass]
    public class ExcelColumnIdentifierGeneratorTests
    {
        [DataTestMethod]
        [DataRow(1, "A")]
        [DataRow(2, "B")]
        [DataRow(27, "AA")]
        [DataRow(28, "AB")]
        [DataRow(703, "AAA")]
        [DataRow(704, "AAB")]
        public void GenerateColumnIdentifier_Success(int index, string expectedColumnIdentifier)
        {
            // Act
            var columnIdentifier = ExcelColumnIdentifierGenerator.GenerateColumnIdentifier(index);

            // Assert
            columnIdentifier.Should().Be(expectedColumnIdentifier);
        }
    }
}
