using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalSorting.SortTask
{
    public class MultiFileSort : FileSort, ISortStrategy
    {
        private const int HundredMegaBytes = 134217728/100;

        private readonly Guid SortId;
        private readonly string WorkingDirectory;

        private string TempFileName => $"{WorkingDirectory}temp{SortId}_{{0}}.abb";
        private string SortedFile => $"{WorkingDirectory}temp_sorted.abb";

        public MultiFileSort(string filePath)
            : base(filePath)
        {
            SortId = Guid.NewGuid();
            var directoryInfo = new DirectoryInfo(_filePath);
            WorkingDirectory = _filePath.Substring(0, _filePath.LastIndexOf(directoryInfo.Name));
        }

        public void Sort()
        {
            SplitFile();
            SortChunks();
            MergeSort();
            DeleteOldFileAndRenameSorted();
        }

        private void SplitFile()
        {
            void LogSplitFile(int fileNumber) =>
                Console.WriteLine($"Splitting {_filePath} to file number {fileNumber}");

            var splitIndex = 1;
            LogSplitFile(splitIndex);
            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(string.Format(TempFileName, splitIndex));
                using (var streamReader = new StreamReader(_filePath))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        streamWriter.WriteLine(streamReader.ReadLine());
                        if (streamWriter.BaseStream.Length > HundredMegaBytes && streamReader.Peek() >= 0)
                        {
                            streamWriter.Close();
                            splitIndex++;
                            LogSplitFile(splitIndex);
                            streamWriter = new StreamWriter(string.Format(TempFileName, splitIndex));
                        }
                    }
                }
            }
            finally
            {
                streamWriter?.Dispose();
            }
        }

        private void SortChunks()
        {
            void LogStartSorting(string fileName) =>
                Console.WriteLine($"Started sorting file {fileName}");

            void LogFinishedSorting(string fileName) =>
                Console.WriteLine($"Finished sorting file {fileName}");

            var tempFilePaths = GetTempFilePaths();

            Parallel.ForEach(tempFilePaths,
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 }, 
                currentFile =>
                {
                    try
                    {
                        LogStartSorting(currentFile);
                        var floats = File.ReadAllLines(currentFile)
                            .Select(line => Convert.ToSingle(line))
                            .ToArray();

                        Array.Sort(floats);
                        File.WriteAllText(currentFile, string.Join(Environment.NewLine, floats));
                        LogFinishedSorting(currentFile);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                });
        }

        private void MergeSort()
        {
            var tempFilePaths = GetTempFilePaths();
            var tempFilesReaders = tempFilePaths.Select(file => new StreamReader(file))
                .ToArray();

            Console.WriteLine("Start merge sort of temp files");
            using (var sortedFileStreamWriter = new StreamWriter(SortedFile))
            {
                try
                {
                    var readLines = tempFilesReaders.Select(reader => reader.ReadLine())
                        .Select((line, index) => new TempFileNumber(line, index))
                        .ToArray();

                    while (readLines.Any(line => line.Number.HasValue))
                    {
                        var minValue = readLines.Where(x => x.Number.HasValue)
                            .Aggregate((a, b) => a.Number < b.Number ? a : b);

                        sortedFileStreamWriter.WriteLine(minValue.Number.Value);

                        var line = tempFilesReaders[minValue.ReaderIndex].ReadLine();
                        readLines[minValue.ReaderIndex] = new TempFileNumber(line, minValue.ReaderIndex);
                    }

                }
                finally
                {
                    foreach (var reader in tempFilesReaders)
                        reader.Dispose();
                }
            }
        }

        private void DeleteOldFileAndRenameSorted()
        {
            File.Delete(_filePath);
            File.Move(SortedFile, _filePath);
        }

        private string[] GetTempFilePaths()
        {
            return Directory.GetFiles(WorkingDirectory, $"temp{SortId}*.abb");
        }

        public void CleanUp()
        {
            foreach (var tempFilePath in GetTempFilePaths())
                File.Delete(tempFilePath);
        }
    }
}