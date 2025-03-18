using Domain.Widget;

namespace Strategy
{
    public class StorageContext
    {
        public IWidgetStorageStrategy? Strategy { get; private set; }

        public void SetStrategy(IWidgetStorageStrategy strategy)
        {
            this.Strategy = strategy;
        }


        public async Task Save(IEnumerable<WidgetBase> widgets)
        {
            if (this.Strategy is not null)
            {
                await this.Strategy.Save(widgets);
            }
        }

        public async Task<List<WidgetBase>> Load()
        {
            return (await this.Strategy?.Load()) ?? [];
        }
    }
}