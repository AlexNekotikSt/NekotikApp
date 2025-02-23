using Domain.Widget;
using FactoryMethod.Core;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {

            WidgetType[] widgetTypes =
            [
                WidgetType.Text,
                WidgetType.Text,
                WidgetType.Date,
                WidgetType.Numeric,
                WidgetType.File,
            ];


            var factory = new WidgetsFactory();

            var widgets = widgetTypes
                .Select(factory.Create)
                .ToList();

            foreach (var widget in widgets)
            {
                Console.WriteLine($"{widget.Name} - {widget.GetValue()}");
            }
        }

    }
}