using Domain.Widget;

namespace Interpreter
{
    public class NumericLessThanExpression : IExpression
    {
        private readonly string _widgetName;
        private readonly decimal _threshold;

        public NumericLessThanExpression(string widgetName, decimal threshold)
        {
            _widgetName = widgetName;
            _threshold = threshold;
        }

        public bool Interpret(Dictionary<string, WidgetBase> context)
        {
            if (context.TryGetValue(_widgetName, out var widget) && widget is NumericWidget numericWidget)
            {
                return numericWidget.Value < _threshold;
            }
            return false;
        }
    }

}
