using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GenericHostLab.Quartz;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

//using Quartz;

namespace GenericHostLab.Service
{
    public class QuartzHostedService: IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;
        private readonly ILogger<QuartzHostedService> _logger;
        private IScheduler _scheduler;

        public QuartzHostedService(ILoggerFactory loggerFactory,
            ISchedulerFactory schedulerFactory, IEnumerable<JobSchedule> jobSchedules, IJobFactory jobFactory)
        {
            _logger = loggerFactory.CreateLogger<QuartzHostedService>();
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("QuartzHostedService Start...");

            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            _scheduler.JobFactory = _jobFactory;

            foreach (var schedule in _jobSchedules)
            {
                await _scheduler.ScheduleJob(
                    JobBuilder
                        .Create(schedule.JobType)
                        .WithIdentity(schedule.JobType.FullName)
                        .WithDescription(schedule.JobType.Name)
                        .Build(),
                    TriggerBuilder
                        .Create()
                        .WithIdentity($"{schedule.JobType.FullName}.trigger")
                        .WithCronSchedule(schedule.CronExpression)
                        .WithDescription(schedule.CronExpression)
                        .Build()
                    , cancellationToken);
            }
            Thread.Sleep(10000);
            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("QuartzHostedService Stop...");

            await _scheduler?.Shutdown(cancellationToken);
        }
    }
}
