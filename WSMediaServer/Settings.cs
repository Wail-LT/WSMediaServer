using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WSMediaServer
{
    static class Settings
    {
        private static int time = 60;
        private static string token = "your_plex_token";
        private static int port = 32400;
        private static int delay = 1;
        private static string ip = "plex_server_ip";



        /* Properties */

        public static int Time => time;
        public static string Token => token;
        public static int Port => port;
        public static int Delay => delay;
        public static string Ip => ip;

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
                            catch (Exception e){ Logger.Log("ERROR InitSettings on IDLE Time : " + e.Message); }
                            break;
                        case 1:
                            token = line[1];
                            break;
                        case 2:
                            try { port = int.Parse(line[1]); }
                            catch (Exception e) { Logger.Log("ERROR InitSettings on Server Port : " + e.Message); }
                            break;
                        case 3:
                            try { delay = int.Parse(line[1]); }
                            catch (Exception e) { Logger.Log("ERROR InitSettings on Delay Time : " + e.Message); }
                            break;
                        case 4:
                            Regex regex = new Regex(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");

                            if (regex.IsMatch(line[1]))
                                ip = line[1];
                            else
                                throw new NotImplementedException("ERROR InitSettings on Server IP 1");
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
            Console.WriteLine("Ip : {0}", ip);
        }

        public static void LogSettings()
        {
            Logger.Log("Settings");
            Logger.Log("Time : " + time);
            Logger.Log("Token : " + token);
            Logger.Log("Port : " + port);
            Logger.Log("Delay : " + delay);
            Logger.Log("Ip : " + ip);
        }

    }
}
