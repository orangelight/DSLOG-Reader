using DSLOG_Reader_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOGMatchCollector
{
    public partial class Form1 : Form
    {
        private List<Match> Matches;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCollectMatches_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerGet.IsBusy) return;
            listView1.Items.Clear();
            backgroundWorkerGet.RunWorkerAsync();
            listView1.DoubleBuffered(true);
        }

        private void backgroundWorkerGet_DoWork(object sender, DoWorkEventArgs e)
        {
            lastMatch = 0;
            Matches = new List<Match>();
            DirectoryInfo dslogDir = new DirectoryInfo(textBoxLoc.Text);
            var Files = dslogDir.GetFiles("*.dslog");
            Directory.CreateDirectory("tempDSLOGSCollector");
            for (int y = 0; y < Files.Count(); y++)
            {
                string name = Files[y].Name.Replace(".dslog", "");
                if (!File.Exists($"{textBoxLoc.Text}\\{name}.dslog") || !File.Exists($"{textBoxLoc.Text}\\{name}.dsevents")) continue;

                Match match;
                try
                {
                    if (!SetFMS(textBoxLoc.Text, name, out match)) continue;
                    bool useless;
                    if (match != null && match.IsFMS && SetUseless(textBoxLoc.Text, name, out useless))
                    {
                        if (!useless)
                        {
                            Matches.Add(match);
                            File.Copy($"{textBoxLoc.Text}\\{name}.dslog", $"tempDSLOGSCollector\\{name}.dslog");
                            File.Copy($"{textBoxLoc.Text}\\{name}.dsevents", $"tempDSLOGSCollector\\{name}.dsevents");
                        }
                    }
                } catch(Exception ex)
                {

                }
                


                int num = (int)(100.0 * ((double)y / (double)Files.Count()));
                backgroundWorkerGet.ReportProgress(num);
            }

            if (File.Exists("MatchFiles.zip")) File.Delete("MatchFiles.zip");
            ZipFile.CreateFromDirectory("tempDSLOGSCollector", "MatchFiles.zip");
            Directory.Delete("tempDSLOGSCollector", true);

            e.Result = Matches;

            
        }
        public enum FMSMatchType
        {
            Qualification,
            Elimination,
            Practice,
            None,
            NA
        }
        class Match
        {
            public string Name { get; set; }
            public bool IsFMS { get; set; }
            public int MatchNum { get; set; }
            public string EventName { get; set; }
            public FMSMatchType MatchType { get; set; }

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
        }

        private bool SetFMS(string dir, string name, out Match match)
        {
            match = null;
           string dseventsPath = dir + "\\" + name + ".dsevents";
            if (File.Exists(dseventsPath))
            {
                DSEVENTSReader reader = new DSEVENTSReader(dseventsPath);
                reader.ReadForFMS();
                if (reader.Version == 3)
                {
                    match = new Match();
                    match.Name = name;
                    StringBuilder sb = new StringBuilder();
                    foreach (DSEVENTSEntry en in reader.Entries)
                    {
                        sb.Append(en.Data+" ");
                    }
                    String txtF = sb.ToString();
                    
                    if (!string.IsNullOrWhiteSpace(txtF))
                    {
                        match.IsFMS = true;
                    }
                    else
                    {
                        match.IsFMS = false;
                    }


                    if (txtF.Contains("FMS Connected:   Qualification"))
                    {
                        match.MatchType = FMSMatchType.Qualification;
                        match.MatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Qualification - ") + 33, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   Elimination"))
                    {
                        match.MatchType = FMSMatchType.Elimination;
                        match.MatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Elimination - ") + 31, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   Practice"))
                    {
                        match.MatchType = FMSMatchType.Practice;
                        match.MatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   Practice - ") + 28, 5).Split(':')[0]);
                    }
                    else if (txtF.Contains("FMS Connected:   None"))
                    {
                        match.MatchType = FMSMatchType.None;
                        match.MatchNum = int.Parse(txtF.Substring(txtF.IndexOf("FMS Connected:   None - ") + 24, 5).Split(':')[0]);
                    }

                    if (txtF.Contains("FMS Event Name: "))
                    {
                        string[] sArray = txtF.Split(new string[] { "Info" }, StringSplitOptions.None);
                        foreach (String ss in sArray)
                        {
                            if (ss.Contains("FMS Event Name: "))
                            {
                                match.EventName = ss.Replace("FMS Event Name: ", "").Trim();
                                break;
                            }
                        }
                    }
                    else
                    {
                        match.EventName = "???";
                    }
                }
                else
                {
                    return false;
                }

            }
            return true;
        }

        private bool SetUseless(string dir, string name, out bool useless)
        {
            useless = false;
            return true;


            //useless = true;
            //string dslogPath = dir + "\\" + name + ".dslog";
            //if (File.Exists(dslogPath))
            //{

            //        DSLOGReader reader = new DSLOGReader(dslogPath);
            //        reader.Read();
            //        if (reader.Version == 3)
            //        {

            //            foreach (var entry in reader.Entries)
            //            {
            //                if (entry.RobotAuto || entry.RobotTele || entry.DSAuto || entry.DSTele || entry.RobotDisabled)
            //                {
            //                    useless = false;
            //                    return true;
            //                }
            //            }
            //            useless = true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //        return true;
            //}
            //return false;
        }
        int lastMatch = 0;
        private void backgroundWorkerGet_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = $"Match Files Found: {listView1.Items.Count}";
            for (;lastMatch<Matches.Count; lastMatch++)
            {
                string[] subItems = new string[3] { Matches[lastMatch].EventName, Matches[lastMatch].MatchNum.ToString(), Matches[lastMatch].Name };
                var item = new ListViewItem(subItems);
                item.BackColor = Matches[lastMatch].GetMatchTypeColor();
                listView1.Items.Add(item);
            }
        }

        private void backgroundWorkerGet_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            progressBar1.Value = 100;
            listView1.Items.Clear();
            
            if (e.Result == null) return;
            var matches = (List<Match>)e.Result;
            foreach(var match in matches)
            {
                string[] subItems = new string[3] { match.EventName, match.MatchNum.ToString(), match.Name};
                var item = new ListViewItem(subItems);
                item.BackColor = match.GetMatchTypeColor();
                listView1.Items.Add(item);
            }
            label2.Text = $"Match Files Found: {listView1.Items.Count}";
            MessageBox.Show($"{listView1.Items.Count} match files exported to MatchFiles.zip");
        }

        private void buttonChangeLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                textBoxLoc.Text = dialog.SelectedPath;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://forms.gle/x1xYVCCbX9utV6PMA");
        }
    }
    static class Util
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
    
}
