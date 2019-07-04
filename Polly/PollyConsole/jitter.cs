using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using Polly;
using Polly.Retry;
using Polly.Wrap;
namespace PollyConsole
{
    class jitter
    {
        static void Mains(string[] args)
        {
            Random jitterer = new Random();
            Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(result => result.StatusCode != HttpStatusCode.OK)
                .WaitAndRetry(5,
                    retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        + TimeSpan.FromMilliseconds(jitterer.Next(0, 100))
                )
                .Execute(doMockHTTPRequest);
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
