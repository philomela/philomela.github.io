using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsServiceCHloadingTracking
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Watcher Watcher = new WatcherEventLog(new BuilderMessage());
        }

        protected override void OnStop()
        {
            GC.Collect();
        }
    }
}
