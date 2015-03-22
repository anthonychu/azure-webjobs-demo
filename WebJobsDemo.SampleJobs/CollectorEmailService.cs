using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJobsDemo.Sample;

namespace WebJobsDemo.SampleJobs
{
    class CollectorEmailService : IEmailService
    {
        private ICollector<EmailMessage> _messageCollector;
        public CollectorEmailService(ICollector<EmailMessage> messageCollector)
        {
            _messageCollector = messageCollector;
        }

        public Task Send(EmailMessage message)
        {
            _messageCollector.Add(message);

            return Task.FromResult(true);
        }
    }
}
