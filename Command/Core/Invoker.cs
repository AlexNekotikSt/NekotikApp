namespace Command
{
    public class Invoker
    {
        private readonly Queue<ICommand> _commands = [];
       
        public void AddCommand(ICommand command)
        {
            _commands.Enqueue(command);
        }

        public async Task ExecuteCommands()
        {
            while(_commands.TryDequeue(out var command))
            {
                await command.Execute();
            }
        }
    }
}
