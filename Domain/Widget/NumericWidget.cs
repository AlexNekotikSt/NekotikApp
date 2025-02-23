namespace Domain.Widget
{
    public class NumericWidget : WidgetBase
    {
        public decimal Value { get; set; }

        public override string GetValue()
        {
            return Value.ToString();
        }
    }

}
