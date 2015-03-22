using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsDemo.Sample
{
    public class EmailService : IEmailService
    {
        public Task Send(EmailMessage message)
        {
            // simulate sending an email
            return Task.Delay(1000);
        }
    }
}
