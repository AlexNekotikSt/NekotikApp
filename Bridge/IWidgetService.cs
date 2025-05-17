using Domain.Widget;

namespace Bridge
{
    public interface IWidgetService
    {
        Task<IEnumerable<WidgetBase>> GetAllAsync();
        Task<WidgetBase> GetByIdAsync(int id);
        Task CreateAsync(WidgetBase widget);
        Task UpdateAsync(WidgetBase widget);
        Task DeleteAsync(int id);
    }
}
