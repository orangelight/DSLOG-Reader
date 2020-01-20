using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSReader
    {
        protected BigEndianBinaryReader reader;
        public readonly List<DSEVENTSEntry> Entries;
        public DateTime StartTime;
        public readonly string Path;
        private Int32 Version = -1;


        public DSEVENTSReader(string path)
        {
            Path = path;
            Entries = new List<DSEVENTSEntry>();
        }

        public void Read()
        {
            if (ReadFile())
            {
                reader.Close();
            }
        }

        public void ReadForFMS()
        {
            if (ReadFile(true))
            {
                reader.Close();
            }
        }

        protected bool ReadFile(bool fms = false)
        {
            if (reader != null)
            {
                return false;//Throw something
            }
            if (!File.Exists(Path))
            {
                return false;//Throw something
            }
            reader = new BigEndianBinaryReader(File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            if (reader == null) return false;//Throw something
            Version = reader.ReadInt32();
            if (Version != 3) return false;
            StartTime = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
            ReadEntries(fms);
            return true;
        }

        protected void ReadEntries(bool fms = false)
        {
            bool isFMSMatch = false;
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                if (!fms)
                {
                    Entries.Add(ReadEntry());
                }
                else
                {
                    
                    var entry = ReadEntry();
                    if (entry.Data.Contains("FMS Connected:   "))
                    {
                        isFMSMatch = true;
                        Entries.Add(entry);
                        continue;
                    }

                    if (isFMSMatch)
                    {
                        if (entry.Data.Contains("FMS Event Name: "))
                        {
                            Entries.Add(entry);
                            break;
                        }
                    }
                    else break;
                }
                
            }
        }

        protected DSEVENTSEntry ReadEntry()
        {
            DateTime time = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
            Int32 l = reader.ReadInt32();
            string s = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(l));
            return new DSEVENTSEntry(time, s);
        }

        public int GetVersion()
        {
            return Version;
        }


    }
}
