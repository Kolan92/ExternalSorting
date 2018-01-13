using ExternalSorting.Input;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ExternalSorting.Tests.Input
{
    [TestFixture]
    public class InputTests
    {
        private IInputReader _mockInputReader;
        private IInputParser _parser;

        [SetUp]
        public void SetUp()
        {
            _mockInputReader = Substitute.For<IInputReader>();
            _parser = new InputParser(_mockInputReader);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("        ")]
        [TestCase("\t\t")]
        [TestCase("\r\n")]
        [TestCase("\n\r")]
        [TestCase("\r\n")]
        public void Should_Parse_Empty_String_As_Empty_CommandName(string consoleInput)
        {
            _mockInputReader.Read().Returns(consoleInput);
            var input = _parser.Parse();
            input.CommandName.Should().BeEmpty();
            input.CommandParameters.Should().BeEmpty();
        }

        [Test]
        public void Should_Parse_String_Without_Spaces_As_CommandName()
        {
            _mockInputReader.Read().Returns("test");
            var input = _parser.Parse();
            input.CommandName.Should().Be("test");
            input.CommandParameters.Should().BeEmpty();
        }

        [Test]
        public void Should_Parse_String_With_Spaces_As_Command_Name_And_Parameters()
        {
            _mockInputReader.Read().Returns("name parameters");
            var input = _parser.Parse();
            input.CommandName.Should().Be("name");
            input.CommandParameters.Should().Be("parameters");
        }
    }
}
