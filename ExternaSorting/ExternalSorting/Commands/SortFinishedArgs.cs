using System;

namespace ExternalSorting.Commands
{
    public class SortFinishedArgs : EventArgs
    {
        public SortFinishedArgs(Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
        }

        public SortFinishedArgs()
        {
            IsSuccess = true;
        }

        public bool IsSuccess { get; private set; }
        public Exception Exception { get; private set; }
    }
}
