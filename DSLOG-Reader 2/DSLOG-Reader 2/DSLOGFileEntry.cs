using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
        public DSLOGFileEntry(string fileName, string dir)
        {
            Name = fileName;
            MatchType = FMSMatchType.NA;
            IsFMSMatch = false;
            EventName = "";
            if (!SetTime(dir) || !SetFMS(dir))
            {
                Valid = false;
                return;
            }
            SetSeconds(dir);

            

            Valid = true;
            
        }

        private bool SetTime(string dir)
        {
            DateTime sTime;
            if (!DateTime.TryParseExact(Name, "yyyy_MM_dd HH_mm_ss ddd", CultureInfo.InvariantCulture, DateTimeStyles.None, out sTime))
            {
                DSLOGReader reader = new DSLOGReader(dir + "\\" + Name + ".dslog");
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

        private void SetSeconds(string dir)
        {
            Seconds = ((new FileInfo(dir + "\\" + Name + ".dslog").Length - 19) / 35) / 50;
        }

        private bool SetFMS(string dir)
        {
            string dseventsPath = dir + "\\" + Name + ".dsevents";
            if (File.Exists(dseventsPath))
            {
                DSEVENTSReader reader = new DSEVENTSReader(dseventsPath);
                reader.ReadForFMS();
                if (reader.Version == 3)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DSEVENTSEntry en in reader.Entries)
                    {
                        sb.Append(en.Data);
                    }
                    String txtF = sb.ToString();
                    if (txtF.Contains("FMS Connected:"))
                    {
                        IsFMSMatch = true;
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
                    
                    if (txtF.Contains("FMS Event Name: "))
                    {
                        string[] sArray = txtF.Split(new string[] { "Info" }, StringSplitOptions.None);
                        foreach (String ss in sArray)
                        {
                            if (ss.Contains("FMS Event Name: "))
                            {
                                EventName = ss.Replace("FMS Event Name: ", "").Trim();
                                break;
                            }
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

        public ListViewItem ToListViewItem()
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
                    subItems[5] = EventName;
                    switch (MatchType)
                    {
                        case FMSMatchType.Qualification:
                            backColor = Color.Khaki;
                            break;
                        case FMSMatchType.Elimination:
                            backColor = Color.LightCoral;
                            break;
                        case FMSMatchType.Practice:
                            backColor = Color.LightGreen;
                            break;
                        case FMSMatchType.None:
                            backColor = Color.LightSkyBlue;
                            break;
                    }
                }
               
                TimeSpan sub = DateTime.Now.Subtract(StartTime);
                subItems[4] = sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m";
            }
            var item = new ListViewItem(subItems);
            item.BackColor = backColor;
            if (IsFMSMatch) item.Font = FMSFont;
            return item;
        }
    }
}
