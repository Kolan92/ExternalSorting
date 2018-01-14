using System;

namespace ExternalSorting.Commands
{
    public class ExitCommand : Command
    {
        private const string Message = "Application terminated";

        public override ICommandData Execute(bool canScheduleTask = true)
        {
            OnCancellationRequested(EventArgs.Empty);
            return new ExitData(Message);
        }
    }
}