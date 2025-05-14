using Visitor;

namespace Domain.Widget
{
    public class DateWidget : WidgetBase
    {
        public DateTime Date { get; set; }

        public override void Accept(IWidgetVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string GetValue()
        {
            return Date.ToString("dd/MM/yyyy");
        }

        protected override void RestoreInternal(WidgetBase from)
        {
            if(from is DateWidget widget)
            {
                Date = widget.Date;
            }
        }
    }
}
