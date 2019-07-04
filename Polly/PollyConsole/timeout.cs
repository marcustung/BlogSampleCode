using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;

namespace PollyConsole
{
    class timeout
    {
        static async Task Main3(string[] args)
        {
            Policy
                .Timeout(TimeSpan.FromMilliseconds(1), onTimeout: (context, timespan, task) =>
                {
                    Console.WriteLine($"{context.PolicyKey} : execution timed out after {timespan} seconds.");
                })
                .Execute(doTimeOutHTTPRequest);

            // TimeoutStrategy
            //var timeoutPolicy = Policy.TimeoutAsync(30, TimeoutStrategy.Optimistic);
            //HttpResponseMessage httpResponse = await timeoutPolicy
            //    .ExecuteAsync(
            //        async ct => await httpClient.GetAsync(requestEndpoint, ct),
            //        CancellationToken.None
            //    );
        }

        static string doTimeOutHTTPRequest()
        {
            Console.WriteLine($"開始發送 Request");

            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(3);
                response = client.GetAsync(" http://www.mocky.io/v2/5cfed9a23200004f0045f284").Result;
            }

            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
