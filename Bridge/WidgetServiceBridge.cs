using Domain.Widget;

namespace Bridge
{
    public class WidgetServiceBridge : IWidgetService
    {
        private readonly IWidgetService _implementation;

        public WidgetServiceBridge(IWidgetService implementation)
        {
            _implementation = implementation;
        }

        public Task<IEnumerable<WidgetBase>> GetAllAsync()
            => _implementation.GetAllAsync();

        public Task<WidgetBase> GetByIdAsync(int id)
            => _implementation.GetByIdAsync(id);

        public Task CreateAsync(WidgetBase widget)
            => _implementation.CreateAsync(widget);

        public Task UpdateAsync(WidgetBase widget)
            => _implementation.UpdateAsync(widget);

        public Task DeleteAsync(int id)
            => _implementation.DeleteAsync(id);
    }
}
