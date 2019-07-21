using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Spi;

namespace GenericHostLab.Job
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }
        public void ReturnJob(IJob job)
        {
        }
    }
}
