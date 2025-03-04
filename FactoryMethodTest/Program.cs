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

            var cloned = widgets.Select(widget => widget.Clone())
                .Cast<WidgetBase>()
                .Select(widget =>
                {
                    widget.Name = $"{widget.Name} (cloned)";
                    return widget;
                })
                .ToList();


            Console.WriteLine("Original Widgets:");
            foreach (var widget in widgets)
            {
                Console.WriteLine($"{widget.Name} - {widget.GetValue()}");
            }
            
            Console.WriteLine("Cloned Widgets:");
            foreach(var widget in cloned)
            {
                Console.WriteLine($"{widget.Name} - {widget.GetValue()}");
            }
        }

    }
}