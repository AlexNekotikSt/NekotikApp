using Domain;

namespace Command.Commands
{
    public class DisplayCommand(ValueContext context) : ICommand
    {
        private readonly ValueContext context = context;

        public Task Execute()
        {
            foreach (var widget in context.Widgets)
            {
                Console.WriteLine($"{widget.Name} - {widget.GetValue()}");
            }

            return Task.CompletedTask;
        }
    }
}
