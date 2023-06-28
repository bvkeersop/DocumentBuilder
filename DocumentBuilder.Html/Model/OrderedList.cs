using DocumentBuilder.Constants;

namespace DocumentBuilder.Html.Model;
public class OrderedList<T> : ListBase<T>
{
    public OrderedList(IEnumerable<T> value) : base(Indicators.OrderedList, value)
    {
    }
}
