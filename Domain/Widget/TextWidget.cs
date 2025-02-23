namespace Domain.Widget
{
    public class TextWidget : WidgetBase
    {
        public string Text { get; set; }

        public override string GetValue()
        {
            return Text;
        }
    }

}
