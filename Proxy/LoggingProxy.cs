using Domain.Widget;

namespace Proxy
{
    public class LoggingProxy : IWidgetService
    {
        private readonly IWidgetService _inner;
        private readonly ILogger _logger;

        public LoggingProxy(IWidgetService inner, ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<IEnumerable<WidgetBase>> GetAllAsync()
        {
            _logger.Info("GetAllAsync called");
            var start = DateTime.UtcNow;
            var result = await _inner.GetAllAsync();
            _logger.Info($"GetAllAsync completed in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
            return result;
        }

        public async Task<WidgetBase> GetByIdAsync(int id)
        {
            _logger.Info($"GetByIdAsync called with id={id}");
            var start = DateTime.UtcNow;
            var result = await _inner.GetByIdAsync(id);
            _logger.Info($"GetByIdAsync completed in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
            return result;
        }

        public async Task SaveAsync(WidgetBase widget)
        {
            _logger.Info($"SaveAsync called for widget id={widget.Id}");
            var start = DateTime.UtcNow;
            await _inner.SaveAsync(widget);
            _logger.Info($"SaveAsync completed in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
        }

        public async Task DeleteAsync(int id)
        {
            _logger.Info($"DeleteAsync called for widget id={id}");
            var start = DateTime.UtcNow;
            await _inner.DeleteAsync(id);
            _logger.Info($"DeleteAsync completed in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
        }
    }
}
