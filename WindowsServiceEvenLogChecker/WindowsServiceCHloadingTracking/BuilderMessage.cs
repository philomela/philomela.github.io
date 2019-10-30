using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsServiceCHloadingTracking
{
    class BuilderMessage
    {
        List<Warning> _warnings = new List<Warning>();
        string TextMessage = null;
        Warning _warning = new Warning();

        public void BuildMessage(List<EventLogEntry> collectionWarnings)
        {
            foreach (EventLogEntry currentEntry in collectionWarnings)
            {
                _warning.WarningText = currentEntry.Message;
                _warning.DateTimeWarning = currentEntry.TimeWritten;
                _warning.TypeWarning = "Warning";
                _warnings.Add(_warning);
            }
        }
        public string GetEvents()
        {
            TextMessage = null;
            foreach (Warning currentWarning in _warnings)
            {
                TextMessage += $"{currentWarning.ToString()}\n";
            }
            _warnings.Clear();
            return TextMessage;
        }
    }
}
