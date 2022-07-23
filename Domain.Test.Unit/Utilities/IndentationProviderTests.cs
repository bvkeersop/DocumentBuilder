using FluentAssertions;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using System.Text;

namespace NDocument.Domain.Test.Unit.Utilities
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

        private string CreateExpectedIndentation(IndentationType indentationType, int indentationSize, int indentationLevel, int rootIndentationLevel)
        {
            var indentationCharacter = GetIndentationCharacter(indentationType);
            var identation = (rootIndentationLevel * indentationSize) + (indentationSize * indentationLevel);

            return new StringBuilder(indentationCharacter.Length * identation)
                .Insert(0, indentationCharacter, identation)
                .ToString();
        }

        private string GetIndentationCharacter(IndentationType indentationType)
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
