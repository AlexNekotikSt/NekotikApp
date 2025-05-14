using Domain.Widget;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Memento
{
    public interface IMemento
    {
        public WidgetMemento CreateSnapshot();

        public void Restore(WidgetMemento memento);
    }
}
