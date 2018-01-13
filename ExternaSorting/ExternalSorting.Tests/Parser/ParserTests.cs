using ExternalSorting.Commands;
using ExternalSorting.Input;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ExternalSorting.Tests.Parser
{
    [TestFixture]
    public class ParserTests
    {
        private CommandParser _commandParser;

        [SetUp]
        public void SetUp()
        {
            _commandParser = new CommandParser();
        }

        [TestCase("")]
        [TestCase("abb")]
        [TestCase("lkajsdf")]
        public void Should_Return_Unrecognized_Command_And_Help_Message(string userInput)
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns(userInput);

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<IncorrectCommand>();
            var data = command.Execute();
            data.CommandOutput.Should().Contain($"Unrecognized command {userInput}");
            data.ContinueExecution.Should().BeTrue();
        }

        [TestCase("help")]
        [TestCase("Help")]
        [TestCase("HELP")]
        [TestCase("HeLp")]
        [TestCase("h")]
        public void Should_Return_Help_Message(string userInput)
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns(userInput);

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<HelpCommand>();
            var data = command.Execute();
            var expectedHelpMessage = 
@"External Sort
This program allow to sort large files of float numbers.
File for sorting hast to have .abb extension.
Supported commands:
    h, help             Prints help
    s, sort     <path>  Sorts file in given path
    c, cancel           Breaks sorting step
    e, exit             Exit program";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }

        [TestCase("e")]
        [TestCase("E")]
        [TestCase("exit")]
        [TestCase("EXIT")]
        public void Should_Return_Exit_Message(string userInput)
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns(userInput);

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<ExitCommand>();
            var data = command.Execute();

            var expectedHelpMessage = @"Application terminated";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeFalse();
        }


        [TestCase("c")]
        [TestCase("C")]
        [TestCase("cancel")]
        [TestCase("CANCEL")]
        public void Should_Return_Cancel_Message(string userInput)
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns(userInput);

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<CancelCommand>();
            var data = command.Execute();

            var expectedHelpMessage = @"Sort cancel requested";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }
    }
}
