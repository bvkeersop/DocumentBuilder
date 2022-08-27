using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Utilities;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Factories
{
    [TestClass]
    public class IndentationProviderFactoryTests
    {
        [DataTestMethod]
        [DataRow(IndentationType.Spaces)]
        [DataRow(IndentationType.Tabs)]
        public void Create_ReturnsCorrectInstance(IndentationType indentationType)
        {
            // Act
            var indentationProvider = IndentationProviderFactory.Create(indentationType, 1);

            // Assert
            var expectedIndentationProvider = GetIndenationProvider(indentationType, 1, 0);
            indentationProvider.GetType().Should().Be(expectedIndentationProvider.GetType());
        }

        private static IIndentationProvider GetIndenationProvider(IndentationType indentationType, int indentationSize, int rootIndentationLevel)
        {
            return indentationType switch
            {
                IndentationType.Spaces => new SpaceIdentationProvider(indentationSize, rootIndentationLevel),
                IndentationType.Tabs => new TabIdentationProvider(indentationSize, rootIndentationLevel),
                _ => throw new NotSupportedException($"{indentationType} is currently not supported")
            };
        }
    }
}
