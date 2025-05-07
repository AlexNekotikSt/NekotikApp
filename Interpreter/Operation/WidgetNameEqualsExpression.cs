using Domain.Widget;

namespace Interpreter
{
    public class WidgetNameEqualsExpression : IExpression
    {
        private readonly string _expectedName;

        public WidgetNameEqualsExpression(string expectedName)
        {
            _expectedName = expectedName;
        }

        public bool Interpret(Dictionary<string, WidgetBase> context)
        {
            return context.Values.Any(w => string.Equals(w.Name, _expectedName, StringComparison.OrdinalIgnoreCase));
        }
    }

}
