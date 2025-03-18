using Domain;

namespace Command.Commands
{
    public class ClearCommand(ValueContext context) : ICommand
    {
        private readonly ValueContext context = context;

        public Task Execute()
        {
            context.Widgets.Clear();
            return Task.CompletedTask;
        }
    }
}
