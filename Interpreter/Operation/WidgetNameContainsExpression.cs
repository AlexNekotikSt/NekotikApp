using Domain.Widget;

namespace Interpreter
{
    public class WidgetNameContainsExpression : IExpression
    {
        private readonly string _expectedSubstring;

        public WidgetNameContainsExpression(string expectedSubstring)
        {
            _expectedSubstring = expectedSubstring;
        }

        public bool Interpret(Dictionary<string, WidgetBase> context)
        {
            return context.Values.Any(w => w.Name.Contains(_expectedSubstring, StringComparison.OrdinalIgnoreCase));
        }
    }

}
