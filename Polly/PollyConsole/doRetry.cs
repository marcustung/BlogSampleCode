using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PollyConsole
{
    static class retry
    {
        private static int retryCount = 3;
        private static void doRetry(int second, Action action)
        {
            while (true)
            {
                try
                {
                    action(); // do something
                    break;
                }
                catch
                {
                    if (--retryCount == 0)
                        throw;
                    Thread.Sleep(1000 * second);
                }
            }
        }
    }
}
