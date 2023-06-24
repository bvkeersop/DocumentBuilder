using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentBuilder.Markdown.Extensions;

internal static class StringExtensions
{
    public static string ReplaceAt(this string @string, int index, char replacementCharacter) 
        => @string.Remove(index, 1).Insert(index, replacementCharacter.ToString());
}
