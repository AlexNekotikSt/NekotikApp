using Domain.Widget;
using System.Net.Http.Json;

namespace Bridge
{
    public class HttpWidgetService : IWidgetService
    {
        private readonly HttpClient _httpClient;

        public HttpWidgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WidgetBase>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("/api/widgets");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<WidgetBase>>();
        }

        public async Task<WidgetBase> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/widgets/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<WidgetBase>();
        }

        public async Task CreateAsync(WidgetBase widget)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/widgets", widget);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(WidgetBase widget)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/widgets/{widget.Id}", widget);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/widgets/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
