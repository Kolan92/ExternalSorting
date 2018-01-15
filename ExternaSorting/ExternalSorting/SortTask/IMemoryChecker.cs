using System.Diagnostics;

namespace ExternalSorting.SortTask
{
    public interface IMemoryChecker
    {
        bool CanFitWholeFileToMemory(string filePath);
    }

    public class MemoryChecker : IMemoryChecker
    {
        private double SafetyFactor = 0.4;

        public bool CanFitWholeFileToMemory(string filePath)
        {
            var fileMegabytes = new System.IO.FileInfo(filePath).Length / 1024 / 1024; //Get file size in megabytes
            var performanceCounter  = new PerformanceCounter("Memory", "Available MBytes", true);
            var availableMemory = performanceCounter.NextValue();
            return fileMegabytes < (availableMemory * SafetyFactor);
        }
    }
}