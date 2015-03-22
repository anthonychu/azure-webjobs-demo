using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebJobsDemo.Web.Models;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebJobsDemo.Web.Hubs
{
    public class LoggingHub : Hub
    {
        static LogEventsSummary latestSummary = new LogEventsSummary();

        public override Task OnConnected()
        {
            Clients.All.onSummaryUpdate(latestSummary);
            return base.OnConnected();
        }

        public async Task AddWarning()
        {
            await LogWarningMessageToQueue(string.Format("Warning from {0}", GetIp()));
        }

        public async Task AddError()
        {
            await LogErrorMessageToQueue(string.Format("Error from {0}", GetIp()));
        }

        public void SendError(string errorMessage)
        {
            Clients.All.onError(errorMessage);
        }

        public void SendRunningCounts(LogEventsSummary summary)
        {
            Clients.All.onRunningCounts(summary);
        }

        public void UpdateSummary(LogEventsSummary summary)
        {
            Clients.All.onSummaryUpdate(summary);
            latestSummary = summary;
        }

        public void UpdateAlertLevel(string alertLevel)
        {
            Clients.All.onAlertLevelUpdate(alertLevel);
        }

        private string GetIp()
        {
            return Context.Request.GetHttpContext().Request.UserHostAddress;
        }

        private Task LogErrorMessageToQueue(string message)
        {
            return LogMessageToQueue("logerrors", message);
        }

        private Task LogWarningMessageToQueue(string message)
        {
            return LogMessageToQueue("logwarnings", message);
        }

        private async Task LogMessageToQueue(string queueName, string message)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            CloudQueueMessage cloudMessage = new CloudQueueMessage(message);
            await queue.AddMessageAsync(cloudMessage);
        }
    }
}