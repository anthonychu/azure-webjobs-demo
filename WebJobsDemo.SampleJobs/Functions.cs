using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebJobsDemo.Sample;
using System.Web.Helpers;
using System.Diagnostics;

namespace WebJobsDemo.SampleJobs
{
    public class Functions
    {
        public static async Task ProcessOrder(
            [QueueTrigger("orders")] Order order,
            [Queue("emails")] ICollector<EmailMessage> emailMessageCollector,
            TextWriter logger)
        {
            var orderProcessor = new OrderProcessor(new CollectorEmailService(emailMessageCollector));
            await orderProcessor.Process(order);

            logger.WriteLine("Processed order: {0}", order.Id);
        }

        public static async Task ProcessEmail(
            [QueueTrigger("emails")] EmailMessage emailMessage,
            TextWriter logger)
        {
            var emailService = new EmailService();
            await emailService.Send(emailMessage);

            logger.WriteLine("Sent email: {0}", emailMessage.Subject);
        }

        public static void GenerateThumbnail(
            [BlobTrigger("imageblobs-original/{filename}.{ext}")] WebImage originalImage,
            [Blob("imageblobs-resized/{filename}-small.{ext}")] out WebImage resizedImage)
        {
            resizedImage = originalImage.Resize(96, 96, true, true);
        }
    }
}
