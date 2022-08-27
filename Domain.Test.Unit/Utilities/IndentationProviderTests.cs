using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using FluentAssertions;
using System.Text;

namespace DocumentBuilder.Test.Unit.Utilities
{
    [TestClass]
    public class IndentationProviderTests
    {
        private const string _space = " ";
        private const string _tab = "    ";

        [DataTestMethod]
        [DataRow(IndentationType.Spaces, 2, 1, 0)]
        [DataRow(IndentationType.Spaces, 2, 1, 1)]
        [DataRow(IndentationType.Tabs, 2, 1, 0)]
        [DataRow(IndentationType.Tabs, 2, 1, 1)]
        public void CreateIndentation_CreatesCorrectIndentation(IndentationType indentationType, int indentationSize, int indentationLevel, int rootIndentationLevel)
        {
            // Arrange
            var indentationProvider = IndentationProviderFactory.Create(indentationType, indentationSize, rootIndentationLevel);

            // Act
            var indentation = indentationProvider.GetIndentation(indentationLevel);

            // Assert
            var expectedIndentation = CreateExpectedIndentation(indentationType, indentationSize, indentationLevel, rootIndentationLevel);
            indentation.Should().Be(expectedIndentation);
        }

        private static string CreateExpectedIndentation(IndentationType indentationType, int indentationSize, int indentationLevel, int rootIndentationLevel)
        {
            var indentationCharacter = GetIndentationCharacter(indentationType);
            var identation = rootIndentationLevel * indentationSize + indentationSize * indentationLevel;

            return new StringBuilder(indentationCharacter.Length * identation)
                .Insert(0, indentationCharacter, identation)
                .ToString();
        }

        private static string GetIndentationCharacter(IndentationType indentationType)
        {
            return indentationType switch
            {
                IndentationType.Spaces => _space,
                IndentationType.Tabs => _tab,
                _ => throw new NotSupportedException($"{indentationType} is currently not supported")
            };
        }
    }
}
