using Domain.Widget;

namespace Proxy
{
    public class AuthProxy : IWidgetService
    {
        private readonly IWidgetService _inner;
        private readonly IUserContext _userContext;

        public AuthProxy(IWidgetService inner, IUserContext userContext)
        {
            _inner = inner;
            _userContext = userContext;
        }

        public async Task<IEnumerable<WidgetBase>> GetAllAsync()
        {
            EnsurePermission("read");
            return await _inner.GetAllAsync();
        }

        public async Task<WidgetBase> GetByIdAsync(int id)
        {
            EnsurePermission("read");
            return await _inner.GetByIdAsync(id);
        }

        public async Task SaveAsync(WidgetBase widget)
        {
            EnsurePermission("write");
            await _inner.SaveAsync(widget);
        }

        public async Task DeleteAsync(int id)
        {
            EnsurePermission("delete");
            await _inner.DeleteAsync(id);
        }

        private void EnsurePermission(string operation)
        {
            if (!_userContext.HasAccess(operation))
                throw new UnauthorizedAccessException($"User is not allowed to perform '{operation}' operation.");
        }
    }
}
