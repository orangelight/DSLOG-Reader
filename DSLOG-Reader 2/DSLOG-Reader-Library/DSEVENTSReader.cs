using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSLOG_Reader_Library
{
    public class DSEVENTSReader : Reader
    {
        public readonly List<DSEVENTSEntry> Entries;



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
            if (ReadFile(true))
            {
                reader.Close();
            }
        }

        protected override void ReadEntries(bool fms = false)
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
    }
}
