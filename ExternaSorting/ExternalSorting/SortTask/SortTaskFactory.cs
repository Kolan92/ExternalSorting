using System.Threading;
using System.Threading.Tasks;

namespace ExternalSorting.SortTask
{
    public class SortTaskFactory
    {
        public CancellationTokenSource StartNew(string fileName)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var factory = new TaskFactory(token);

            factory.StartNew(() =>
            {
                ISortStrategy strategy = null;

                try
                {
                    token.Register(Thread.CurrentThread.Abort);
                    var memoryChecker = new MemoryChecker();
                    var strategyFactory = new SortStrategyFactory(memoryChecker);
                    strategy = strategyFactory.ChooseSortStrategy(fileName);
                    strategy.Sort();

                }
                catch (ThreadAbortException)
                {
                    // do nothing, operation was cancelled
                }
                finally
                {
                    strategy.CleanUp();
                }

            }, token);
            return source;
        }
    }
}
