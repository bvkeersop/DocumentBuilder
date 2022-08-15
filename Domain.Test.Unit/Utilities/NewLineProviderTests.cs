using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Factories;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Utilities
{
    [TestClass]
    public class NewLineProviderTests
    {
        private const string _windowsNewLine = "\r\n";
        private const string _linuxNewLine = "\n";

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        public void CreateNewLine_CreatesCorrectNewLine(LineEndings lineEndings)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            // Act
            var newLine = newLineProvider.GetNewLine();

            // Assert
            var expectedNewLine = GetExpectedNewLine(lineEndings);
            newLine.Should().Be(expectedNewLine);
        }

        private static string GetExpectedNewLine(LineEndings lineEndings)
        {
            return lineEndings switch
            {
                LineEndings.Environment => Environment.NewLine,
                LineEndings.Windows => _windowsNewLine,
                LineEndings.Linux => _linuxNewLine,
                _ => throw new NotSupportedException($"{lineEndings} is currently not supported")
            };
        }
    }
}
