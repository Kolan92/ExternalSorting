namespace ExternalSorting.SortTask
{
    public interface ISortStrategyFactory
    {
        ISortStrategy ChooseSortStrategy(string filePath);
    }
}