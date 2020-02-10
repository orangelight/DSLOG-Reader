using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public enum FMSMatchType
    {
        Qualification,
        Elimination,
        Practice,
        None,
        NA
    }
    public class DSLOGFileEntry
    {
        private static Font FMSFont = new Font(FontFamily.GenericSansSerif, 7f, FontStyle.Bold);
        public string Name { get; private set; }
        
        public bool IsFMSMatch { get; private set; }
        public long Seconds { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool Valid { get; private set; }
        public FMSMatchType MatchType { get; private set; }
        public int FMSMatchNum { get; private set; }
        public string EventName {  get; set; }
        public bool FMSFilledIn { get; set; }
        public bool Useless { get; set; }
        public bool Live { get; set; }
        public string FilePath { get; set; }
        public DSLOGFileEntry(string fileName, string dir)
        {
            Name = fileName;
            FilePath = dir;
            MatchType = FMSMatchType.NA;
            IsFMSMatch = false;
            EventName = "";
            
            Valid = PopulateInfo();
            FMSFilledIn = false;
        }

        public bool PopulateInfo()
        {
            SetSeconds();
            if (!SetTime() || !SetFMS() || !SetUseless())
            {
                return false;
            }

            Live = CheckLive();
            return true;
        }
        private bool CheckLive()
        {
            if (StartTime == null || (StartTime - DateTime.Now).Duration().TotalHours > 6) return false;
            try
            {
                File.OpenRead(FilePath + "\\" + Name + ".dslog").Close();
            }
            catch (IOException ex)
            {
                return true;
            }
            return false;
        }
        private bool SetTime()
        {
            DateTime sTime;
            if (!DateTime.TryParseExact(Name, "yyyy_MM_dd HH_mm_ss ddd", CultureInfo.InvariantCulture, DateTimeStyles.None, out sTime))
            {
                DSLOGReader reader = new DSLOGReader(FilePath + "\\" + Name + ".dslog");
                reader.OnlyReadMetaData();
                if (reader.Version != 3)
                {
                    return false;
                }
                StartTime = reader.StartTime;
            }
            else
            {
                StartTime = sTime;
            }
            return true;
        }

        private void SetSeconds()
        {
            Seconds = ((new FileInfo(FilePath + "\\" + Name + ".dslog").Length - 19) / 35) / 50;
        }

        private bool SetFMS()
        {
            string dseventsPath = FilePath + "\\" + Name + ".dsevents";
            if (File.Exists(dseventsPath))
            {
                DSEVENTSReader reader = new DSEVENTSReader(dseventsPath);
                reader.ReadForFMS();
                if (reader.Version == 3)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DSEVENTSEntry en in reader.Entries)
                    {
                        sb.Append(en.Data+" ");
                    }
                    String txtF = sb.ToString();
                    if (!string.IsNullOrWhiteSpace(txtF))
                    {
                        IsFMSMatch = true;
                    }
                    else
                    {
                        IsFMSMatch = false;
                        return true;
                    }


                    if (txtF.Contains("FMS Connected:   Qualification"))
                    {
                        MatchType = FMSMatchType.Qualification;
                        FMSMatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Qualification - ") + 33, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   Elimination"))
                    {
                        MatchType = FMSMatchType.Elimination;
                        FMSMatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Elimination - ") + 31, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   Practice"))
                    {
                        MatchType = FMSMatchType.Practice;
                        FMSMatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Practice - ") + 28, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   None"))
                    {
                        MatchType = FMSMatchType.None;
                        FMSMatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   None - ") + 24, 5).Split(':')[0]);
                    }
                    Match eventMatch = Regex.Match(txtF, "Info FMS Event Name: [a-zA-Z0-9]+");
                    if (eventMatch.Success)
                    {
                        if (!string.IsNullOrWhiteSpace(eventMatch.Value))
                        {
                            EventName = eventMatch.Value.Replace("FMS Event Name: ", "").Replace("Info", "").Trim();
                        }
                    }
                }
                else
                {
                    return false;
                }

            }
            return true;
        }

        private bool SetUseless()
        {
            string dslogPath = FilePath + "\\" + Name + ".dslog";
            if (File.Exists(dslogPath))
            {
                if (Seconds <= 20 && !IsFMSMatch)
                {
                    Useless = true;
                    return true;
                }
                if (IsFMSMatch)
                {
                    DSLOGReader reader = new DSLOGReader(dslogPath);
                    reader.Read();
                    if (reader.Version == 3)
                    {

                        foreach (var entry in reader.Entries)
                        {
                            if (entry.RobotAuto || entry.RobotTele || entry.DSAuto || entry.DSTele)
                            {
                                Useless = false;
                                return true;
                            }
                        }
                        Useless = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Useless = false;
                }
                
                return true;
            }
            return false;
        }

        public ListViewItem ToListViewItem(bool useFilledInEvent = true)
        {
            string[] subItems = new string[6];
            Color backColor = SystemColors.ControlLightLight;
            subItems[0] = Name;
            if (!Valid)
            {
                backColor = SystemColors.ControlDark;
                subItems[1] = "VERSION";
                subItems[2] = "NOT";
                subItems[3] = "";
                subItems[4] = "SUPPORTED";
            }
            else
            {

                subItems[1] = StartTime.ToString("MMM dd, HH:mm:ss ddd");
                subItems[2] = Seconds.ToString();
                if (IsFMSMatch)
                {
                    subItems[3] = FMSMatchNum.ToString();
                    if (useFilledInEvent || !FMSFilledIn) subItems[5] = EventName;
                    backColor = GetMatchTypeColor();
                }
               
                TimeSpan sub = DateTime.Now.Subtract(StartTime);
                subItems[4] = sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m";

                if (Live) backColor = Color.Lime;
            }
            var item = new ListViewItem(subItems);
            item.BackColor = backColor;
            if (IsFMSMatch) item.Font = FMSFont;
            return item;
        }

        public Color GetMatchTypeColor()
        {
            switch (MatchType)
            {
                case FMSMatchType.Qualification:
                    return Color.Khaki;

                case FMSMatchType.Elimination:
                    return Color.LightCoral;

                case FMSMatchType.Practice:
                    return Color.LightGreen;

                case FMSMatchType.None:
                    return Color.LightSkyBlue;
                default:
                    return SystemColors.ControlLightLight;
            }
        }
        public DSLOGFileEntry()
        {

        }
        public static DSLOGFileEntry FromCache(string entry, string dir)
        {
            var logFile = new DSLOGFileEntry();
            string[] data = entry.Split(',');
            logFile.Name = data[0];
            logFile.FilePath = dir;
            logFile.IsFMSMatch = bool.Parse(data[1]);
            logFile.Seconds = long.Parse(data[2]);
            logFile.StartTime = DateTime.Parse(data[3]);
            logFile.Useless = bool.Parse(data[4]);
            if (logFile.IsFMSMatch)
            {
                logFile.FMSFilledIn = bool.Parse(data[5]);
                logFile.MatchType = (FMSMatchType) Enum.Parse(typeof(FMSMatchType), data[6]);
                logFile.FMSMatchNum = int.Parse(data[7]);
                logFile.EventName = data[8];
            }
            logFile.Valid = true;
            return logFile;
        }

        public string ToCacheEntry()
        {
            List<string> row = new List<string>();
            row.Add(Name);
            row.Add(IsFMSMatch.ToString());
            row.Add(Seconds.ToString());
            row.Add(StartTime.ToString());
            row.Add(Useless.ToString());

            if (IsFMSMatch)
            {
                row.Add(FMSFilledIn.ToString());
                row.Add(MatchType.ToString());
                row.Add(FMSMatchNum.ToString());
                row.Add(EventName);
            }
            return string.Join(",", row);
        }
    }

    public class DSLOGFileEntryCache
    {
        private Dictionary<string, DSLOGFileEntry> Cache;
        private List<DSLOGFileEntry> NewEntries;
        public string FilePath { get; private set; }

        public bool New { get; private set; }

        public DSLOGFileEntryCache(string file)
        {
            Cache = new Dictionary<string, DSLOGFileEntry>();
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach(var line in lines)
                {
                    var entry = DSLOGFileEntry.FromCache(line.Replace("\n", ""), Path.GetDirectoryName(file));
                    Cache.Add(entry.Name, entry);
                }
                New = false;
            }
            else
            {
                New = true;
            }
            FilePath = file;
            NewEntries = new List<DSLOGFileEntry>();
        }

        public bool TryGetEntry(string name, out DSLOGFileEntry entry)
        {
            return Cache.TryGetValue(name, out entry);
        }
        public DSLOGFileEntry AddEntry(string filename, string dir)
        {
            var entry = new DSLOGFileEntry(filename, dir);
            Cache.Add(entry.Name, entry);
            NewEntries.Add(entry);
            return entry;
        }

        public void SaveCache()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var entry in NewEntries)
            {
                if (((entry.FMSFilledIn && entry.EventName == "???") || (entry.FMSFilledIn && Math.Abs((entry.StartTime-DateTime.Now).TotalDays) < 2)) || !entry.Valid || entry.Live) continue;
                sb.Append(entry.ToCacheEntry());
                sb.Append("\n");
            }
            File.AppendAllText(FilePath, sb.ToString());
            NewEntries.Clear();
        }
    }
}
