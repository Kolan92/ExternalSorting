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

        protected virtual void OnSortFinished(SortFinishedArgs e)
        {
            var handler = SortFinished;
            handler?.Invoke(this, e);
        }

        public event EventHandler CancellationRequested;
        public event EventHandler<SortFinishedArgs> SortFinished;
        public abstract ICommandData Execute(bool canScheduleTask = false);
    }
}
