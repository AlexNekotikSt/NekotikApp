using Domain.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Memento
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
