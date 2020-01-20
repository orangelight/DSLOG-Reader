using System;
using System.Collections.Generic;
using System.Text;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSEntry
    {
        public readonly DateTime Time;
        public readonly String Data;
        public readonly String TimeData;

        public DSEVENTSEntry(DateTime time, string s)
        {
            Time = time;
            Data = s;
            TimeData = time.ToString("h:mm:ss.fff tt");
        }
    }
}
