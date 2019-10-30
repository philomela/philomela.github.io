using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceCHloadingTracking
{
    struct Warning
    {
        public string WarningText;
        public DateTime DateTimeWarning;
        public string TypeWarning;

        public override string ToString()
        {
            return $"Date Event from log: {DateTimeWarning.ToString()} <br> Text Warning: {WarningText} <br> Type event: {TypeWarning} <br><br>";
        }
    }
}
