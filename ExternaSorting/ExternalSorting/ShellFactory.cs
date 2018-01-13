using ExternalSorting.Input;
using ExternalSorting.Printer;

namespace ExternalSorting
{
    public static class ShellFactory
    {
        public static Shell DefaultShell()
        {
            var consoleReader = new ConsoleReader();
            var commandParser = new CommandParser();
            var inputParser = new InputParser(consoleReader);
            var consolePrinter = new ConsolePrinter();
            return new Shell(commandParser, inputParser, consolePrinter);
        }
    }
}
