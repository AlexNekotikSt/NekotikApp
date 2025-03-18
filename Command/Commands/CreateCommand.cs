using Domain;
using Domain.Widget;
using FactoryMethod.Core;

namespace Command.Commands
{
    public class CreateCommand(ValueContext context, WidgetsFactory factory, WidgetType widgetType) : ICommand
    {
        private readonly ValueContext context = context;
        private readonly WidgetsFactory factory = factory;
        private readonly WidgetType widgetType = widgetType;

        public Task Execute()
        {
            context.Widgets.Add(factory.Create(widgetType));
            return Task.CompletedTask;
        }
    }
}
