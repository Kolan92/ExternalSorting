using ExternalSorting.Input;
using ExternalSorting.Printer;
using NSubstitute;
using NUnit.Framework;

namespace ExternalSorting.Tests.Shell
{
    [TestFixture]
    public class ShellTests
    {
        private ExternalSorting.Shell _shell;
        private IInputReader _inputReader;
        private IPrinter _printer;

        [SetUp]
        public void SetUp()
        {
            _inputReader = Substitute.For<IInputReader>();
            _printer = Substitute.For<IPrinter>();

            var commandParser = new CommandParser();
            var inputParser = new InputParser(_inputReader);

            _shell = new ExternalSorting.Shell(commandParser, inputParser, _printer);

        }

        [Test]
        public void Should_Print_Exit_Command()
        {
            _inputReader.Read().Returns("e");
            _shell.Start();
            _printer.Received(1).Print("Application terminated");
        }
    }
}
