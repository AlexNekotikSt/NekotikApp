using Domain.Widget;
using Iterator;

namespace Interpreter
{
    public static class ProjectFilterEvaluator
    {
        public static List<WidgetBase> FilterWidgets(Project project, IExpression expression)
        {
            var result = new List<WidgetBase>();


            foreach(var widget in project)
            {
                var context = new Dictionary<string, WidgetBase> { { widget.Name, widget } };
                if (expression.Interpret(context))
                {
                    result.Add(widget);
                }
            }

            return result;
        }
    }


}
