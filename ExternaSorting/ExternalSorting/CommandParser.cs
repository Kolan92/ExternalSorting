using ExternalSorting.Commands;
using ExternalSorting.Input;

namespace ExternalSorting
{
    public class CommandParser
    {
        public ICommand ParseCommand(IInput input)
        {
            switch (input.CommandName.ToLower())
            {
                case "h":
                case "help":
                    return new HelpCommand();
                default:
                    return new IncorrectCommand(input.CommandName);

            }
        }
    }
}
