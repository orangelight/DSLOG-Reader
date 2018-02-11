using Dslog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader
{

    struct InfoEntry
    {
        public readonly DateTime Time;
        public readonly String Data;
        public readonly String TimeData;

        public InfoEntry(DateTime time, string s)
        {
            Time = time;
            Data = s;
            TimeData = time.ToString("h:mm:ss.fff tt");
        }
    }
    class DSEVENTSReader
    {
        public Int32 Version;
        public DateTime StartTime;
        public List<InfoEntry> EntryList;

        public DSEVENTSReader(string path, int numToRead)
        {
            EntryList = new List<InfoEntry>();
            readFile(path, numToRead);
        }


        private void readFile(string path, int numToRead)
        {
            if (File.Exists(path))
            {

                using (BinaryReader2 reader = new BinaryReader2(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    Version = reader.ReadInt32();
                    if (Version == 3)
                    {
                        StartTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                        int tempRead = 0;
                        bool FMSMatch = false;
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                             DateTime time = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                            Int32 l = reader.ReadInt32();
                            string s = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(l));
                            EntryList.Add(new InfoEntry(time, s));
                            //if (s.StartsWith("Warning <Code> 44008")) ReadWarning44008(s, time);
                            //if (s.StartsWith("Warning <Code> 44004")) ReadWarning44004(s, time);
                            if (s.Contains("FMS Connected:   Qualification") || s.Contains("FMS Connected:   Elimination") || s.Contains("FMS Connected:   Practice") || s.Contains("FMS Connected:   None")) FMSMatch = true;

                            //Make sure we don't read every line for the listView
                            if (numToRead != -1)
                            {
                                if(FMSMatch)
                                {
                                    if (s.Contains("FMS Event Name: "))  break;
                                }
                                else if (numToRead < tempRead) break;
                                tempRead++;
                            }
                            

                             
                            
                        }
                    }
                   
                }
            }
          
        }

        private DateTime FromLVTime(long unixTime, UInt64 ummm)
        {
            var epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds(unixTime);
            epoch = TimeZoneInfo.ConvertTimeFromUtc(epoch, TimeZoneInfo.Local);
            return epoch.AddSeconds(((double)ummm / UInt64.MaxValue));
        }
    }
}
