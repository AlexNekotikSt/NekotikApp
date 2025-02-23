using Domain.Widget;
using FactoryMethod.Core;

namespace FactoryMethod.Impl
{
    public abstract class GenericWidgetFactory<TWidget> : WidgetFactory
        where TWidget : WidgetBase, new()
    {
        protected int _id = 0;

        protected abstract TWidget SetValue(TWidget widget);

        public override WidgetBase CreateEmpty()
        {
            return SetValue(new TWidget
            {
                Id = ++_id,
                Name = $"Widget {_id}",
                Column = 0,
                Order = _id,
            });
        }
    }
}
