using ExternalSorting.Input;
using System;

namespace ExternalSorting
{
    public class Shell
    {
        private readonly CommandParser _commandParser;
        private readonly InputParser _inputParser;
        private bool _isRunning;

        public Shell()
        {
            _commandParser = new CommandParser();
            var consoleReader = new ConsoleReader();
            _inputParser = new InputParser(consoleReader);
            _isRunning = true;
        }

        public void Start()
        {
            while (_isRunning)
            {
                var userInput = _inputParser.Parse();
                var currentCommand = _commandParser.ParseCommand(userInput);
                var commandOutput = currentCommand.Execute();
                Console.WriteLine(commandOutput);
            }
        }
    }
}