using Domain.Widget;

namespace Interpreter
{
    public class AndExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndExpression(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public bool Interpret(Dictionary<string, WidgetBase> context)
        {
            return _left.Interpret(context) && _right.Interpret(context);
        }
    }
}
