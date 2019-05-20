using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WSMediaServer
{
    static class FileManager
    {
        private const string WS_PATH = @"C:\Users\Public\AppData\Roaming\WSMediaServer";
        public static readonly string LOG_FILE = Path.Combine(WS_PATH,"WSMediaServer." + DateTime.UtcNow.ToString("dd-MM-yyyy.HH-mm").ToString() + ".log");
        public static readonly string INIT_FILE = Path.Combine(WS_PATH, "WSMediaServer.ini");

        private static string defaultSettings =
            "Time:60\r\n" +
            "Token:Your_Plex_Token\r\n" +
            "Port:32400\r\n" +
            "Delay:1";

        public static void GenerateFiles()
        {
            Directory.CreateDirectory(WS_PATH);

            Logger.Log("LOG WSMediaServer " + DateTime.UtcNow.Date.ToString("dd-MM-yyyy"));

            if (!IsFileExist())
            {
                Logger.Log("Error : Unable to find the init file or is corrupted");
                using (StreamWriter streamWriter = new StreamWriter(INIT_FILE))
                {
                    streamWriter.WriteLine(defaultSettings);
                }
                Logger.Log(INIT_FILE + " File generated");
            }
            Logger.Log("Init File found");
        }


        private static bool IsFileExist()
        {
            if (File.Exists(INIT_FILE))
            {
                using (StreamReader fReader = new StreamReader(INIT_FILE))
                {
                    string content = fReader.ReadToEnd().Replace("\r", "");

                    /* Regex to check if the file is corrupted */
                    Regex regex = new Regex(@"^Time:[0-9]+\nToken:.+\nPort:[0-9]+\nDelay:[0-9]+$");
                    if (!regex.IsMatch(content))
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }

    }
}
