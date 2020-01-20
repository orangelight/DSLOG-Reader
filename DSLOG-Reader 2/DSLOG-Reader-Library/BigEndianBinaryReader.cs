using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DSLOG_Reader_Library
{
    public class BigEndianBinaryReader : BinaryReader
    {
        private bool Reverse = true;
        public BigEndianBinaryReader(Stream stream) : base(stream) 
        {
            Reverse = BitConverter.IsLittleEndian;
        }

        public override int ReadInt32()
        {
            if (!Reverse) return base.ReadInt32();
            return BitConverter.ToInt32(base.ReadBytes(sizeof(Int32)).Reverse(), 0);
        }
        public override Int16 ReadInt16()
        {
            if (!Reverse) return base.ReadInt16();
            return BitConverter.ToInt16(base.ReadBytes(sizeof(Int16)).Reverse(), 0);
        }
        public override Int64 ReadInt64()
        {
            if (!Reverse) return base.ReadInt64();
            return BitConverter.ToInt64(base.ReadBytes(sizeof(Int64)).Reverse(), 0);
        }

        public override UInt64 ReadUInt64()
        {
            if (!Reverse) return base.ReadUInt64();
            return BitConverter.ToUInt64(base.ReadBytes(sizeof(UInt64)).Reverse(), 0);
        }
        public override UInt32 ReadUInt32()
        {
            if (!Reverse) return base.ReadUInt32();
            return BitConverter.ToUInt32(base.ReadBytes(sizeof(UInt32)).Reverse(), 0);
        }

        public override UInt16 ReadUInt16()
        {
            if (!Reverse) return base.ReadUInt16();
            return BitConverter.ToUInt16(base.ReadBytes(sizeof(UInt16)).Reverse(), 0);
        }
    }
}
