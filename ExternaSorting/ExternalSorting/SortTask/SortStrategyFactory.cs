namespace ExternalSorting.SortTask
{
    public class SortStrategyFactory : ISortStrategyFactory
    {
        private readonly IMemoryChecker _memoryChecker;

        public SortStrategyFactory(IMemoryChecker memoryChecker)
        {
            _memoryChecker = memoryChecker;
        }

        public ISortStrategy ChooseSortStrategy(string filePath)
        {
            if(_memoryChecker.CanFitWholeFileToMemory(filePath))
                return new SingleFileSort(filePath);
            return new MultiFileSort(filePath);
        }
    }
}