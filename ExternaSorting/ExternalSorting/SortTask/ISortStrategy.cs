namespace ExternalSorting.SortTask
{
    public interface ISortStrategy
    {
        void Sort();
    }

    public abstract class FileSort
    {
        private readonly string _filePath;

        protected FileSort(string filePath)
        {
            _filePath = filePath;
        }
    }
    public class SingleFileSort : FileSort, ISortStrategy
    {
        public SingleFileSort(string filePath)
            : base(filePath)
        { }

        public void Sort()
        {
            throw new System.NotImplementedException();
        }
    }

    public class MultiFileSort : FileSort, ISortStrategy
    {
        public MultiFileSort(string filePath)
            : base(filePath)
        { }

        public void Sort()
        {
            throw new System.NotImplementedException();
        }
    }
}