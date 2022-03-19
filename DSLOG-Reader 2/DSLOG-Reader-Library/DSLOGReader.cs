using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DSLOG_Reader_Library
{
    public class DSLOGReader : Reader
    {
        public const int EntryDistanceMs = 20;

        public PDPType StartingPDPType { get; private set; }


        public readonly List<DSLOGEntry> Entries;
        private int EntryNum;
        public DSLOGReader(string path) : base(path)
        {
            Entries = new List<DSLOGEntry>();
            EntryNum = 0;
        }

        public override void Read()
        {
            ReadFile();
            if (StartingPDPType == PDPType.Unknown)
            {
                throw new Exception("PDP not supported");
            }
            reader.Close();

        }

        protected override void ReadMetadata()
        {
            base.ReadMetadata();
            StartingPDPType = (Version == 4) ? ParsePDPType() : PDPType.Unknown;

        }

        protected PDPType ParsePDPType()
        {
            reader.BaseStream.Seek(13, SeekOrigin.Current);
            byte pdpTypeId = reader.ReadByte();
            reader.BaseStream.Seek(-14, SeekOrigin.Current);
            return GetPdpTypeFromId(pdpTypeId);
        }

        private PDPType GetPdpTypeFromId(byte id)
        {
            if (id == 33) return PDPType.REV;
            if (id == 25) return PDPType.CTRE;
            if (id == 0) return PDPType.None;
            return PDPType.Unknown;
        }

        protected override bool ReadEntries()
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var entry = ReadEntryV4();
                if (entry == null) return false;
                Entries.Add(entry);
            }

            return true;
        }


        // So rev code is 0, 0, 33, 1 (last might be id?)
        // CTRE is 0, 0, 25, 0  (last might be id?)
        // Rev uses 30 bytes for pdp + only has temp (not volt or restist)
        // CTRE uses 21 + voltage + resist + temp
        protected DSLOGEntry ReadEntryV4()
        {

            var tripTime = TripTimeToDouble(reader.ReadByte());
            var packetLoss = PacketLossToDouble(reader.ReadSByte());
            var voltage = VoltageToDouble(reader.ReadUInt16());
            var rio = RoboRioCPUToDouble(reader.ReadByte());
            var status = StatusFlagsToBooleanArray(reader.ReadByte());
            var can = CANUtilToDouble(reader.ReadByte());
            var wifi = WifidBToDouble(reader.ReadByte());
            var bandwidth = BandwidthToDouble(reader.ReadUInt16());
            var pdpId = reader.ReadByte();
            var pdpData = ReadPdpData();
            if (!pdpData.HasValue) return null;
            var time = StartTime.AddMilliseconds(EntryDistanceMs * EntryNum++);
            return new DSLOGEntry(tripTime, packetLoss, voltage, rio, status, can, wifi, bandwidth, pdpId, pdpData.Value.data, pdpData.Value.res, pdpData.Value.volt, pdpData.Value.temp, time);
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

        private (double[] data, double res, double volt, double temp)? ReadPdpData()
        {
            var pdpMetaData = reader.ReadBytes(3);
            var pdpType = GetPdpTypeFromId(pdpMetaData[2]);
            if (pdpType == PDPType.CTRE)
            {
                reader.ReadByte(); // Read id
                return (ReadCTREPDP(), reader.ReadByte(), (double)(reader.ReadByte()) * 0.0736, reader.ReadByte());
            }
            else if (pdpType == PDPType.REV)
            {
                reader.ReadByte(); // Read id
                return (ReadRevPDH(), 0, 0, reader.ReadByte());
            }
            else if (pdpType == PDPType.None)
            {
                return (new double[0], 0, 0, 0);
            }
            return null;
        }

        private double[] ReadRevPDH()
        {
            var ints = new uint[7];

            for (int i = 0; i < 6; ++i)
            {
                ints[i] = reader.ReadUInt32Little();
            }
            ints[6] = BitConverter.ToUInt32(reader.ReadBytes(3).Concat(new byte[] { 0 }).ToArray(), 0);
            var dataBytes = reader.ReadBytes(4);

            double[] d = new double[24];
            for (int i = 0; i < 20; ++i)
            {
                var dataIndex = i / 3;
                var dataOffset = i % 3;
                var data = ints[dataIndex];
                var num = data << (32 - ((dataOffset + 1) * 10));
                num = num >> 22;
                d[i] = num / 8.0d;
            }
            for (int i = 0; i < 4; ++i)
            {
                d[i + 20] = dataBytes[i] / 16.0d;
            }
            return d;
        }

        private double[] ReadCTREPDP()
        {
            var longs = new ulong[3];
            double[] d = new double[16];
            longs[0] = reader.ReadUInt64();
            longs[1] = reader.ReadUInt64();
            longs[2] = BitConverter.ToUInt64(reader.ReadBytes(5).Concat(new byte[] { 0, 0, 0 }).Reverse().ToArray(), 0);
            
            for (int i = 0; i < 16; ++i)
            {
                var dataIndex = i / 6;
                var dataOffset = i % 6;
                var data = longs[dataIndex];
                var num = data << (dataOffset * 10);
                num = num >> (54);
                d[i] = num / 8.0d;
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
