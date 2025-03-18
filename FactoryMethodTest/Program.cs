using Command;
using Command.Commands;
using Domain;
using Domain.Widget;
using FactoryMethod.Core;
using Strategy;
using Strategy.Strategies;

namespace FactoryMethod
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //ExampleOfFactoryAndPrototype();
            //await ExampleOfStrategyMethod();
            await ExampleOfCommand();
        }

        private static async Task ExampleOfCommand()
        {
            var invoker = new Invoker();
            var factory = new WidgetsFactory();
            var widgetContext = new ValueContext();
            var storage = new StorageContext();
            storage.SetStrategy(new JsonWidgetStorageStrategy());

            var key = "1";
            var isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("");
                Console.WriteLine("1 - Enqueue create new widget");
                Console.WriteLine("2 - Enqueue save");
                Console.WriteLine("3 - Enqueue load saved");
                Console.WriteLine("4 - Enqueue show");
                Console.WriteLine("5 - Enqueue flush loaded widgets");
                Console.WriteLine("6 - run enqueued commands");
                Console.WriteLine("8 - set Json as storage");
                Console.WriteLine("9 - set XML as storage");
                Console.WriteLine("0 - close");
                Console.Write("Enter command >>> ");

                key = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine("");

                switch (key)
                {
                    case "1":
                        {
                            WidgetChooseMenu(invoker, factory, widgetContext);
                            break;
                        }
                    case "2":
                        {
                            invoker.AddCommand(new SaveCommand(widgetContext, storage));
                            break;
                        }
                    case "3":
                        {
                            invoker.AddCommand(new LoadCommand(widgetContext, storage));
                            break;
                        }
                    case "4":
                        {
                            invoker.AddCommand(new DisplayCommand(widgetContext));
                            break;
                        }

                    case "5":
                        {
                            invoker.AddCommand(new ClearCommand(widgetContext));
                            break;
                        }

                    case "6":
                        {
                            Console.Clear();
                            await invoker.ExecuteCommands();
                            break;
                        }
                    case "8":
                        {
                            invoker.AddCommand(new SetStorageCommand(storage, new JsonWidgetStorageStrategy()));
                            break;
                        }
                    case "9":
                        {
                            invoker.AddCommand(new SetStorageCommand(storage, new XmlWidgetStorageStrategy()));
                            break;
                        }

                    case "0":
                        {
                            isRunning = false;
                            break;
                        }

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        private static void WidgetChooseMenu(Invoker invoker, WidgetsFactory factory, ValueContext widgetContext)
        {
            Console.WriteLine("-----");
            Console.WriteLine("1 - Text");
            Console.WriteLine("2 - Date");
            Console.WriteLine("3 - Numeric");
            Console.WriteLine("4 - File");
            Console.WriteLine("5 - Picture");
            Console.Write("Enter widget type >>> ");

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    {
                        invoker.AddCommand(new CreateCommand(widgetContext, factory, WidgetType.Text));
                        break;
                    }
                case '2':
                    {
                        invoker.AddCommand(new CreateCommand(widgetContext, factory, WidgetType.Date));
                        break;
                    }
                case '3':
                    {
                        invoker.AddCommand(new CreateCommand(widgetContext, factory, WidgetType.Numeric));
                        break;
                    }
                case '4':
                    {
                        invoker.AddCommand(new CreateCommand(widgetContext, factory, WidgetType.File));
                        break;
                    }
                case '5':
                    {
                        invoker.AddCommand(new CreateCommand(widgetContext, factory, WidgetType.Picture));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid widget Type");
                        break;
                    }
            }

            Console.WriteLine("");
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