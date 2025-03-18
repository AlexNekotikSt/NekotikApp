using Domain.Widget;
using FactoryMethod.Core;
using Strategy;
using Strategy.Strategies;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //ExampleOfFactoryAndPrototype();

            await ExampleOfStrategyMethod();
        }

        private static async Task ExampleOfStrategyMethod()
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

            StorageContext storage = new();
            storage.SetStrategy(new JsonWidgetStorageStrategy());

            var jsonWidgets = widgets.Select(widget => widget.Clone())
                .Cast<WidgetBase>()
                .Select(widget =>
                {
                    widget.Name = $"{widget.Name} (JsonWidgetStorageStrategy)";
                    return widget;
                });

            await storage.Save(jsonWidgets);

            jsonWidgets = await storage.Load();

            storage.SetStrategy(new XmlWidgetStorageStrategy());

            var xmlWidgets = widgets.Select(widget => widget.Clone())
                .Cast<WidgetBase>()
                .Select(widget =>
                {
                    widget.Name = $"{widget.Name} (XmlWidgetStorageStrategy)";
                    return widget;
                });

            await storage.Save(xmlWidgets);

            xmlWidgets = await storage.Load();

            ShowWidget(jsonWidgets);
            ShowWidget(xmlWidgets);
        }

        private static void ShowWidget(IEnumerable<WidgetBase> widgets)
        {
            foreach (var widget in widgets)
            {
                Console.WriteLine($"{widget.Name} - {widget.GetValue()}");
            }
        }

        private static void ExampleOfFactoryAndPrototype()
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

            ShowWidget(widgets);
            ShowWidget(cloned);
        }
    }
}