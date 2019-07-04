using Polly;
using Polly.Timeout;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;

namespace PollyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Policy
                // 故障處理 : 要 handle 什麼樣的異常
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(result => result.StatusCode != HttpStatusCode.OK)
                // 重試策略 : 異常發生時要進行的重試次數及重試機制
                .Retry(3, onRetry: (exception, retryCount) =>
                {
                    Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 進行第 {retryCount} 次重試, Error :{exception.Result.StatusCode}");
                })
                // 要執行的任務
                .Execute(doMockHTTPRequest);

            Console.WriteLine("結束退出");
            Console.ReadKey();
        }

        static HttpResponseMessage doMockHTTPRequest()
        {
            Console.WriteLine($"[App] {DateTime.Now.ToString(CultureInfo.InvariantCulture)}: 開始發送 Request");

            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                result = client.GetAsync("http://www.mocky.io/v2/5cfb4d9b3000006e080a8b0a").Result;
            }

            return result;
        }
    }
}
