using ExternalSorting.Commands;
using ExternalSorting.Input;

namespace ExternalSorting
{
    public interface ICommandParser
    {
        ICommand ParseCommand(IInput input);
    }
    public class CommandParser : ICommandParser
    {
        public ICommand ParseCommand(IInput input)
        {
            switch (input.CommandName.ToLower())
            {
                case "e":
                case "exit":
                    return new ExitCommand();
                case "h":
                case "help":
                    return new HelpCommand();
                default:
                    return new IncorrectCommand(input.CommandName);

            }
        }
    }
}
