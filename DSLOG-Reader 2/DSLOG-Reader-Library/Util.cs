using System;
using System.Collections.Generic;
using System.Text;

namespace DSLOG_Reader_Library
{
    public static class Util
    {
        public static byte[] Reverse(this byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        public static DateTime FromLVTime(long unixTime, UInt64 offset)
        {
            var epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds(unixTime);
            epoch = TimeZoneInfo.ConvertTimeFromUtc(epoch, TimeZoneInfo.Local);

            return epoch.AddSeconds((double)offset / UInt64.MaxValue);
        }

        public static uint ReadUInt32Little(this BigEndianBinaryReader reader)
        {
            if (!BitConverter.IsLittleEndian) return reader.ReadUInt32();
            return BitConverter.ToUInt32(reader.ReadBytes(sizeof(UInt32)), 0);
        }
    }
}
