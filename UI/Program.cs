
using Bridge;  

namespace WidgetConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IWidgetService rawService = new HttpWidgetService(new HttpClient { BaseAddress = new Uri("http://localhost:5132") });

            IWidgetService widgetService = new WidgetServiceBridge(rawService);

            var widgets = await widgetService.GetAllAsync();
            foreach (var w in widgets)
                Console.WriteLine($"  • [{w.Id}] {w.Name}");


            Console.Write("\nВведіть ID для деталізації: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var single = await widgetService.GetByIdAsync(id);
                if (single != null)
                    Console.WriteLine($"\n📋 Віджет {single.Id}:\n  Назва: {single.Name}\n  Колонка: {single.Column}");
                else
                    Console.WriteLine("❌ Віджет не знайдено.");
            }

            Console.ReadKey();
        }
    }
}
