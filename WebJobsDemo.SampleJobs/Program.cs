using Microsoft.Azure.WebJobs;
using System;

namespace WebJobsDemo.SampleJobs
{
    class Program
    {
        public static void Main()
        {
            var config = new JobHostConfiguration();
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(8);

            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
