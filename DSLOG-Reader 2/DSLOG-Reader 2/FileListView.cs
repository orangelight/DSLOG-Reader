using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public partial class FileListView : UserControl
    {
        private MainForm MForm;
        private string Path;
        List<DSLOGFileEntry> DSLOGFiles;
        private string lastFilter = "";
        public FileListView()
        {
            InitializeComponent();
            DSLOGFiles = new List<DSLOGFileEntry>();
        }

        public void SetMainForm(MainForm form) { MForm = form; }
        public void SetPath(string path) { Path = path; }

        public void LoadFiles()
        {
            if (Path == null)
            {
                return; //Throw something
            }
            if (Directory.Exists(Path))
            {
                DirectoryInfo dslogDir = new DirectoryInfo(Path);
                FileInfo[] Files = dslogDir.GetFiles("*.dslog");

                for (int y = 0; y < Files.Count(); y++)
                {
                    
                    var entry = new DSLOGFileEntry(Files[y].Name.Replace(".dslog", ""), Path);
                    DSLOGFiles.Add(entry);
                    
                    
                }
                FillInMissingFMSEventInfo();
                foreach (var entry in DSLOGFiles)
                {
                    listView.Items.Add(entry.ToListViewItem());
                }
                //sroll down to bottom (need to use timer cuz it's weird
                timerScrollToBottom.Start();
                CreateFileWatcher();
            }

            InitFilterCombo();
        }

        private void FillInMissingFMSEventInfo()
        {
            var fmsMatches = DSLOGFiles.Where(e => e.IsFMSMatch);
            foreach (var match in fmsMatches)
            {
                if (string.IsNullOrWhiteSpace(match.EventName))
                {
                    var nearestEvent = fmsMatches.Where(e => !string.IsNullOrWhiteSpace(e.EventName)).OrderBy(e => Math.Abs((e.StartTime - match.StartTime).Ticks)).First();
                    TimeSpan span = nearestEvent.StartTime - match.StartTime;
                    if (Math.Abs((span).TotalDays) < 5)
                    {
                        //if (match.MatchType == nearestEvent.MatchType && ((span.TotalSeconds < 0 && match.FMSMatchNum < nearestEvent.FMSMatchNum) || (span.TotalSeconds > 0 && match.FMSMatchNum > nearestEvent.FMSMatchNum)))
                        //{
                        //    continue;
                        //}

                        match.EventName = nearestEvent.EventName;
                        continue;
                    }

                    match.EventName = "???";
                }
            }
        }


        private void CreateFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.Filter = "*.dslog*";
            watcher.Created += new FileSystemEventHandler(OnFileChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var entry = new DSLOGFileEntry(e.Name.Replace(".dslog", ""), Path);
            listView.Invoke(new MethodInvoker(delegate { listView.Items.Add(entry.ToListViewItem()); }));
        }

        private void timerScrollToBottom_Tick(object sender, EventArgs e)
        {
            listView.EnsureVisible(listView.Items.Count - 1);
            timerScrollToBottom.Stop();
        }

        private void InitFilterCombo()
        {
            filterSelectorCombo.Items.Add("");
            var eventNames = DSLOGFiles.Where(e => e.IsFMSMatch).Select(e => e.EventName + " "+e.StartTime.ToString("yyyy")).Distinct();
            foreach(var name in eventNames)
            {
                filterSelectorCombo.Items.Add(name);
            }
            filterSelectorCombo.SelectedIndex = 0;
        }

        private void filterSelectorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEvent = filterSelectorCombo.Items[filterSelectorCombo.SelectedIndex].ToString();
            if (lastFilter == selectedEvent) return;


            listView.Items.Clear();
            
            if (selectedEvent == "")
            {
                foreach (var entry in DSLOGFiles)
                {
                    listView.Items.Add(entry.ToListViewItem());
                }
            }
            else
            {
                foreach (var entry in DSLOGFiles.Where(en => en.EventName == selectedEvent.Substring(0, selectedEvent.Length-5) && en.StartTime.ToString("yyyy") == selectedEvent.GetLast(4)))
                {
                    listView.Items.Add(entry.ToListViewItem());
                }
            }
            
            //sroll down to bottom (need to use timer cuz it's weird
            timerScrollToBottom.Start();
            lastFilter = selectedEvent;
        }
    }

        
}
