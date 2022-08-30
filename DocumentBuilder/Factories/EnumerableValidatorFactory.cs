using DocumentBuilder.Enumerations;
using DocumentBuilder.Validators;

namespace DocumentBuilder.Factories
{
    internal static class EnumerableValidatorFactory
    {
        public static IEnumerableValidator Create(EmptyEnumerableBehavior behavior)
        {
            return new EnumerableValidator(behavior);
        }
    }
}
