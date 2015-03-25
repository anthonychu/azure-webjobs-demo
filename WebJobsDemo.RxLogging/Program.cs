using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebJobsDemo.RxLogging.Core;

namespace WebJobsDemo.RxLogging
{
    class Program
    {
        public static EventNotificationHub EventNotificationHub = new EventNotificationHub();

        static void Main()
        {
            var config = new JobHostConfiguration();
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(4);

            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
