using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsDemo.Sample
{
    public class OrderProcessor
    {
        private IEmailService _emailService;

        public OrderProcessor(IEmailService emailService)
        {
            _emailService = emailService;
        }


        public async Task Process(Order order)
        {
            await ProcessOrder(order);
            await SendOrderProcessedEmail(order);
        }


        private async Task ProcessOrder(Order order)
        {
            await Task.Delay(3000);
            Trace.TraceInformation("Processed order: {0}", order.Id);
        }

        private async Task SendOrderProcessedEmail(Order order)
        {
            var message = new EmailMessage
            {
                ToEmail = order.Email,
                Subject = string.Format("Order Processed - {0}", order.Id),
                Body = string.Format("Thanks for your order of {0} widgets!", order.NumberOfWidgets)
            };

            await _emailService.Send(message);
            Trace.TraceInformation("Send email to: {0}", message.ToEmail);
        }
    }
}
