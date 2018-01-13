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
            var message = command.Execute();
            message.Should().Contain($"Unrecognized command {userInput}");
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
            var message = command.Execute();
            var expectedHelpMessage = 
@"External Sort
This program allow to sort large files of float numbers.
File for sorting hast to have .abb extension.
Supported commands:
    h, help             Prints help
    s, sort     <path>  Sorts file in given path
    c, cancel           Breaks sorting step
    e, exit             Exit program";
            message.Should().Be(expectedHelpMessage);
        }
    }
}
