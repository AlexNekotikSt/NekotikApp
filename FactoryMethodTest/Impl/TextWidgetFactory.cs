using Domain.Widget;

namespace FactoryMethod.Impl
{
    public class TextWidgetFactory : GenericWidgetFactory<TextWidget>
    {
        protected override TextWidget SetValue(TextWidget widget)
        {
            widget.Text = $"Text {widget.Id}";

            return widget;
        }
    }

}
