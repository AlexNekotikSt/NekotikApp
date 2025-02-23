using Domain.Widget;

namespace FactoryMethod.Impl
{
    public class DateWidgetFactory : GenericWidgetFactory<DateWidget>
    {
        protected override DateWidget SetValue(DateWidget widget)
        {
            widget.Date = DateTime.UtcNow;
            return widget;
        }
    }
}
