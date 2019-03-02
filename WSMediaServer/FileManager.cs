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
        public static readonly string FILE_PATH = Path.Combine(WS_PATH, "WSMediaServer.ini");

        private static string defaultSettings =
            "Time:60\r\n" +
            "Token:Your_Plex_Token\r\n" +
            "Port:32400";

        public static void GenerateFiles()
        {
            if (!IsFileExist())
            {
                Logger.Log("Unable to find the init file or is corrupted");
                Directory.CreateDirectory(WS_PATH);
                using (StreamWriter fWriter = new StreamWriter(FILE_PATH))
                {
                    fWriter.WriteLine(defaultSettings);
                }
                Logger.Log(FILE_PATH + " File generated");
            }
        }


        private static bool IsFileExist()
        {
            if (File.Exists(FILE_PATH))
            {
                using (StreamReader fReader = new StreamReader(FILE_PATH))
                {
                    string content = fReader.ReadToEnd().Replace("\r", "");

                    /* Regex to check if the file is corrupted */
                    Regex regex = new Regex(@"^Time:[0-9]+\nToken:.+\nPort:[0-9]+$");
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
