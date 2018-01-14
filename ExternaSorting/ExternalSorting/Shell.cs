using ExternalSorting.Commands;
using ExternalSorting.Input;
using ExternalSorting.Optional;
using ExternalSorting.Printer;
using System;
using System.Threading;

namespace ExternalSorting
{
    public class Shell
    {
        private readonly ICommandParser _commandParser;
        private readonly IInputParser _inputParser;
        private readonly IPrinter _printer;

        private Option<CancellationTokenSource> _cancellationToken;
        private bool CanScheduleTask => !_cancellationToken.HasValue;
        private bool _isRunning;

        public Shell(
            ICommandParser commandParser,
            IInputParser inputParser, 
            IPrinter printer)
        {
            _commandParser = commandParser;
            _inputParser = inputParser;
            _printer = printer;
            _cancellationToken = new Option<CancellationTokenSource>();
            _isRunning = true;
        }

        public void Start()
        {
            var initialCommand = new HelpCommand();
            var helpMessage = initialCommand.Execute();
            _printer.Print(helpMessage.CommandOutput);

            do
            {
                var userInput = _inputParser.Parse();
                var currentCommand = _commandParser.ParseCommand(userInput);
                currentCommand.CancellationRequested += CancellationRequested;

                if (CanScheduleTask)
                    currentCommand.SortFinished += SortFinished;

                var commandData = currentCommand.Execute(CanScheduleTask);

                _printer.Print(commandData.CommandOutput);
                _isRunning = commandData.ContinueExecution;

                if(commandData.Token.HasValue)
                    _cancellationToken = commandData.Token;

            } while (_isRunning);
        }


        private void CancellationRequested(object sender, EventArgs e)
        {
            if (_cancellationToken.HasValue)
            {
                _cancellationToken.Value.Cancel();
                _cancellationToken = new Option<CancellationTokenSource>();
            }
        }

        private void SortFinished(object sender, SortFinishedArgs e)
        {
            if (e.IsSuccess)
                _printer.Print("Sorting finished with success");
            else
            {
                _printer.Print($"Sorting finished with error: {e.Exception}");
            }

            if (_cancellationToken.HasValue)
            {
                _cancellationToken = new Option<CancellationTokenSource>();
            }
        }
    }
}