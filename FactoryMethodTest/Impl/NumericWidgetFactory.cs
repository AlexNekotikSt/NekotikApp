using Domain.Widget;

namespace FactoryMethod.Impl
{
    public class NumericWidgetFactory : GenericWidgetFactory<NumericWidget>
    {
        protected override NumericWidget SetValue(NumericWidget widget)
        {
            widget.Value = _id * 10;

            return widget;
        }
    }

}
