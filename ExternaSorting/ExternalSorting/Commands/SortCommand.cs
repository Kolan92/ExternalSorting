using ExternalSorting.SortTask;
using System.IO;
using System.Threading;

namespace ExternalSorting.Commands
{
    public class SortCommand : Command
    {
        private const string EmptyPath = "Sort command needs path argument";
        private const string NotAbbFile = "Only files with '.abb' extensions are supported";
        private const string CanNotScheduleAnotherSort = "Can not schedule file sort, while another sort is in progress";
        private const string SortingStarted = "Sorting file";
        private string IncorrectPath = "{0} is not correct path";
        private readonly string path;

        public SortCommand(string path)
        {
            this.path = path;
        }

        public override ICommandData Execute(bool canScheduleTask)
        {
            if(!canScheduleTask)
                return new ContinueData(CanNotScheduleAnotherSort);
            if(string.IsNullOrEmpty(path))
                return new ContinueData(EmptyPath);
            if(!IsAbbFile(path))
                return new ContinueData(NotAbbFile);
            if(!File.Exists(path))
                return new ContinueData(string.Format(IncorrectPath, path));

            var token = RunTask();
            return new SortData(SortingStarted, token);
        }

        private CancellationTokenSource RunTask()
        {
            var factory = new SortTaskFactory();
            return factory.StartNew(path);
        }

        private bool IsAbbFile(string path)
        {
            const int expectedExtensionLength = 3;
            return path.Substring(path.Length - expectedExtensionLength).Equals("abb");
        }
    }
}