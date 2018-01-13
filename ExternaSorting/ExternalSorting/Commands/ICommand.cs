using System;

namespace ExternalSorting.Commands
{
    public interface ICommand
    {
        ICommandData Execute();
        event EventHandler CancellationRequested;
    }
}
