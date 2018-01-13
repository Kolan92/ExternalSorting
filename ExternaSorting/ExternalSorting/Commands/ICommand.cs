namespace ExternalSorting.Commands
{
    public interface IState
    {
        bool ExitApplication { get; }
    }

    public interface ICommand
    {
        //IState GetNextState();
        string Execute();
    }
}
