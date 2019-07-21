using System;
using System.Collections.Generic;
using System.Text;

namespace GenericHostLab.Models
{
    public class ThreadPool
    {
        public string Type { get; set; }

        public string ThreadPriority { get; set; }

        public int ThreadCount { get; set; }
    }
}
