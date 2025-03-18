using Strategy;

namespace Command.Commands
{
    public class SetStorageCommand(StorageContext storageContext, IWidgetStorageStrategy widgetStorageStrategy) : ICommand
    {
        private readonly StorageContext storageContext = storageContext;
        private readonly IWidgetStorageStrategy widgetStorageStrategy = widgetStorageStrategy;

        public Task Execute()
        {
            storageContext.SetStrategy(widgetStorageStrategy);
            return Task.CompletedTask;
        }
    }
}
