using Domain;
using Strategy;

namespace Command.Commands
{
    public class LoadCommand(ValueContext context, StorageContext storageContext) : ICommand
    {
        private readonly ValueContext context = context;
        private readonly StorageContext storageContext = storageContext;

        public async Task Execute()
        {
            context.Widgets.AddRange(await storageContext.Load());
        }
    }
}
