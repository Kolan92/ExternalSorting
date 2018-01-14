using System;

namespace ExternalSorting.Commands
{
    public class IncorrectCommand : HelpCommand
    {
        private readonly string userInput;

        public IncorrectCommand(string userInput)
        {
            this.userInput = userInput;
        }

        public override ICommandData Execute(bool canScheduleTask = true)
        {
            var helpMessage = base.Execute().CommandOutput;
            return new ContinueData($"Unrecognized command {userInput}{Environment.NewLine}{helpMessage}");
        }
    }
}