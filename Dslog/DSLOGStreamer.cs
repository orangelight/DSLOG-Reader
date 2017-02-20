using Dslog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader
{
    class DSLOGStreamer : DSLOGReader
    {
        public volatile List<Entry> Queue;
        private Boolean done = false;
        private BinaryReader2 reader;
        private int i = 0;
        public DSLOGStreamer(string s) : base(s)
        {
            Queue = new List<Entry>();
        }
        
        protected override void readFile(string path)
        {
            if (File.Exists(path))
            {
                reader = new BinaryReader2(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                
                    Version = reader.ReadInt32();
                    if (Version == 3)
                    {
                        StartTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                       // int i = 0;
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            Entries.Add(new Entry(TripTimeToDouble(reader.ReadByte()), PacketLossToDouble(reader.ReadSByte()), VoltageToDouble(reader.ReadUInt16()), RoboRioCPUToDouble(reader.ReadByte()), StatusFlagsToBooleanArray(reader.ReadByte()), CANUtilToDouble(reader.ReadByte()), WifidBToDouble(reader.ReadByte()), BandwidthToDouble(reader.ReadUInt16()), reader.ReadByte(), PDPValuesToArrayList(reader.ReadBytes(21)), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), StartTime.AddMilliseconds(20 * i++)));
                        }
                        Thread t = new Thread(stream);
                        t.Start();
                    }
                    else
                    {
                        MessageBox.Show("ERROR: dslog version not supported");
                    }
                

            }
            else
            {

            }
        }

        private void stream()
        {
            while (!done)
            {
                if (reader.BaseStream.Length - reader.BaseStream.Position >= 35)
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Queue.Add(new Entry(TripTimeToDouble(reader.ReadByte()), PacketLossToDouble(reader.ReadSByte()), VoltageToDouble(reader.ReadUInt16()), RoboRioCPUToDouble(reader.ReadByte()), StatusFlagsToBooleanArray(reader.ReadByte()), CANUtilToDouble(reader.ReadByte()), WifidBToDouble(reader.ReadByte()), BandwidthToDouble(reader.ReadUInt16()), reader.ReadByte(), PDPValuesToArrayList(reader.ReadBytes(21)), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), StartTime.AddMilliseconds(20 * i++)));
                    }
                }
                Thread.Sleep(50);
            }
        }

        public void Close()
        {
            done = true;
        }
    }
}
