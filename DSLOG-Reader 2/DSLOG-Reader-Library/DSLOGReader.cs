using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DSLOG_Reader_Library
{
    public class DSLOGReader
    {
        public const int EntryDistanceMs = 20;

        protected BigEndianBinaryReader reader;
        public readonly List<DSLOGEntry> Entries;
        public DateTime StartTime;
        public readonly string Path;
        private int EntryNum;
        public DSLOGReader(string path)
        {
            Path = path;
            Entries = new List<DSLOGEntry>();
            EntryNum = 0;
        }

        public void Read()
        {
            if (ReadFile())
            {
                reader.Close();
            }
            
        }

        protected bool ReadFile()
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
            int version = reader.ReadInt32();
            if (version != 3) return false;
            StartTime = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
            ReadEntries();
            return true;
        }

        protected void ReadEntries()
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                Entries.Add(ReadEntry());
            }
        }

        protected DSLOGEntry ReadEntry()
        {
            return new DSLOGEntry(TripTimeToDouble(reader.ReadByte()), PacketLossToDouble(reader.ReadSByte()), VoltageToDouble(reader.ReadUInt16()), RoboRioCPUToDouble(reader.ReadByte()), StatusFlagsToBooleanArray(reader.ReadByte()), CANUtilToDouble(reader.ReadByte()), WifidBToDouble(reader.ReadByte()), BandwidthToDouble(reader.ReadUInt16()), reader.ReadByte(), PDPValuesToArrayList(reader.ReadBytes(21)), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), StartTime.AddMilliseconds(EntryDistanceMs * EntryNum++));
        }

        protected DateTime FromLVTime(long unixTime, UInt64 ummm)
        {
            var epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds(unixTime);
            epoch = TimeZoneInfo.ConvertTimeFromUtc(epoch, TimeZoneInfo.Local);

            return epoch.AddSeconds(((double)ummm / UInt64.MaxValue));
        }

        //Import methods
        protected double TripTimeToDouble(byte b)
        {
            return (double)b * 0.5d;
        }

        protected double PacketLossToDouble(sbyte b)
        {
            return (double)(b * 4) * .01;
        }

        protected double VoltageToDouble(UInt16 i)
        {
            return (double)i * .00390625d;
        }

        protected double RoboRioCPUToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        protected bool[] StatusFlagsToBooleanArray(byte b)
        {
            byte[] bytes = { b };
            return bytes.SelectMany(GetBits).ToArray();
        }

        protected double CANUtilToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        protected double WifidBToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        protected double BandwidthToDouble(UInt16 i)
        {
            return (double)i * .00390625d;
        }
        protected double[] PDPValuesToArrayList(byte[] ba)
        {
            double[] d = new double[16];
            for (int s = 0; s < 5; s++)
            {
                if (s % 2 == 0)
                {
                    byte[] b5 = new byte[5];
                    Array.Copy(ba, s * 4, b5, 0, 5);
                    for (int n = 0; n < 4; ++n)
                    {
                        if (n == 0)
                        {
                            d[(s * 3) + n] = (double)(Convert.ToUInt16(b5[0] << 2) + Convert.ToUInt16(b5[1] >> 6)) * .125d;
                        }
                        else
                        {
                            d[(s * 3) + n] = (double)(Convert.ToUInt16(((UInt16)((byte)(b5[n] << (n * 2)))) << 2) + Convert.ToUInt16(b5[n + 1] >> (6 - (n * 2)))) * .125d;
                        }
                    }
                }
                else
                {
                    byte[] b3 = new byte[3];
                    Array.Copy(ba, (s * 4) + 1, b3, 0, 3);
                    for (int n = 0; n < 2; ++n)
                    {
                        if (n == 0)
                        {
                            d[((s * 3) + 1) + n] = (double)(Convert.ToUInt16(b3[0] << 2) + Convert.ToUInt16(b3[1] >> 6)) * .125d;
                        }
                        else
                        {
                            d[((s * 3) + 1) + n] = (double)(Convert.ToUInt16(((UInt16)((byte)(b3[1] << 2))) << 2) + Convert.ToUInt16(b3[2] >> 4)) * .125d;
                        }
                    }
                }
            }
            return d;
        }

        protected IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return !((b & 0x80) != 0);
                b *= 2;
            }
        }

    }
}
