using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebJobsDemo.RxLogging.Core;
using System.Diagnostics;

namespace WebJobsDemo.RxLogging
{
    public class Functions
    {
        public static void ProcessErrorMessage([QueueTrigger("logerrors")] string message)
        {
            Program.EventProcessor.AddEvent(new ErrorLogEvent { Message = message });
            Trace.TraceInformation("Received an error.");
        }
        public static void ProcessWarningMessage([QueueTrigger("logwarnings")] string message)
        {
            Program.EventProcessor.AddEvent(new WarningLogEvent { Message = message });
            Trace.TraceInformation("Received a warning.");
        }
    }
}
