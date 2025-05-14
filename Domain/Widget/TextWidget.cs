using Visitor;

namespace Domain.Widget
{
    public class TextWidget : WidgetBase
    {
        public string Text { get; set; }

        public override string GetValue()
        {
            return Text;
        }

        protected override void RestoreInternal(WidgetBase from)
        {
            if(from is  TextWidget widget)
            {
                Text = widget.Text;
            }
        }

        public override void Accept(IWidgetVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
