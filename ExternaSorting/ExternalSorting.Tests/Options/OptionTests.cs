using ExternalSorting.Optional;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace ExternalSorting.Tests.Options
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void Should_Throw_Exception_Accessing_Value_Of_Empty_Option()
        {
            Option<string> option = new Option<string>();
            option.HasValue.Should().BeFalse();
            Assert.Throws<InvalidOperationException>(() =>
            {
                var optionValue = option.Value;
            });
        }

        [Test]
        public void Should_Return_Option_Value()
        {
            Option<string> option = new Option<string>("test");
            option.HasValue.Should().BeTrue();
            option.Value.Should().Be("test");
        }
    }
}
