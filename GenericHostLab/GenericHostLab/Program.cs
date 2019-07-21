using GenericHostLab.Job;
using GenericHostLab.Quartz;
using GenericHostLab.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericHostLab
{
    class Program
    {
        private static readonly string _currentDirectory = $"{Directory.GetCurrentDirectory()}/Config";
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.ToList().Contains("--console"));

            try
            {
                var builder = CreateHostBuilder();

                if (isService)
                {
                    await builder.RunAsServiceAsync();
                }
                else
                {
                    await builder.RunConsoleAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder() =>
            new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    // setup Host configuration
                    configHost.SetBasePath(_currentDirectory)
                        .AddJsonFile("hostsettings.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    // setup App 
                    configApp.SetBasePath(_currentDirectory)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile(
                            $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                            optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<HostOptions>(option =>
                    {
                        option.ShutdownTimeout = TimeSpan.FromSeconds(30);
                    });

                    // Add Quartz services
                    services.AddSingleton<IJobFactory, SingletonJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                    // Add job
                    var testJob = new JobSchedule(jobType: typeof(TestJob), cronExpression: "0/3 * * * * ?");
                    services.AddSingleton<TestJob>();
                    services.AddSingleton(testJob);

                    services.AddHostedService<QuartzHostedService>();
                    //services.AddHostedService<TimedHostedService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // Get Config from Logging section
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                            .AddConsole();

                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                    }
                });
    }
}
