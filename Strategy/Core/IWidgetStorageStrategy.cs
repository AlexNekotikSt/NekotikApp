using Domain.Widget;

namespace Strategy
{
    public interface IWidgetStorageStrategy
    {
        Task Save(IEnumerable<WidgetBase> widgets);
        Task<List<WidgetBase>> Load();
    }
}
