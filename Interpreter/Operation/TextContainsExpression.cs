using Domain.Widget;

namespace Interpreter
{
    public class TextContainsExpression : IExpression
    {
        private readonly string _widgetName;
        private readonly string _expectedValue;

        public TextContainsExpression(string widgetName, string expectedValue)
        {
            _widgetName = widgetName;
            _expectedValue = expectedValue;
        }

        public bool Interpret(Dictionary<string, WidgetBase> context)
        {
            if (context.TryGetValue(_widgetName, out var widget) && widget is TextWidget textWidget)
            {
                return textWidget.Text?.Contains(_expectedValue, StringComparison.OrdinalIgnoreCase) == true;
            }
            return false;
        }
    }

}
