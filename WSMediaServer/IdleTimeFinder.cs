using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WSMediaServer
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;

        public uint dwTime;
    }

    /// <summary>
    /// Helps to find the idle time, (in milliseconds or minutes) spent since the last user input
    /// </summary>
    public static class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        /* Get IDLE Time in minutes*/

        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime)/60000;
        }
        /// <summary>
        /// Get the Last input time in milliseconds
        /// </summary>
        /// <returns></returns>
        public static long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }
    }
}
