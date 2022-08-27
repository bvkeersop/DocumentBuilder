namespace DocumentBuilder.Extensions
{
    internal static class StringExtensions
    {
        public static string ReplaceAt(this string @string, int index, char replacementCharacter)
        {
            return @string
                .Remove(index, 1)
                .Insert(index, replacementCharacter.ToString());
        }

        public static string ToHtmlStartTag(this string indicator)
        {
            return $"<{indicator}>";
        }

        public static string ToHtmlEndTag(this string indicator)
        {
            return $"</{indicator}>";
        }
    }
}
