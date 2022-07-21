namespace NDocument.Domain.Extensions
{
    internal static class StringExtensions
    {
        public static string ReplaceAt(this string @string, int index, char replacementCharacter)
        {
            return @string
                .Remove(index, 1)
                .Insert(index, replacementCharacter.ToString());
        }
    }
}
