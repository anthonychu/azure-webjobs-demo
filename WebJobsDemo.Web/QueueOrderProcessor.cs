using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebJobsDemo.Sample;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure;
using Newtonsoft.Json;
using System.Configuration;

namespace WebJobsDemo.Web
{
    class QueueOrderProcessor
    {
        public async Task Process(Order order)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            CloudQueue queue = queueClient.GetQueueReference("orders");
            
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(order));
            await queue.AddMessageAsync(message);
        }
    }
}
