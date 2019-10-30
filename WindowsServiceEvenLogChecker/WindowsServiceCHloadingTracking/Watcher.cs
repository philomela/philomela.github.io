using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace WindowsServiceCHloadingTracking
{
    abstract class Watcher 
    {
        static event TimerCallback CheckerEvent;
        public abstract void Check(object obj);
        Timer timer;

        public Watcher()
        {
            CheckerEvent += Check;
            timer = new Timer(CheckerEvent, 0, 0, 60000); //1200000
        }
    }
}
