using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WSMediaServer
{
    public partial class WSMediaServer : ServiceBase
    {

        public WSMediaServer()
        {
            FileManager.GenerateFiles();
            Logger.GenerateLogFile();
            Settings.InitSettings();
            Logger.Dispose();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
            Logger.Dispose();
        }

    }
}
