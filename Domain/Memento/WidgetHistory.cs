using Domain.Widget;

namespace Domain.Memento
{
    public class WidgetHistory
    {
        private readonly Stack<WidgetMemento> _history = new();

        public void Save(WidgetBase widget)
        {
            _history.Push(widget.CreateSnapshot());
        }

        public void Undo(WidgetBase widget)
        {
            if (_history.Count > 0)
            {
                var memento = _history.Pop();
                widget.Restore(memento);
            }
        }

        public void Clear()
        {
            _history.Clear();
        }
    }
}
