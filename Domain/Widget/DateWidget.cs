namespace Domain.Widget
{
    public class DateWidget : WidgetBase
    {
        public DateTime Date { get; set; }

        public override string GetValue()
        {
            return Date.ToString("dd/MM/yyyy");
        }
    }
}
