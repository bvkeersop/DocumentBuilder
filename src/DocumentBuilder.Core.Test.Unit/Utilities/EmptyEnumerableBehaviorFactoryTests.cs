using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Factories
{
    [TestClass]
    public class EmptyEnumerableBehaviorFactoryTests
    {
        [DataTestMethod]
        [DataRow(EmptyRenderingStrategy.Render)]
        [DataRow(EmptyRenderingStrategy.SkipRender)]
        [DataRow(EmptyRenderingStrategy.ThrowException)]
        public void Create_CreatesCorrectEnumerableValidator(EmptyRenderingStrategy emptyEnumerableBehavior)
        {
            // Act
            var enumerableValidator = EnumerableValidatorFactory.Create(emptyEnumerableBehavior);

            // Assert
            enumerableValidator.Behavior.Should().Be(emptyEnumerableBehavior);
        }
    }
}
