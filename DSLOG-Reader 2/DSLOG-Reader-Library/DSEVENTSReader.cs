using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSReader : Reader
    {
        public readonly List<DSEVENTSEntry> Entries;
        private bool ReadOnlyFms = false;
        private bool Parse = false;
        private Regex FMSMatchRegex = new Regex(@"(FMS Connected:)|(FMS Event Name:)|(FMS Disconnect)|(FMS-GOOD FRC)", RegexOptions.Compiled);
        private Regex FMSEventRegex = new Regex(@"(FMS Connected:)|(FMS Event Name:)", RegexOptions.Compiled);
        private Regex SubEventRegex = new Regex(@"(<TagVersion>1 <time> \-?\d*:?\d+\.\d+ (<message> .*?(?=<TagVersion>1| $)|<count> \d+ <flags> \d <Code> \-?\d+ <details> .*?<location> .*?<stack>.*?(?=<TagVersion>1| $))|Info Joystick.*?(?=<TagVersion>1| $|Info)|Info Rail.*?(?=<TagVersion>1| $|Info))", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);

        public DSEVENTSReader(string path) : base(path)
        {
            Entries = new List<DSEVENTSEntry>();
        }

        public override void Read()
        {
            if (ReadFile())
            {
                reader.Close();
            }
        }

        public void ReadForFMS()
        {
            ReadOnlyFms = true;
            if (ReadFile())
            {
                reader.Close();
            }
        }

        public void ReadAndParse()
        {
            Parse = true;
            if (ReadFile())
            {
                reader.Close();
            }
        }

        protected override bool ReadEntries()
        {
            bool isFMSMatch = false;
            bool haveName = false;
            bool haveNum = false;
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                if (Parse)
                {
                    Entries.Add(ParseEntry());
                }
                else if (!ReadOnlyFms)
                {
                    Entries.Add(ReadEntry());
                }
                else
                {
                    
                    var entry = ReadEntry();
                    if (!isFMSMatch)
                    {
                        if (FMSMatchRegex.Match(entry.Data).Success)
                        {
                            isFMSMatch = true;
                            if (entry.Data.Contains("FMS Connected:")) haveNum = true;
                            if (entry.Data.Contains("FMS Event Name:")) haveName= true;
                            Entries.Add(entry);
                        }
                    }
                    else
                    {
                        if (FMSEventRegex.Match(entry.Data).Success)
                        {
                            if (entry.Data.Contains("FMS Connected:")) haveNum = true;
                            if (entry.Data.Contains("FMS Event Name:")) haveName = true;
                            Entries.Add(entry);
                            if (haveName && haveNum) break;
                        }
                    }
                }
                
            }

            return true;
        }

        protected DSEVENTSEntry ReadEntry()
        {
            DateTime time = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
            Int32 l = reader.ReadInt32();
            string s = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(l));
            return new DSEVENTSEntry(time, s);
        }

        protected DSEVENTSEntry ParseEntry()
        {
            DateTime time = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
            Int32 l = reader.ReadInt32();
            string rawEvent = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(l));

            var stopwatch = new Stopwatch();
            var matches = SubEventRegex.Matches(rawEvent);
            if (matches.Count != 0)
            {
                var subEntries = new List<DSEVENTSSubEntry>();
                foreach(Match sub in matches)
                {
                    var subEntry = ParseSubEntry(sub.Value, time);
                    if (subEntry != null) subEntries.Add(subEntry);
                }
                return new DSEVENTSEntry(time, rawEvent, subEntries);
            }
            return new DSEVENTSEntry(time, rawEvent);
        }

        private DSEVENTSSubEntry ParseSubEntry(string data, DateTime parentTime)
        {
            if (data.StartsWith("Info"))
            {
                return new DSEVENTSSubEntry(parentTime, -1, 0, data, -1, 0, string.Empty, string.Empty);
            }
            if (data.Contains("<message>"))
            {
                return new DSEVENTSSubEntry(parentTime, -1, 0, data, -1, 0, string.Empty, string.Empty);
            }
            if (data.Contains("<count>"))
            {
                return new DSEVENTSSubEntry(parentTime, -1, 0, data, -1, 0, string.Empty, string.Empty);
            }
            return null;
        }
    }
}
