using System;

namespace ExternalSorting.Commands
{
    public abstract class Command : ICommand
    {
        protected virtual void OnCancellationRequested(EventArgs e)
        {
            var handler = CancellationRequested;
            handler?.Invoke(this, e);
        }

        public event EventHandler CancellationRequested;
        public abstract ICommandData Execute(bool canScheduleTask = false);
    }
}
