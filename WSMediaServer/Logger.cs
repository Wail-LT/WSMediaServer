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
        
        private static StreamWriter streamWriter = new StreamWriter(FileManager.LOG_FILE);

        public static void Log(string line)
        {
            streamWriter.WriteLine(DateTime.UtcNow.ToString("dd-MM-yyyy.HH-mm").ToString().Replace('/', '-') + " : " + line);
        }

        public static void Dispose()
        {
            Log("logger disposed");
            streamWriter.Dispose();
        }
    }
}
