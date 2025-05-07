using Domain.Widget;
using Iterator;

namespace Interpreter
{
    public static class ProjectFilterEvaluator
    {
        public static List<WidgetBase> FilterWidgets(Project project, IExpression expression)
        {
            var result = new List<WidgetBase>();
            var iterator = project.GetIterator();

            while (iterator.MoveNext())
            {
                var current = iterator.Current;
                var context = new Dictionary<string, WidgetBase> { { current.Name, current } };
                if (expression.Interpret(context))
                {
                    result.Add(current);
                }
            }

            return result;
        }
    }


}
