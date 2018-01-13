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

        public override string Execute()
        {
            var helpMessage = base.Execute();
            return $"Unrecognized command {userInput}{Environment.NewLine}{helpMessage}";
        }
    }
}