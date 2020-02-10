using DSLOG_Reader_Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;

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
        public static void ClearPointsQuick(Series s)
        {
            if (s.Points.Count != 0)
            {
                s.Points.SuspendUpdates();
                while (s.Points.Count > 0)
                    s.Points.RemoveAt(s.Points.Count - 1);
                s.Points.ResumeUpdates();
            }

        }

        public static void HighlightText(this RichTextBox myRtb, string word, Color color)
        {
            var t = myRtb.Text;
            myRtb.Text = t;
            if (word == string.Empty)
                return;

            int s_start = myRtb.SelectionStart, startIndex = 0, index;
            var Bold = new Font(myRtb.Font.FontFamily, myRtb.Font.Size+2,FontStyle.Bold);
            while ((index = myRtb.Text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                myRtb.Select(index, word.Length);
                myRtb.SelectionFont = Bold;
                myRtb.SelectionColor = color;
                

                startIndex = index + word.Length;
            }

            myRtb.SelectionStart = s_start;
            myRtb.SelectionLength = 0;
            myRtb.SelectionColor = Color.Black;
        }
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
        public static Color MessageColor(String s)
        {
            if (s.Contains("ERROR") || s.Contains("<flags> 1"))
            {
                return Color.LightCoral;
            }
            else if (s.Contains("<Code> 44004 "))
            {
                return Color.SandyBrown;
            }
            else if (s.Equals("Radio Seen"))
            {
                return Color.Lime;
            }
            else if (s.Equals("Radio Lost"))
            {
                return Color.Yellow;
            }
            else if (s.Contains("Warning") || s.Contains("<flags> 2"))
            {
                return Color.Khaki;
            }
            else
            {
                return SystemColors.Window;
            }
        }

        public static string EntryAttToUnit(this DSLOGEntry en, string name, Dictionary<string, int[]> idTOpdp, bool units = true)
        {
            if (name == DSAttConstants.TripTime)
            {
                return en.TripTime + ((units) ? "ms":"");
            }
            else if (name == DSAttConstants.LostPackets)
            {
                return (en.LostPackets * 100).ToString("0.##") + ((units) ? "%" : "");
            }
            else if (name == DSAttConstants.Voltage)
            {
                return en.Voltage.ToString("0.##") + ((units) ? "V" : "");
            }
            else if (name == DSAttConstants.RoboRIOCPU)
            {
                return (en.RoboRioCPU * 100).ToString("0.##") + ((units) ? "%" : "");
            }
            else if (name == DSAttConstants.CANUtil)
            {
                return (en.CANUtil * 100).ToString("0.##") + ((units) ? "%" : "");
            }
            else if (name.StartsWith(DSAttConstants.PDPPrefix))
            {
                return en.GetPDPChannel(int.Parse(name.Substring(3))).ToString("0.##") + ((units) ? "A" : "");
            }
            else if (name == DSAttConstants.DSDisabled)
            {
                return en.DSDisabled.ToString();
            }
            else if (name == DSAttConstants.DSAuto)
            {
                return en.DSAuto.ToString();
            }
            else if (name == DSAttConstants.DSTele)
            {
                return en.DSTele.ToString();
            }
            else if (name == DSAttConstants.RobotDisabled)
            {
                return en.RobotDisabled.ToString();
            }
            else if (name == DSAttConstants.RobotAuto)
            {
                return en.RobotAuto.ToString();
            }
            else if (name == DSAttConstants.RobotTele)
            {
                return en.RobotTele.ToString();
            }

            else if (name == DSAttConstants.Brownout)
            {
                return en.Brownout.ToString();
            }
            else if (name == DSAttConstants.Watchdog)
            {
                return en.Watchdog.ToString();
            }
            else if (name == DSAttConstants.TotalPDP)
            {
                return en.GetDPDTotal().ToString("0.##") + ((units) ? "A" : "");
            }
            else if (name.StartsWith(DSAttConstants.TotalPrefix))
            {
                return en.GetGroupPDPTotal(idTOpdp[name]).ToString("0.##") + ((units) ? "A" : "");
            }
            else if (name.StartsWith(DSAttConstants.DeltaPrefix))
            {
                return en.GetGroupPDPSd(idTOpdp[name]).ToString("0.##") + ((units) ? "A" : "");
            }
            return "";
        }

        public readonly static Color[] PdpColors = { Color.FromArgb(255, 113, 113), Color.FromArgb(255, 198, 89), Color.FromArgb(152, 255, 136), Color.FromArgb(136, 154, 255), Color.FromArgb(255, 52, 42), Color.FromArgb(255, 176, 42), Color.FromArgb(0, 255, 9), Color.FromArgb(0, 147, 255), Color.FromArgb(238, 12, 0), Color.FromArgb(239, 139, 0), Color.FromArgb(46, 220, 0), Color.FromArgb(57, 42, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(200, 132, 0), Color.FromArgb(42, 159, 0), Color.FromArgb(0, 47, 239) };

        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }

        public static bool TryFindMatchStart(this List<DSLOGEntry> list, out DateTime matchTime)
        {
            foreach (var en in list)
            {
                if (en.RobotAuto)
                {
                    matchTime = en.Time;
                    return true;
                }
            }
            matchTime = DateTime.UtcNow;
            return false;
        }

        public static string GetTableFromEntries(List<DSLOGEntry> entries, Dictionary<string, string> series, Dictionary<string, int[]> idTOpdp, bool UseMatchTime, DateTime matchTime, string sep = ",")
        {
            if (entries == null) return "";
            StringBuilder csv = new StringBuilder();

            List<string> headers = new List<string>();
            headers.Add("Time");
            foreach (string value in series.Values)
            {
                headers.Add(value);
            }
            csv.Append(string.Join(sep, headers) + "\n");
            foreach (var entry in entries)
            {
                List<string> row = new List<string>();
                if (UseMatchTime) row.Add((entry.Time - matchTime).TotalSeconds.ToString("0.00"));
                else row.Add(entry.Time.ToString("HH:mm:ss.fff"));

                foreach (string key in series.Keys)
                {
                    row.Add(entry.EntryAttToUnit(key, idTOpdp, false));
                }
                csv.Append(string.Join(sep, row) + "\n");
            }
            return csv.ToString();
        }
    }

    public static class DSAttConstants
    {
        public const string TripTime = "triptime";
        public const string LostPackets = "lostpackets";
        public const string Voltage = "voltage";
        public const string RoboRIOCPU = "roboriocpu";
        public const string CANUtil = "canutil";
        public const string DSDisabled = "dsdisabled";
        public const string DSAuto = "dsauto";
        public const string DSTele = "dstele";
        public const string RobotDisabled = "robotdisabled";
        public const string RobotAuto = "robotauto";
        public const string RobotTele = "robottele";
        public const string Brownout = "brownout";
        public const string Watchdog = "watchdog";
        public const string TotalPDP = "deftotalpdp";
        public const string Messages = "messages";
        public const string PDPPrefix = "pdp";
        public const string TotalPrefix = "total";
        public const string DeltaPrefix = "delta";
    }

    public class XmlColor
    {
        private Color color_ = Color.Black;

        public XmlColor() { }
        public XmlColor(Color c) { color_ = c; }


        public Color ToColor()
        {
            return color_;
        }

        public void FromColor(Color c)
        {
            color_ = c;
        }

        public static implicit operator Color(XmlColor x)
        {
            return x.ToColor();
        }

        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        [XmlAttribute]
        public string Web
        {
            get { return ColorTranslator.ToHtml(color_); }
            set
            {
                try
                {
                    color_ = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
                }
                catch (Exception)
                {
                    color_ = Color.Black;
                }
            }
        }

        [XmlAttribute]
        public byte Alpha
        {
            get { return color_.A; }
            set
            {
                if (value != color_.A) // avoid hammering named color if no alpha change
                    color_ = Color.FromArgb(value, color_);
            }
        }

        public bool ShouldSerializeAlpha() { return Alpha < 0xFF; }
    }
}
