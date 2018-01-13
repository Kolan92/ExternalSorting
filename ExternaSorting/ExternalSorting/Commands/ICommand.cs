namespace ExternalSorting.Commands
{
    public interface ICommand
    {
        ICommandData Execute();
    }
}
