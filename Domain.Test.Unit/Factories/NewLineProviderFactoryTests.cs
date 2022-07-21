using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Test.Unit.Factories
{
    [TestClass]
    public class NewLineProviderFactoryTests
    {
        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public void Create_ReturnsCorrectInstance(LineEndings lineEndings)
        {
            // Act
            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            // Assert
            var expectedNewLineProvider = GetNewLineProvider(lineEndings);
            newLineProvider.GetType().Should().Be(expectedNewLineProvider.GetType());
        }

        private static INewLineProvider GetNewLineProvider(LineEndings lineEndings)
        {
            return lineEndings switch
            {
                LineEndings.Environment => new EnvironmentNewLineProvider(),
                LineEndings.Windows => new WindowsNewLineProvider(),
                LineEndings.Linux => new LinuxNewLineProvider(),
                _ => throw new NotSupportedException($"{lineEndings} is currently not supported")
            };
        }
    }
}
