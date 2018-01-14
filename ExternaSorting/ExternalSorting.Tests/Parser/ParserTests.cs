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

        [TestCase("s")]
        [TestCase("S")]
        [TestCase("sort")]
        [TestCase("SORT")]
        public void Should_Return_Sort_Message_Needs_Path_Argument(string userInput)
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns(userInput);

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<SortCommand>();
            var data = command.Execute(true);

            var expectedHelpMessage = @"Sort command needs path argument";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }

        [Test]
        public void Should_Return_Not_Supported_File_Type()
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns("sort");
            inputSubstitute.CommandParameters.Returns("some.txt");

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<SortCommand>();
            var data = command.Execute(true);

            var expectedHelpMessage = "Only files with '.abb' extensions are supported";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }

        [Test]
        public void Should_Return_Message_Path_Is_Incorrect()
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns("sort");
            inputSubstitute.CommandParameters.Returns("not_a_path.abb");

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<SortCommand>();
            var data = command.Execute(true);

            var expectedHelpMessage = $"{inputSubstitute.CommandParameters} is not correct path";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }

        [Test]
        public void Should_Return_Can_Not_Schedule_Sort_When_Another_Is_In_Progress()
        {
            var inputSubstitute = Substitute.For<IInput>();
            inputSubstitute.CommandName.Returns("sort");

            var command = _commandParser.ParseCommand(inputSubstitute);
            command.Should().BeOfType<SortCommand>();
            var data = command.Execute(false);

            var expectedHelpMessage = "Can not schedule file sort, while another sort is in progress";
            data.CommandOutput.Should().Be(expectedHelpMessage);
            data.ContinueExecution.Should().BeTrue();
        }
    }
}
