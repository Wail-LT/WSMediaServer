using Cassia;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Timers;

namespace WSMediaServer
{
    class Program
    {
        private static bool turnOff;
        private static Timer timer;
        static void Main(string[] args)
        {
            turnOff = false;


            FileManager.GenerateFiles();
            Settings.InitSettings();
            int delay = Settings.Delay * 60000;

            timer = new System.Timers.Timer();
            timer.Interval = delay;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.AutoReset = true;

            Logger.Log("WSMediaServer Starts");
            Settings.LogSettings();

            timer.Start();

            CountUsers();

            Console.WriteLine("\nGlobal Idle Time : 0 min");
            Console.WriteLine("Idle Time : 0 min");
            Console.WriteLine("Plex Idle Time : 0 min");

            Console.WriteLine("Number of Streaming : 0");

            Settings.PrintSettings();

            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);

            while (!turnOff){}
        }

        private static void OnTimer(object sender, ElapsedEventArgs e)
        {
   
            Console.Clear();

            if (PlexAPI.StreamingNumber.Get() == 0)
                PlexAPI.IdleTime += Settings.Delay; // IdleTime incremented with minutes
            else
                PlexAPI.IdleTime = 0;

            if (IdleTimeFinder.GetIdleTime() > Settings.Time && PlexAPI.IdleTime > Settings.Time && CountUsers()<2)
            {
                Logger.Log("Media Server turned off");
                TurnOff();
                turnOff = true;
            }


            if (IdleTimeFinder.GetIdleTime() > PlexAPI.IdleTime)
                Console.WriteLine("\nGlobal Idle Time : {0} min", PlexAPI.IdleTime);
            else
                Console.WriteLine("\nGlobal Idle Time : {0} min", IdleTimeFinder.GetIdleTime());

            Console.WriteLine("Idle Time : {0} min " , IdleTimeFinder.GetIdleTime());
            Console.WriteLine("Plex Idle Time : {0} min", PlexAPI.IdleTime);

            Console.WriteLine("Number of Streaming : {0}", PlexAPI.StreamingNumber.Get());
            Console.WriteLine("Number of connected Users : {0}", CountUsers());

            Settings.PrintSettings();

            Logger.Log("Idle Time : " + IdleTimeFinder.GetIdleTime().ToString() + " min");
            Logger.Log("Plex Idle Time : " + PlexAPI.IdleTime.ToString() + " min");

        }

        private static void TurnOff()
        {
            Dispose();

            // Instance de la classe Process
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            // Nom de l'executable à lancer
            proc.StartInfo.FileName = "shutdown.exe";
            // Arguments à passer à l'exécutable à lancer
            proc.StartInfo.Arguments = " -s -f";
            // Démarrage du processus
            proc.Start();
            // On libère les ressources dont on n'a plus besoin.
            proc.Close(); // Attention Close ne met pas fin au processus.

        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)

        {

            // Put your own handler here

            switch (ctrlType)

            {
                case CtrlTypes.CTRL_C_EVENT:
                case CtrlTypes.CTRL_CLOSE_EVENT:
                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    Dispose();
                    Environment.Exit(0);
                    break;
            }

            return true;

        }


        private static ITerminalServicesSession GetSession()
        {
            ITerminalServicesManager manager = new TerminalServicesManager();
            ITerminalServicesSession session;
            using (ITerminalServer server = manager.GetRemoteServer("192.168.0.10"))
            {
                server.Open();
                int i = 0;
  
                IList<ITerminalServicesSession> sessionsList = server.GetSessions();
                do
                {
                    session = sessionsList[i];
                    i++;
                } while (i < sessionsList.Count && session.UserName.ToUpper() != "MEDIA SERVER");
            }
            return session.UserName.ToUpper() == "MEDIA SERVER" && session != null ? session : null;
        }


        private static int CountUsers()
        {
            int NbrUsers = 0;
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetRemoteServer("192.168.0.10"))
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    NTAccount account = session.UserAccount;

                    if (account != null)
                    {
                        NbrUsers++;
                    }
                }
            }
            return NbrUsers;
        }

        private static void Dispose()
        {
            timer.Stop();
            Logger.Log("timer stoped");

            timer.Dispose();
            Logger.Log("timer disposed");

            Logger.Dispose();
        }






        #region unmanaged

        // Declare the SetConsoleCtrlHandler function

        // as external and receiving a delegate.



        [DllImport("Kernel32")]

        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);



        // A delegate type to be used as the handler routine

        // for SetConsoleCtrlHandler.

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);



        // An enumerated type for the control messages

        // sent to the handler routine.

        public enum CtrlTypes

        {

            CTRL_C_EVENT = 0,

            CTRL_BREAK_EVENT,

            CTRL_CLOSE_EVENT,

            CTRL_LOGOFF_EVENT = 5,

            CTRL_SHUTDOWN_EVENT

        }



        #endregion


    }
}
