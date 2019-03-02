using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMediaServer
{
    static class Logger
    {
        private static readonly string LOG_FILE = "WSMediaServer."+ DateTime.UtcNow.Date.ToString("dd-mm-yyyy")+".log";
        private static StreamWriter sWriter = new StreamWriter(Path.Combine(FileManager.FILE_PATH, LOG_FILE), true);

        public static void GenerateLogFile()
        {
           sWriter.WriteLine("LOG WSMediaServer " + DateTime.UtcNow.Date.ToString("dd-mm-yyyy"));
        }

        public static void Log(string line)
        {
            sWriter.WriteLine(DateTime.UtcNow.Date.ToString("u") + " : " + line);
        }

        public static void Dispose()
        {
            sWriter.Dispose();
        }
    }
}
