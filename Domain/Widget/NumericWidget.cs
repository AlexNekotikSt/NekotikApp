namespace Domain.Widget
{
    public class NumericWidget : WidgetBase
    {
        public decimal Value { get; set; }

        public override string GetValue()
        {
            return Value.ToString();
        }

        protected override void RestoreInternal(WidgetBase from)
        {
            if(from is  NumericWidget widget)
            {
                Value = widget.Value;
            }
        }
    }

}
