namespace DocumentBuilder.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => (enumerable == null || !enumerable.Any());
    }
}
