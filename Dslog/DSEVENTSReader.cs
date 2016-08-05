using Dslog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader
{
    class DSEVENTSReader
    {
        public Int32 Version;
        public DateTime StartTime;
        public DSEVENTSReader(string path)
        {
            readFile(path);
        }

        private void readFile(string path)
        {
            if (File.Exists(path))
            {

                using (BinaryReader2 reader = new BinaryReader2(File.Open(path, FileMode.Open)))
                {
                    Version = reader.ReadInt32();
                    if (Version == 3)
                    {
                        StartTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                        int i = 0;
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            DateTime time = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                            Int32 l = reader.ReadInt32();
                            MessageBox.Show(time.ToString("mm:ss.fff") + " " + new string(reader.ReadChars(l)));
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR: dslog version not supported");
                    }
                }

            }
            else
            {

            }
        }

        private DateTime FromLVTime(long unixTime, UInt64 ummm)
        {
            var epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds(unixTime);
            epoch = epoch.AddHours(-4);

            return epoch.AddSeconds(((double)ummm / UInt64.MaxValue));
        }
    }
}
