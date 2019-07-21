using System;
using System.Collections.Generic;
using System.Text;

namespace GenericHostLab.Models
{
    public class Plugin
    {
        public JobInitializer JobInitializer { get; set; }
    }

    public class JobInitializer
    {
        public string Type { get; set; }
        public string FileNames { get; set; }
    }
}
