using DocumentBuilder.Exceptions;
using System.Text;

namespace DocumentBuilder.Html.Model;

public class Attributes
{
    public const string Id = "id";
    public const string Class = "class";
    public const string Style = "style";

    private readonly HashSet<string> _uniqueAttributes = new()
    {
        Id
    };

    private readonly IDictionary<string, HashSet<string>> _attributes = new Dictionary<string, HashSet<string>>();
    public int Count => _attributes.Count;
    public bool IsEmpty => Count <= 0;

    public void Add(string key, string value)
    {
        var shouldBeUnique = _uniqueAttributes.Contains(key);
        var containsKey = _attributes.ContainsKey(key);

        if (shouldBeUnique && containsKey)
        {
            throw new DocumentBuilderException(DocumentBuilderErrorCode.AttemptedToAddDuplicateUniqueHtmlAttribute,
                $"Attempted to add unique key '{key}' with value '{value}' to the html attributes while it was already present with value '{_attributes[key]}'");
        }

        if (containsKey)
        {
            ExpandAttributeValues(key, value);
            return;
        }

        OverrideAttributeValues(key, value);
    }

    private void OverrideAttributeValues(string key, string value)
    {
        var attributeValues = new HashSet<string>
        {
            value
        };

        _attributes.Add(key, attributeValues);
    }

    private void ExpandAttributeValues(string key, string value)
    {
        var existingValues = _attributes[key];
        existingValues.Add(value);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var kvp in _attributes)
        {
            sb.Append(kvp.Key);
            sb.Append('=');
            sb.Append('"');
            sb.Append(GetAttributeValuesAsString(kvp.Value));
            sb.Append('"');
            sb.Append(' ');
        }
        return sb.ToString().TrimEnd(' ');
    }

    private static string GetAttributeValuesAsString(HashSet<string> attributeValues) 
    {
        var sb = new StringBuilder();
        foreach (var attributeValue in attributeValues)
        {
            sb.Append(attributeValue);
            sb.Append(' ');
        }
        var attributeValuesAsString = sb.ToString();
        return attributeValuesAsString.TrimEnd(' ');
    }
}
