using System;

namespace WebJobsDemo.RxLogging
{
    public class LogEventEntity
    {
        public LogEventEntity()
        {
            PartitionKey = DateTime.Now.ToString("yyyy-MM-dd");
            RowKey = Guid.NewGuid().ToString();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
    }
}