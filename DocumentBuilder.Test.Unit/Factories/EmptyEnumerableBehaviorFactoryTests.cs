using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Factories
{
    [TestClass]
    public class EmptyEnumerableBehaviorFactoryTests
    {
        [DataTestMethod]
        [DataRow(EmptyEnumerableBehavior.Render)]
        [DataRow(EmptyEnumerableBehavior.SkipRender)]
        [DataRow(EmptyEnumerableBehavior.ThrowException)]
        public void Create_CreatesCorrectEnumerableValidator(EmptyEnumerableBehavior emptyEnumerableBehavior)
        {
            // Act
            var enumerableValidator = EnumerableValidatorFactory.Create(emptyEnumerableBehavior);

            // Assert
            enumerableValidator.Behavior.Should().Be(emptyEnumerableBehavior);
        }
    }
}
