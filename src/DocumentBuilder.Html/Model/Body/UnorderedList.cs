using DocumentBuilder.Html.Constants;

namespace DocumentBuilder.Html.Model.Body;
public class UnorderedList<T> : ListBase<T>
{
    public UnorderedList(IEnumerable<T> value) : base(Indicators.UnorderedList, value)
    {
    }
}
