namespace ExternalSorting.Commands
{
    public class ExitCommand : ICommand
    {
        private const string Message = "Application terminated";

        public ICommandData Execute()
        {
            return new ExitData(Message);
        }
    }
}