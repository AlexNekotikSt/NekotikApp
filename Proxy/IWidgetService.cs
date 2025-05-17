using Domain.Widget;

namespace Proxy
{
    public interface IWidgetService
    {
        Task<IEnumerable<WidgetBase>> GetAllAsync();
        Task<WidgetBase> GetByIdAsync(int id);
        Task SaveAsync(WidgetBase widget);
        Task DeleteAsync(int id);
    }
}
