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

                try
                {
                    token.Register(Thread.CurrentThread.Abort);
                    var memoryChecker = new MemoryChecker();
                    var strategyFactory = new SortStrategyFactory(memoryChecker);
                    var strategy = strategyFactory.ChooseSortStrategy(fileName);
                    strategy.Sort();

                }
                catch (ThreadAbortException ex)
                {
                    // do nothing
                }

            }, token);
            return source;
        }
    }
}
