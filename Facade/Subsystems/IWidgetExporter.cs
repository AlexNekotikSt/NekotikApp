using Domain.Widget;
using System.Text.Json;

namespace Facade
{
    public interface IWidgetExporter
    {
        string Export(IEnumerable<WidgetBase> widgets);
    }

    public class WidgetExporter : IWidgetExporter
    {
        public string Export(IEnumerable<WidgetBase> widgets)
        {
            return JsonSerializer.Serialize(widgets);
        }
    }
}
