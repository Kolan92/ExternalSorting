using System;
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
                    using (token.Register(Thread.CurrentThread.Abort))
                    {
                        while (true)
                        {

                            Console.WriteLine("Hello from task! " + DateTime.Now);
                            Thread.Sleep(1000);
                        }
                    }
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
