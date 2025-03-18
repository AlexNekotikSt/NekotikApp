using Domain.Widget;

namespace FactoryMethod.Core
{
    public abstract class WidgetFactory
    {
        public abstract WidgetBase CreateEmpty();
    }
}
