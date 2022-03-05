using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSReader : Reader
    {
        public readonly List<DSEVENTSEntry> Entries;

        private Regex FMSMatchRegex = new Regex(@"(FMS Connected:)|(FMS Event Name:)|(FMS Disconnect)|(FMS-GOOD FRC)", RegexOptions.Compiled);
        private Regex FMSEventRegex = new Regex(@"(FMS Connected:)|(FMS Event Name:)", RegexOptions.Compiled);
        private bool ReadOnlyFms = false;

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

        protected override bool ReadEntries()
        {
            bool isFMSMatch = false;
            bool haveName = false;
            bool haveNum = false;
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                if (!ReadOnlyFms)
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
    }
}
