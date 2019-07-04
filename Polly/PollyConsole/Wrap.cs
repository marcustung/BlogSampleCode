using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace PollyConsole
{
    class Wrap
    {
        static async Task Main(string[] args)
        {
            var timeoutPolicys = Policy
                .Timeout(TimeSpan.FromMilliseconds(1),
                    onTimeout: (context, timespan, task) =>
                    {
                        Console.WriteLine($"{context.PolicyKey} : execution timed out after {timespan} seconds.");
                    });

            RetryPolicy waitAndRetryPolicy = Policy
                .Handle<Exception>()
                .Retry(3,
                    onRetry: (exception, retryCount) =>
                    {
                        Console.WriteLine($"[Polly retry] : 呼叫 API 異常, 進行第 {retryCount} 次重試");
                    });

            FallbackPolicy<String> fallbackForTimeout = Policy<String>
                .Handle<TimeoutRejectedException>()
                .Fallback(
                    fallbackValue: "Please try again later [Fallback for timeout]",
                    onFallback: b => { Console.WriteLine($"這個請求超時了耶"); }
                );

            FallbackPolicy<String> fallbackForAnyException = Policy<String>
                .Handle<Exception>()
                .Fallback(
                    fallbackAction: () => { return "Please try again later [Fallback for any exception]"; },
                    onFallback: e => { Console.WriteLine($"[Polly fallback] : 重試失敗, say goodbye"); }
                );

            PolicyWrap<String> policyWrap = fallbackForAnyException.Wrap(fallbackForTimeout).Wrap(waitAndRetryPolicy)
                .Wrap(timeoutPolicys);
            policyWrap.Execute(() => doMockHTTPRequest());
        }

        static string doMockHTTPRequest()
        {
            Console.WriteLine($"開始發送 Request");

            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(3);
                result = client.GetAsync("http://www.mocky.io/v2/5cfb4d9b3000006e080a8b0a").Result;
            }

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
