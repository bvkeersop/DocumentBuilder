using DocumentBuilder.Constants;

namespace DocumentBuilder.Html.Model;
public class UnorderedList<T> : ListBase<T>
{
    public UnorderedList(IEnumerable<T> value) : base(Indicators.UnorderedList, value)
    {
    }
}
