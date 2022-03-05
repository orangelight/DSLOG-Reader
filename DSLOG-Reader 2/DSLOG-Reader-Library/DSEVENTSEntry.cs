using System;
using System.Collections.Generic;
using System.Text;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSEntry
    {
        public readonly DateTime Time;
        public readonly string Data;
        public readonly string TimeData;

        public readonly List<DSEVENTSSubEntry> SubEntries;

        public DSEVENTSEntry(DateTime time, string s)
        {
            Time = time;
            Data = s;
            TimeData = time.ToString("h:mm:ss.fff tt");
            SubEntries = null;
        }

        public DSEVENTSEntry(DateTime time, string raw, List<DSEVENTSSubEntry> subEntries) : this(time, raw)
        {
            SubEntries = subEntries;
        }
    }

    public class DSEVENTSSubEntry
    {
        public readonly int TagVersion;
        public readonly float TimeOffset;
        public readonly string Message;
        public readonly int Flags;
        public readonly int Code;
        public readonly string Details;
        public readonly DateTime Time;
        private readonly string Stack;

        public DSEVENTSSubEntry(DateTime time, int tag, float offset, string message, int flags, int code, string details, string stack)
        {
            TagVersion = tag;
            TimeOffset = offset;
            Message = message;
            Flags = flags;
            Code = code;
            Details = details;
            Time = time;
            Stack = stack;
        }
    }
}
