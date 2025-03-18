using Domain;
using Strategy;

namespace Command.Commands
{
    public class SaveCommand(ValueContext context, StorageContext storageContext) : ICommand
    {
        private readonly ValueContext context = context;
        private readonly StorageContext storageContext = storageContext;

        public Task Execute()
        {
           return storageContext.Save(context.Widgets);
        }
    }
}
