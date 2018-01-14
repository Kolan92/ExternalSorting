using System;

namespace ExternalSorting.Commands
{
    public interface ICommand
    {
        ICommandData Execute(bool canScheduleTask = false);
        event EventHandler CancellationRequested;
    }
}
