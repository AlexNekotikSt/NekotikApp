using Domain.Widget;

namespace Facade.Subsystems
{
    public interface IWidgetValidator
    {
        bool Validate(WidgetBase widget);
    }

    public class WidgetValidator : IWidgetValidator
    {
        public bool Validate(WidgetBase widget)
        {
            return !string.IsNullOrWhiteSpace(widget?.Name);
        }
    }
}
