namespace DocumentBuilder.Html.Extensions;

internal static class StringExtensions
{
    public static string ToHtmlStartTag(this string indicator) => $"<{indicator}>";

    public static string ToHtmlEndTag(this string indicator) => $"</{indicator}>";
}
