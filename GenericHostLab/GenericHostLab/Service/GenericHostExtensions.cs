using System;
using System.Collections.Generic;
using System.Text;
using GenericHostLab.Job;
using GenericHostLab.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace GenericHostLab.Service
{
    public static class GenericHostExtensions
    {
        public static IHostBuilder InitQuartzHostService(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

                var jobs = new List<JobDetailOption>();
                hostContext.Configuration.GetSection("Quartz:Jobs").Bind(jobs);

                StdSchedulerFactory factory = new StdSchedulerFactory(new QuartzOption(configuration).ToProperties());
                IScheduler scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

                foreach (var item in jobs)
                {
                    // 建立 job
                    var job = JobBuilder.Create<TestJob>()
                        .WithIdentity(item.Name, item.Group)
                        .Build();

                    // 建立 trigger
                    var trigger = TriggerBuilder.Create()
                        .WithCronSchedule(item.Cron)
                        .ForJob(job)
                        .WithIdentity(item.Trigger)
                        .Build();

                    // 將 job 加入 scheduler 中
                    scheduler.ScheduleJob(job, trigger);
                    logger.LogInformation($"Register Job: {item.Name}, Trigger: {item.Trigger}");
                }

                var jobFactory = new SingletonJobFactory(serviceProvider);
                scheduler.JobFactory = jobFactory;

                services.AddOptions();
                services.AddSingleton<IJobFactory, SingletonJobFactory>();
                services.AddSingleton<IJob, TestJob>();
                services.AddSingleton(provider => scheduler);
            });
            return builder;
        }

    }
}
