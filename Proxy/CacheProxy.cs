using Domain.Widget;
using System.Collections.Concurrent;

namespace Proxy
{
    public class CacheProxy : IWidgetService
    {
        private readonly IWidgetService _inner;
        private readonly TimeSpan _ttl;
        private readonly ConcurrentDictionary<int, (WidgetBase Value, DateTime Expiry)> _itemCache = new();
        private (IEnumerable<WidgetBase> Value, DateTime Expiry)? _listCache;

        public CacheProxy(IWidgetService inner, TimeSpan ttl)
        {
            _inner = inner;
            _ttl = ttl;
        }

        public async Task<IEnumerable<WidgetBase>> GetAllAsync()
        {
            if (_listCache.HasValue && _listCache.Value.Expiry > DateTime.UtcNow)
                return _listCache.Value.Value;

            var list = await _inner.GetAllAsync();
            _listCache = (list, DateTime.UtcNow.Add(_ttl));
            return list;
        }

        public async Task<WidgetBase> GetByIdAsync(int id)
        {
            if (_itemCache.TryGetValue(id, out var entry) && entry.Expiry > DateTime.UtcNow)
                return entry.Value;

            var widget = await _inner.GetByIdAsync(id);
            if (widget != null)
                _itemCache[id] = (widget, DateTime.UtcNow.Add(_ttl));

            return widget;
        }

        public async Task SaveAsync(WidgetBase widget)
        {
            await _inner.SaveAsync(widget);
            _itemCache[widget.Id] = (widget, DateTime.UtcNow.Add(_ttl));
            _listCache = null;
        }

        public async Task DeleteAsync(int id)
        {
            await _inner.DeleteAsync(id);
            _itemCache.TryRemove(id, out _);
            _listCache = null;
        }
    }
}
