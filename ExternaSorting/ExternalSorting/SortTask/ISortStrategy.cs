using System;

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

    public class TempFileNumber
    {
        public float? Number { get; private set; }
        public int ReaderIndex { get; private set; }

        public TempFileNumber(string line, int index)
        {
            Number = string.IsNullOrEmpty(line) ? null : (float?) Convert.ToSingle(line);
            ReaderIndex = index;
        }
    }
}