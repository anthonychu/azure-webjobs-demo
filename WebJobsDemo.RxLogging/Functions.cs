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
        public static void ProcessErrorMessage(
            [QueueTrigger("logerrors")] string message,
            [Table("logevents")] ICollector<LogEventEntity> logEventsTable)
        {
            logEventsTable.Add(new LogEventEntity { EventType = "Warning", Message = message });
            Program.EventNotificationHub.AddEvent(new ErrorLogEvent { Message = message });
        }

        public static void ProcessWarningMessage(
            [QueueTrigger("logwarnings")] string message,
            [Table("logevents")] ICollector<LogEventEntity> logEventsTable)
        {
            logEventsTable.Add(new LogEventEntity { EventType = "Error", Message = message });
            Program.EventNotificationHub.AddEvent(new WarningLogEvent { Message = message });
        }
    }
}
