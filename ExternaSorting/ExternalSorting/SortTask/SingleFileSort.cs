using System;
using System.IO;
using System.Linq;

namespace ExternalSorting.SortTask
{
    public class SingleFileSort : FileSort, ISortStrategy
    {
        public SingleFileSort(string filePath)
            : base(filePath)
        { }

        public void Sort()
        {
            var floats = File.ReadAllLines(_filePath)
                .Select(line => Convert.ToSingle((string) line))
                .ToArray();

            Array.Sort(floats);
            File.WriteAllText(_filePath, string.Join(Environment.NewLine, floats));
        }

        public void CleanUp()
        {
            //do nothing, nothing to clean
        }
    }
}