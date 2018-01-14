using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExternalSorting.SortTask
{
    public interface ISortStrategy
    {
        void Sort();
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
                .Select(line => float.Parse(line, CultureInfo.InvariantCulture.NumberFormat))
                .ToArray();

            Array.Sort(floats);
            File.WriteAllText(_filePath, string.Join(Environment.NewLine, floats));
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