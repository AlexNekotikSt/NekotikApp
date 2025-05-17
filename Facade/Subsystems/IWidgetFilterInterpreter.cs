using Domain.Widget;
using Interpreter;
using Interpreter.Parser;

namespace Facade.Subsystems
{
    public interface IWidgetFilterInterpreter
    {
        IEnumerable<WidgetBase> Interpret(IEnumerable<WidgetBase> widgets, string query);
    }

    public class WidgetFilterInterpreter : IWidgetFilterInterpreter
    {
        public IEnumerable<WidgetBase> Interpret(IEnumerable<WidgetBase> widgets, string query)
        {
            var tokens = FilterTokenizer.Tokenize(query);
            var parser = new FilterParser(tokens);
            var expression = parser.ParseExpression();

            return ProjectFilterEvaluator.FilterWidgets(widgets, expression);
        }
    }
}
