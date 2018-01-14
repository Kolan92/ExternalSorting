using ExternalSorting.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalSorting.SortTask
{
    public class SortTaskFactory
    {
        public CancellationTokenSource StartNew(string fileName, Action<SortFinishedArgs> postSortAction)
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
                    postSortAction(new SortFinishedArgs());
                }
                catch (ThreadAbortException)
                {
                    // do nothing, operation was cancelled
                }
                catch (Exception exception)
                {
                    var args = new SortFinishedArgs(exception);
                    postSortAction(args);

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
