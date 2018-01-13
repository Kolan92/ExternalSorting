using System.Threading;
using System.Threading.Tasks;

namespace ExternalSorting.SortTask
{
    public interface ISortTaskData
    {
        Task SortTask { get; }
        CancellationTokenSource CancellationTokenSource { get; }
    }
}