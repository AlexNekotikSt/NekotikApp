using Domain.Widget;

namespace Bridge
{
    public class LocalWidgetService : IWidgetService
    {
        private readonly List<WidgetBase> _storage = [
                new TextWidget
                {
                    Id = 1,
                    Name = "Title",
                    Text = "Invoice Report"
                },
                new NumericWidget
                {
                    Id = 2,
                    Name = "Amount",
                    Value = 50
                },
                new NumericWidget
                {
                    Id= 3,
                    Name = "Salary",
                    Value = 50
                },
                new NumericWidget
                {
                    Id= 4,
                    Name = "Amount",
                    Value = 150
                },
                new NumericWidget
                {
                    Id= 5,
                    Name = "Salary",
                    Value = 200
                },
                new NumericWidget
                {
                    Id= 6,
                    Name = "Amount",
                    Value = 33
                },
                new NumericWidget
                {
                    Id= 7,
                    Name = "Salary",
                    Value = 50
                },
                new NumericWidget
                {
                    Id= 8,
                    Name = "Amount",
                    Value = 122
                }];

        public Task<IEnumerable<WidgetBase>> GetAllAsync() => Task.FromResult(_storage.AsEnumerable());

        public Task<WidgetBase> GetByIdAsync(int id) =>
            Task.FromResult(_storage.FirstOrDefault(w => w.Id == id));

        public Task CreateAsync(WidgetBase widget)
        {
            _storage.Add(widget);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _storage.RemoveAll(w => w.Id == id);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(WidgetBase widget)
        {
            var widgetToUpdate = _storage.FirstOrDefault(x => x.Id == widget.Id);

            if (widgetToUpdate is null)
            {
                return Task.CompletedTask;
            }

            _storage.Remove(widgetToUpdate);
            _storage.Add(widget);

            return Task.CompletedTask;

        }
    }
}
