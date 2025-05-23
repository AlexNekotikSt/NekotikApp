﻿using Command;
using Command.Commands;
using Domain;
using Domain.Media;
using Domain.Memento;
using Domain.Widget;
using Facade;
using FactoryMethod.Core;
using Interpreter;
using Interpreter.Parser;
using Iterator;
using Proxy;
using Strategy;
using Strategy.Strategies;
using System.Linq.Expressions;
using Visitor;

namespace FactoryMethod
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //ExampleOfFactoryAndPrototype();
            //await ExampleOfStrategyMethod();
            //await ExampleOfCommand();
            //ExampleOfIterator();
            //ExampleOfInterpreter();
            //ExampleOfMemento();
            //ExampleOfVisitor();

            //await ExampleOfFacade();
            await ExampleOfProxy();
        }

        private static async Task ExampleOfProxy()
        {
            IWidgetService service = new RealWidgetService();
            service = new AuthProxy(service, new ConsoleUserContext());
            service = new CacheProxy(service, TimeSpan.FromSeconds(30));
            service = new LoggingProxy(service, new ConsoleLogger());

            var widget = new TextWidget { Id = 1, Name = "Sample", Text = "Hello World" };

            try
            {
                await service.SaveAsync(widget);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var all = await service.GetAllAsync();
            Console.WriteLine($"Widgets count: {all.Count()}");
        }

        private static async Task ExampleOfFacade()
        {
            var facade = new WidgetFacadeService();
            var widgets = new List<WidgetBase>
            {
                new TextWidget { Name = "Title", Text = "Invoice Report" },
                new NumericWidget { Name = "Amount", Value = 50 },
                new NumericWidget { Name = "Salary", Value = 50 },
                new NumericWidget { Name = "Amount", Value = 150 },
                new NumericWidget { Name = "Salary", Value = 200 },
                new NumericWidget { Name = "Amount", Value = 33 },
                new NumericWidget { Name = "Salary", Value = 50 },
                new NumericWidget { Name = "Amount", Value = 122 }
            };

            var textWidget = new TextWidget { Id = 1, Name = "ExampleWidget", Text = "Hello, Facade!" };

            if (!facade.ApplyValidation(textWidget))
            {
                Console.WriteLine("Віджет не пройшов валідацію.");
                return;
            }

            await facade.Save(textWidget);
            Console.WriteLine("Віджет збережено.");

            widgets.Add(textWidget);

            var stats = facade.GetStatistics(widgets);
            Console.WriteLine($"Загальна кількість віджетів: {stats.TotalWidgets}");

            var filtered = facade.Filter(widgets, "Title contains 'invoice' OR Amount > 100 OR Name contains 'Salary'");
            Console.WriteLine($"Кількість відфільтрованих: {filtered.Count()}");

            var exportJson = facade.Export(widgets);
            Console.WriteLine("Експорт у JSON:");
            Console.WriteLine(exportJson);

            var clone = facade.Clone(textWidget);
            Console.WriteLine($"Клоновано віджет з Id={clone.Id}, Name={clone.Name}");

            facade.UndoChanges(textWidget);
            Console.WriteLine("Відкат змін виконано.");
        }
        
        private static void ExampleOfVisitor()
        {
            var widgets = new List<WidgetBase>
            {
                new TextWidget { Text = "Hello world" },
                new NumericWidget { Value = 42 },
                new DateWidget { Date = DateTime.Today },
                new FileWidget { Medias =
                [
                    new MediaModel {Name = "File1" },
                    new MediaModel { Name = "File2" }
                ] }
            };

            var visitor = new StatisticVisitor();

            foreach (var widget in widgets)
            {
                widget.Accept(visitor);
            }

            Console.WriteLine($"Text symbols: {visitor.TextSymbolCount}");
            Console.WriteLine($"Numeric sum: {visitor.NumericSum}");
            Console.WriteLine($"Media files: {visitor.MediaFileCount}");
            Console.WriteLine($"Earliest date: {visitor.MinDate}");
        }


        private static void ExampleOfMemento()
        {
            var widget = new TextWidget { Id = 1, Name = "w1", Text = "Initial" };
            var history = new WidgetHistory();

            history.SaveState(widget);

            widget.Text = "Changed";
            history.SaveState(widget);

            widget.Text = "Broken";
            history.Undo(widget);
            history.Undo(widget);

        }

        private static void ExampleOfInterpreter()
        {
            var input = "Title contains 'invoice' OR Amount > 100 OR Name contains 'Salary'";
            var tokens = FilterTokenizer.Tokenize(input);
            var parser = new FilterParser(tokens);
            var expression = parser.ParseExpression();

            var project = new Project("Finance");
            project.AddToColumn1(new TextWidget { Name = "Title", Text = "Invoice Report" });
            project.AddToColumn2(new NumericWidget { Name = "Amount", Value = 50 });
            project.AddToColumn2(new NumericWidget { Name = "Salary", Value = 50 });
            project.AddToColumn3(new NumericWidget { Name = "Amount", Value = 150 });
            project.AddToColumn3(new NumericWidget { Name = "Salary", Value = 200 });
            project.AddToColumn3(new NumericWidget { Name = "Amount", Value = 33 });
            project.AddToColumn3(new NumericWidget { Name = "Salary", Value = 50 });
            project.AddToColumn3(new NumericWidget { Name = "Amount", Value = 122 });

            var filteredWidgets = ProjectFilterEvaluator.FilterWidgets(project, expression);

            foreach (var widget in filteredWidgets)
            {
                Console.WriteLine($"{widget.Name}: {widget.GetValue()}");
            }
        }

        private static void ExampleOfIterator()
        {
            var project = new Project("Demo Project");

            // Add widgets to the project
            project.AddToColumn1(new TextWidget { Id = 1, Name = "Text Widget 1" });
            project.AddToColumn2(new FileWidget { Id = 2, Name = "File Widget 1" });
            project.AddToColumn3(new NumericWidget { Id = 3, Name = "Numeric Widget 1" });
            project.AddToColumn1(new PictureWidget { Id = 4, Name = "Picture Widget 1" });


            foreach (var widget in project)
            {
                Console.WriteLine($"Widget: {widget.Name}, Type: {widget.GetType().Name}");
            }
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