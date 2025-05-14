using Domain.Widget;

namespace Memento
{
    public class WidgetMemento
    {
        public WidgetBase Snapshot { get; }

        public WidgetMemento(WidgetBase widget)
        {
            Snapshot = (WidgetBase)widget.Clone();
        }
    }
}
