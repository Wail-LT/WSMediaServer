using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMediaServer
{
    static class Settings
    {
        private static int time = 60;
        private static string token = "your_plex_token";
        private static int port = 32400;
        private static int delay = 1;



        /* Properties */

        public static int Time { get => time; }
        public static string Token { get => token; }
        public static int Port { get => port; }
        public static int Delay { get => delay; set => delay = value; }

        public static void InitSettings()
        {
            using (System.IO.StreamReader fReader = new StreamReader(FileManager.INIT_FILE))
            {
                string [] lines = fReader.ReadToEnd().Replace("\r", "").Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(':');
                    switch (i)
                    {
                        case 0:
                            try{ time = int.Parse(line[1]); }
                            catch (Exception e){ Logger.Log("ERROR InitSettings case 0 : " + e.Message); }
                            break;
                        case 1:
                            token = line[1];
                            break;
                        case 2:
                            try { port = int.Parse(line[1]); }
                            catch (Exception e) { Logger.Log("ERROR InitSettings case 2 : " + e.Message); }
                            break;
                        case 3:
                            try { delay = int.Parse(line[1]); }
                            catch (Exception e) { Logger.Log("ERROR InitSettings case 3 : " + e.Message); }
                            break;
                    }
                }
            }
        }

        public static void PrintSettings()
        {
            Console.WriteLine("\nSettings");
            Console.WriteLine("Time : {0}", time);
            Console.WriteLine("Token : {0}", token);
            Console.WriteLine("Port : {0}", port);
            Console.WriteLine("Delay : {0}", delay);
        }

        public static void LogSettings()
        {
            Logger.Log("Settings");
            Logger.Log("Time : " + time);
            Logger.Log("Token : " + token);
            Logger.Log("Port : " + port);
            Logger.Log("Delay : " + delay);
        }

    }
}
