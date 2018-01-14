using System;
using System.IO;
using System.Linq;

namespace ExternalSorting.SortTask
{
    public interface ISortStrategy
    {
        void Sort();
        void CleanUp();
    }

    public abstract class FileSort
    {
        protected readonly string _filePath;

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
            var floats = File.ReadAllLines(_filePath)
                .Select(line => Convert.ToSingle(line))
                .ToArray();

            Array.Sort(floats);
            File.WriteAllText(_filePath, string.Join(Environment.NewLine, floats));
        }

        public void CleanUp()
        {
            //do nothing, nothing to clean
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

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}