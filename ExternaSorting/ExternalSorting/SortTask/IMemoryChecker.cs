namespace ExternalSorting.SortTask
{
    public interface IMemoryChecker
    {
        bool CanFitWholeFileToMemory(string filePath);
    }

    public class MemoryChecker : IMemoryChecker
    {
        public bool CanFitWholeFileToMemory(string filePath)
        {
            return true;
        }
    }
}