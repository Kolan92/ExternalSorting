namespace ExternalSorting.Commands
{
    public interface ICommandData
    {
        string CommandOutput { get; }
        bool ContinueExecution { get; }
    }

    public abstract class CommandData : ICommandData
    {
        protected CommandData(string commandOutput)
        {
            CommandOutput = commandOutput;
        }

        public string CommandOutput { get; private set; }
        public abstract bool ContinueExecution { get; }
    }

    public class ExitData : CommandData
    {
        public override bool ContinueExecution => false;

        public ExitData(string commandOutput) : base(commandOutput)
        {
        }
    }

    public class ContinueData : CommandData
    {
        public override bool ContinueExecution => true;

        public ContinueData(string commandOutput) : base(commandOutput)
        {
        }
    }
}