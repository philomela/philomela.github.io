using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsServiceCHloadingTracking
{
    class WatcherEventLog : Watcher
    {
        BuilderMessage _builder;
        EventLog _logWindows = new EventLog("System");
        List<EventLogEntry> collectionWarnings = new List<EventLogEntry>();
        DateTime LastDateTimeWarning = new DateTime();

        public WatcherEventLog(BuilderMessage builder) : base()
        {
            _builder = builder;
        }

        public override void Check(object obj)
        {
            EventLogEntryCollection eventsCollection = _logWindows.Entries;

            foreach (EventLogEntry currentEntry in eventsCollection)
            {
                if (currentEntry.EntryType == EventLogEntryType.Warning && DateTime.Now.Date == currentEntry.TimeWritten.Date)
                {
                    if (LastDateTimeWarning < currentEntry.TimeWritten.Date + currentEntry.TimeWritten.TimeOfDay)

                    {
                        collectionWarnings.Add(currentEntry);
                        LastDateTimeWarning = currentEntry.TimeWritten.Date + currentEntry.TimeWritten.TimeOfDay;
                    }
                }
            }
            if (collectionWarnings.Count < 1)
            {
                return;
            }
            _builder.BuildMessage(collectionWarnings);
            SenderMessage _sender = new SenderMessage(_builder.GetEvents());
            _sender.SendMessage();
            collectionWarnings.Clear();
            return;
        }
    }
}
