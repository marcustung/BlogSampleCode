using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace GenericHostLab.Models
{
    public class QuartzOption
    {
        public QuartzOption(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var section = config.GetSection("quartz");
            section.Bind(this);
        }

        public Scheduler Scheduler { get; set; }

        public ThreadPool ThreadPool { get; set; }

        public NameValueCollection ToProperties()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = Scheduler?.InstanceName,
                ["quartz.threadPool.type"] = ThreadPool?.Type,
                ["quartz.threadPool.threadPriority"] = ThreadPool?.ThreadPriority,
                ["quartz.threadPool.threadCount"] = ThreadPool?.ThreadCount.ToString(),
            };

            return properties;
        }

    }
}
