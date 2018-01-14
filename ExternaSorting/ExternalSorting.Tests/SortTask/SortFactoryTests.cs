using ExternalSorting.SortTask;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ExternalSorting.Tests.SortTask
{
    [TestFixture]
    public class SortFactoryTests
    {
        [Test]
        public void Should_Return_Single_File_Sort_If_File_Can_Fit_Into_Memory()
        {
            var memoryChecker = Substitute.For<IMemoryChecker>();
            memoryChecker.CanFitWholeFileToMemory(Arg.Any<string>()).Returns(true);

            var strategyFactory = new SortStrategyFactory(memoryChecker);
            var strategy = strategyFactory.ChooseSortStrategy("pathToFile");
            strategy.Should().BeOfType<SingleFileSort>();
        }
    }
}
