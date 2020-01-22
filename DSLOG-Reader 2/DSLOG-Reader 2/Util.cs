using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLOG_Reader_2
{
    public static class Util
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public readonly static Color[] PdpColors = { Color.FromArgb(255, 113, 113), Color.FromArgb(255, 198, 89), Color.FromArgb(152, 255, 136), Color.FromArgb(136, 154, 255), Color.FromArgb(255, 52, 42), Color.FromArgb(255, 176, 42), Color.FromArgb(0, 255, 9), Color.FromArgb(0, 147, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(239, 139, 0), Color.FromArgb(46, 220, 0), Color.FromArgb(57, 42, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(200, 132, 0), Color.FromArgb(42, 159, 0), Color.FromArgb(0, 47, 239) };
    }
}
