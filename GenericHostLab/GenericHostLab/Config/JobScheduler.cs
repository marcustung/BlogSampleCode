using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace GenericHostLab.Config
{
    public class JobScheduler
    {
        public JobScheduler(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config.GetSection("Scheduler").Bind(this);
        }

        public Dictionary<string, string> Triggers { get; set; }
    }
}
