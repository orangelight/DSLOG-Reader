using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dslog
{
    struct Entry
    {
        public readonly double TripTime,LostPackets, Voltage, RoboRioCPU, CANUtil, WifidB, Bandwith, PDPResistance, PDPVoltage, PDPTemperature;
        private readonly bool[] statusFlags;
        public readonly bool Brownout, Watchdog, DSTele, DSAuto, DSDisabled, RobotTele, RobotAuto, RobotDisabled;
        public readonly int PDPID;
        private readonly double[] pdpValues;
        public readonly DateTime Time;
        
        public Entry(double trip, double packets, double vol, double rrCPU, bool[] flags, double can, double dB, double band, int pdp, double[] pdpV, double res, double volS, double temp, DateTime time)
        {
            TripTime = trip;
            LostPackets = packets;
            Voltage = vol;
            RoboRioCPU = rrCPU;
            statusFlags = flags;
            Brownout = statusFlags[0];
            Watchdog = statusFlags[1];
            DSTele = statusFlags[2];
            DSAuto = statusFlags[3];
            DSDisabled = statusFlags[4];
            RobotTele = statusFlags[5];
            RobotAuto = statusFlags[6];
            RobotDisabled = statusFlags[7];
            CANUtil = can;
            WifidB = dB;
            Bandwith = band;
            PDPID = pdp;
            pdpValues = pdpV;
            PDPResistance = res;
            PDPVoltage = volS;
            PDPTemperature = temp;
            Time = time;
        }

        public double getPDPChannel(int i)
        {
            return pdpValues[i];
        }
    }

    class DSLOGReader
    {
        public readonly List<Entry> Entries;
        public Int32 Version;
        public DateTime StartTime;
                
        public DSLOGReader(string path)
        {
            Entries = new List<Entry>();
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
                            Entries.Add(new Entry(TripTimeToDouble(reader.ReadByte()), PacketLossToDouble(reader.ReadSByte()), VoltageToDouble(reader.ReadUInt16()), RoboRioCPUToDouble(reader.ReadByte()), StatusFlagsToBooleanArray(reader.ReadByte()), CANUtilToDouble(reader.ReadByte()), WifidBToDouble(reader.ReadByte()), BandwidthToDouble(reader.ReadUInt16()), reader.ReadByte(), PDPValuesToArrayList(reader.ReadBytes(21)), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), StartTime.AddMilliseconds(20 * i++)));
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

        //Import methods
        #region
        private double TripTimeToDouble(byte b)
        {
            return (double)b * 0.5d;
        }

        private double PacketLossToDouble(sbyte b)
        {
            return (double)(b * 4) * .01;
        }

        private double VoltageToDouble(UInt16 i)
        {
            return (double)i * .00390625d;
        }

        private double RoboRioCPUToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        private bool[] StatusFlagsToBooleanArray(byte b)
        {
            byte[] bytes = { b };
            return bytes.SelectMany(GetBits).ToArray();
        }

        private double CANUtilToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        private double WifidBToDouble(byte b)
        {
            return ((double)b * 0.5d) * .01d;
        }

        private double BandwidthToDouble(UInt16 i)
        {
            return (double)i * .00390625d;
        }
        private double[] PDPValuesToArrayList(byte[] ba)
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

        private IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return !((b & 0x80) != 0);
                b *= 2;
            }
        }

        #endregion
    }
}
