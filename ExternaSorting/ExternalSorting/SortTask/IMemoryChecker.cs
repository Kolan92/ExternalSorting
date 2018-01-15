using System.Diagnostics;

namespace ExternalSorting.SortTask
{
    public interface IMemoryChecker
    {
        bool CanFitWholeFileToMemory(string filePath);
    }

    public class MemoryData
    {
        public bool CanReadWholeFile { get; private set; }
        public int AvailableMemoryMb { get; private set; }

        public MemoryData(bool canReadWholeFile, int availableMemoryMb)
        {
            CanReadWholeFile = canReadWholeFile;
            AvailableMemoryMb = availableMemoryMb;
        }
    }

    public class MemoryChecker : IMemoryChecker
    {
        public bool CanFitWholeFileToMemory(string filePath)
        {
            return false;
            var fileMegabytes = new System.IO.FileInfo(filePath).Length / 1024 / 1024; //Get file size in megabytes
            var performanceCounter  = new PerformanceCounter("Memory", "Available MBytes", true);
            var availableMemory = performanceCounter.NextValue();
            return fileMegabytes < (availableMemory * SafetyFactor);
        }

        private double SafetyFactor = 0.4;
    }
}