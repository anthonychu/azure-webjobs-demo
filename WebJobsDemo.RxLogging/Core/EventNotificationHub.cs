using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsDemo.RxLogging.Core
{
    public class EventNotificationHub
    {
        Subject<LogEvent> allLogEventsSubject = new Subject<LogEvent>();
        LogNotifier notifier = new LogNotifier();

        public EventNotificationHub()
        {
            SetUpSubscriptions();
        }

        private void SetUpSubscriptions()
        {
            var allEvents = allLogEventsSubject.AsObservable();

            var errorEvents = allEvents.OfType<ErrorLogEvent>();
            errorEvents.Subscribe(e => notifier.SendError(e.Message));

            var eventBuffers = allEvents
                                    .Buffer(TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(500));

            var bufferedEventCounts = eventBuffers
                                            .Select(b => new LogEventsSummary
                                            {
                                                ErrorCount = b.OfType<ErrorLogEvent>().Count(),
                                                WarningCount = b.OfType<WarningLogEvent>().Count()
                                            });
            bufferedEventCounts.Subscribe(s => notifier.UpdateChart(s));

            var warningAlertLevelChanges = eventBuffers
                                            .Select(b =>
                                            {
                                                var warningCount = b.OfType<WarningLogEvent>().Count();
                                                if (warningCount > 10)
                                                    return "HIGH";
                                                else
                                                    return "LOW";
                                            })
                                            .DistinctUntilChanged();
            warningAlertLevelChanges.Subscribe(s => notifier.UpdateAlertLevel(s));

            var logEventSummaries = allEvents
                                        .Scan(new LogEventsSummary(), (p, e) =>
                                        {
                                            if (e is ErrorLogEvent)
                                                p.ErrorCount += 1;
                                            else if (e is WarningLogEvent)
                                                p.WarningCount += 1;
                                            return p;
                                        })
                                        .Sample(TimeSpan.FromSeconds(1));
            logEventSummaries.Subscribe(s => notifier.UpdateSummary(s));
        }

        public void AddEvent(LogEvent ev)
        {
            allLogEventsSubject.OnNext(ev);
        }
    }
}
