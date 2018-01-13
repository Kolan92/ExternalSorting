using ExternalSorting.Commands;
using ExternalSorting.Input;
using ExternalSorting.Printer;

namespace ExternalSorting
{
    public class Shell
    {
        private readonly ICommandParser _commandParser;
        private readonly IInputParser _inputParser;
        private readonly IPrinter _printer;
        private bool _isRunning;

        public Shell(
            ICommandParser commandParser,
            IInputParser inputParser, 
            IPrinter printer)
        {
            _commandParser = commandParser;
            _inputParser = inputParser;
            _printer = printer;
            _isRunning = true;
        }

        public void Start()
        {
            var initialCommand = new HelpCommand();
            var helpMessage = initialCommand.Execute().CommandOutput;
            _printer.Print(helpMessage);

            do
            {
                var userInput = _inputParser.Parse();
                var currentCommand = _commandParser.ParseCommand(userInput);
                var commandData = currentCommand.Execute();

                _printer.Print(commandData.CommandOutput);
                _isRunning = commandData.ContinueExecution;
            } while (_isRunning);
        }
    }
}