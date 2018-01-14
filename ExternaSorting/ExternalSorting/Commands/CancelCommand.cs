using System;

namespace ExternalSorting.Commands
{
    public class CancelCommand : Command
    {
        private const string Message = "Sort cancel requested";

        public override ICommandData Execute(bool canScheduleTask = false)
        {
            OnCancellationRequested(EventArgs.Empty);
            return new ContinueData(Message);
        }
    }
}