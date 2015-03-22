using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsDemo.RxLogging.Core
{
    public class LogNotifier
    {
        IHubProxy loggingHubProxy;

        public LogNotifier()
        {
            var hubConnection = new HubConnection(System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]);
            loggingHubProxy = hubConnection.CreateHubProxy("LoggingHub");
            hubConnection.Start().Wait();
        }

        public void SendError(string message)
        {
            loggingHubProxy.Invoke("SendError", message);
        }

        public void UpdateSummary(LogEventsSummary summary)
        {
            loggingHubProxy.Invoke("UpdateSummary", summary);
        }

        public void UpdateAlertLevel(string status)
        {
            loggingHubProxy.Invoke("UpdateAlertLevel", status);
        }

        public void UpdateChart(LogEventsSummary summary)
        {
            loggingHubProxy.Invoke("SendRunningCounts", summary);
        }
    }
}
