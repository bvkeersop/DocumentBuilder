namespace DocumentBuilder.Extensions
{
    internal static class StringExtensions
    {
        public static string ToMarkdownLink(this string headerValue)
        {
            var link = headerValue.ReplaceSpacesWithDashes();
            return $"[{headerValue}](#{link})";
        }

        public static string ReplaceSpacesWithDashes(this string value)
        {
            return value.Replace(" ", "-");
        }

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
