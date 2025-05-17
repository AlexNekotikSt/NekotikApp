using Domain.Widget;

namespace Proxy
{
    public class RealWidgetService : IWidgetService
    {
        private readonly List<WidgetBase> _storage = new List<WidgetBase>();

        public Task<IEnumerable<WidgetBase>> GetAllAsync() => Task.FromResult(_storage.AsEnumerable());

        public Task<WidgetBase> GetByIdAsync(int id) =>
            Task.FromResult(_storage.FirstOrDefault(w => w.Id == id));

        public Task SaveAsync(WidgetBase widget)
        {
            _storage.RemoveAll(w => w.Id == widget.Id);
            _storage.Add(widget);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _storage.RemoveAll(w => w.Id == id);
            return Task.CompletedTask;
        }
    }
}
