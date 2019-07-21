using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace GenericHostLab.Job
{
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        private readonly ILogger _logger;

        public TestJob(ILogger<TestJob> logger)
        {
            this._logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now} : TestJob Execute ...");
            return Task.CompletedTask;
        }
    }
}
