using DocumentBuilder.Html.Constants;

namespace DocumentBuilder.Html.Model.Body;
public class OrderedList<T> : ListBase<T>
{
    public OrderedList(IEnumerable<T> value) : base(Indicators.OrderedList, value)
    {
    }
}
