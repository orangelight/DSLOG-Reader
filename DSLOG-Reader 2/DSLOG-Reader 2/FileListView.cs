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
using DSLOG_Reader_2.Properties;
using System.Threading;
using System.Collections.Concurrent;

namespace DSLOG_Reader_2
{
    public partial class FileListView : UserControl
    {
        private MainForm MForm;
        public EventsView EventView { get; set; }
        private string Path;
        private Dictionary<string, DSLOGFileEntry> DSLOGFiles;
        private string lastFilter = "";
        public MainGraphView MainChart { get; set; }
        private int lastIndexSelectedFiles = 0;
        private bool filterUseless = false;
        private bool firstColumnResize = true;
        private ConcurrentQueue<string> LogUpdateQueue;
        public CompetitionView CompView { get; set; }
        private bool AllowFillInEventNames = true;
        public FileListView()
        {
            InitializeComponent();
            DSLOGFiles = new Dictionary<string, DSLOGFileEntry>();
            LogUpdateQueue = new ConcurrentQueue<string>();
            toolTip1.SetToolTip(buttonFilter, "Filter Useless Logs");
            listView.DoubleBuffered(true);
        }

        public void SetMainForm(MainForm form) { MForm = form; }
        public void SetPath(string path) { Path = path; textBoxPath.Text = path; }

        public void LoadFiles()
        {
            filterUseless = false;
            firstColumnResize = true;
            listView.Items.Clear();
            DSLOGFiles.Clear();
            if (Path == null)
            {
                return; //Throw something
            }
            if (Directory.Exists(Path))
            {
               
                DSLOGFileEntryCache cache = new DSLOGFileEntryCache($"{Path}\\.dslog.cache");
                CacheLoadingDialog dialog = new CacheLoadingDialog(cache, DSLOGFiles, Path);
                dialog.ShowDialog();
                FillInMissingFMSEventInfo();
                cache.SaveCache();
                listView.BeginUpdate();
                foreach (var entry in DSLOGFiles.Values)
                {
                    listView.Items.Add(entry.ToListViewItem(AllowFillInEventNames));
                }
                listView.EndUpdate();
                //sroll down to bottom (need to use timer cuz it's weird
                timerScrollToBottom.Start();
                CreateFileWatcher();
            }

            InitFilterCombo();
            timerFileUpdate.Start();
        }

        private void FillInMissingFMSEventInfo()
        {
            var fmsMatches = DSLOGFiles.Values.Where(e => e.IsFMSMatch);
            foreach (var match in fmsMatches)
            {
                if (string.IsNullOrWhiteSpace(match.EventName) || match.FMSFilledIn)
                {
                    var nearestEvent = fmsMatches.Where(e => !string.IsNullOrWhiteSpace(e.EventName)).OrderBy(e => Math.Abs((e.StartTime - match.StartTime).Ticks)).First();
                    TimeSpan span = nearestEvent.StartTime - match.StartTime;
                    if (Math.Abs((span).TotalDays) < 4)
                    {
                        //if (match.MatchType == nearestEvent.MatchType && ((span.TotalSeconds < 0 && match.FMSMatchNum < nearestEvent.FMSMatchNum) || (span.TotalSeconds > 0 && match.FMSMatchNum > nearestEvent.FMSMatchNum)))
                        //{
                        //    continue;
                        //}

                        match.EventName = nearestEvent.EventName;
                        match.FMSFilledIn = true;
                        continue;
                    }
                    match.FMSFilledIn = true;
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
            if (e.ChangeType != WatcherChangeTypes.Created) return;

            LogUpdateQueue.Enqueue(e.Name.Replace(".dslog", ""));
        }

        private void timerScrollToBottom_Tick(object sender, EventArgs e)
        {
            listView.BeginUpdate();
            if (listView.Items.Count != 0)
            {
                listView.EnsureVisible(listView.Items.Count - 1);
                
                
            }
            if (firstColumnResize)
            {
                ResizeColumns();
                firstColumnResize = false;
            }
            listView.EndUpdate();
            timerScrollToBottom.Stop();
        }

        private void InitFilterCombo()
        {
            filterSelectorCombo.Items.Clear();
            filterSelectorCombo.Items.Add("All Logs");
            var eventNames = DSLOGFiles.Values.Where(e => e.IsFMSMatch).Select(e => e.EventName + " "+e.StartTime.ToString("yyyy")).Distinct();
            foreach(var name in eventNames)
            {
                filterSelectorCombo.Items.Add(name);
            }
            filterSelectorCombo.SelectedIndex = 0;
        }

        private void filterSelectorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterLogs();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                if (listView.SelectedItems[0].Index != lastIndexSelectedFiles)
                {
                    lastIndexSelectedFiles = listView.SelectedItems[0].Index;
                    DSLOGFileEntry entry;
                    if (DSLOGFiles.TryGetValue(listView.SelectedItems[0].Text, out entry))
                    {
                        GraphLog(entry);
                    }
                }
            }
        }

        private void GraphLog(DSLOGFileEntry file)
        {
            if (MainChart !=null)
            {
                MainChart.LoadLog(file);
                EventView.LoadLog(file);
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            filterUseless = !filterUseless;
            FilterLogs(true);
        }

        private void FilterLogs(bool force = false)
        {
            if (filterUseless)
            {
               
                buttonFilter.BackgroundImage = Resources.StopFilter_16x;
            }
            else
            {
                buttonFilter.BackgroundImage = Resources.RunFilter_16x;
            }
            string selectedEvent = filterSelectorCombo.Items[filterSelectorCombo.SelectedIndex].ToString();
            if (lastFilter == selectedEvent && !force) return;

            listView.BeginUpdate();
            listView.Items.Clear();
            
            
            if (selectedEvent == "All Logs")
            {
                foreach (var entry in GetFilteredFiles())
                {
                    listView.Items.Add(entry.ToListViewItem(AllowFillInEventNames));
                }
                if(lastFilter != selectedEvent) CompView.SetEventMatches(new List<DSLOGFileEntry>());
            }
            else
            {
                foreach (var entry in GetFilteredFiles())
                {
                    listView.Items.Add(entry.ToListViewItem(AllowFillInEventNames));
                }
                if (lastFilter != selectedEvent) CompView.SetEventMatches(DSLOGFiles.Values.Where(e => !(e.Useless) && !e.Live).Where(en => (!en.FMSFilledIn || AllowFillInEventNames) && en.EventName == selectedEvent.Substring(0, selectedEvent.Length - 5) && en.StartTime.ToString("yyyy") == selectedEvent.GetLast(4)).ToList());
            }
            listView.EndUpdate();
            //sroll down to bottom (need to use timer cuz it's weird)
            lastIndexSelectedFiles = -1;
            timerScrollToBottom.Start();
            lastFilter = selectedEvent;
        }

        private void buttonRefreash_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }

        public bool HasFMSMatch()
        {
            if (DSLOGFiles != null && DSLOGFiles.Count > 0)
            {
                return DSLOGFiles.Values.Any(e => e.IsFMSMatch);
            }
            return false;
        }

        private void ResizeColumns()
        {
            for(int i = 0; i < listView.Columns.Count; i++)
            {
                listView.Columns[i].Width = -2;
                if (i != 2 && i!= 3)
                {
                    listView.Columns[i].Width = (int)Math.Ceiling(listView.Columns[i].Width * 1.06);
                }
                if (i == 4)
                {
                    listView.Columns[i].Width = (int)Math.Ceiling(listView.Columns[i].Width * 1.05);
                }
            }
        }

        public List<DSLOGFileEntry> GetMatches(string eventName)
        {
            return DSLOGFiles.Values.Where(en => en.EventName == eventName.Substring(0, eventName.Length - 5) && en.StartTime.ToString("yyyy") == eventName.GetLast(4) && !en.Useless).ToList();
        }

        public string GetPath()
        {
            return Path;
        }

        private void timerFileUpdate_Tick(object sender, EventArgs e)
        {
            var AddBack = new List<string>();
            var AddToFiles = new List<DSLOGFileEntry>();
            bool listChanged = false;
            while (LogUpdateQueue.Count != 0)
            {
                string file = "";
                if (LogUpdateQueue.TryDequeue(out file))
                {
                    FileInfo fileInfo = new FileInfo($"{Path}\\{file}.dslog");
                    if (fileInfo.Length < 100)
                    {
                        AddBack.Add(file);
                    }
                    else
                    {
                        var entry = new DSLOGFileEntry(file, Path);
                        if (entry.Valid)
                        {
                            DSLOGFiles.Add(entry.Name, entry);
                            listChanged = true;
                        } 
                    }
                }
                else
                {
                    break;
                }
            }

            foreach(string file in AddBack)
            {
                LogUpdateQueue.Enqueue(file);
            }

            foreach (var entry in DSLOGFiles.Values.Where(en => en.Live))
            {

                try
                {
                    File.OpenRead($"{Path}\\{entry.Name}.dslog").Close();
                    entry.PopulateInfo();
                    listChanged = true;
                }
                catch (IOException ex)
                {

                }
            }

            if (listChanged) FilterLogs(true);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FileListViewSettingsDialog settingsDialog = new FileListViewSettingsDialog();
            settingsDialog.AllowEventFillIn = AllowFillInEventNames;
            settingsDialog.FolderPath = Path;
            var result = settingsDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                AllowFillInEventNames = settingsDialog.AllowEventFillIn;
                SetPath(settingsDialog.FolderPath);
                LoadFiles();
            }
        }
        private IEnumerable<DSLOGFileEntry> GetFilteredFiles()
        {
            string selectedEvent = filterSelectorCombo.Items[filterSelectorCombo.SelectedIndex].ToString();
            if (selectedEvent == "All Logs")
            {
                return DSLOGFiles.Values.Where(e => !(e.Useless && filterUseless));
            }
            else
            {
                return DSLOGFiles.Values.Where(e => !(e.Useless && filterUseless) || e.Live).Where(en => (!en.FMSFilledIn || AllowFillInEventNames) && en.EventName == selectedEvent.Substring(0, selectedEvent.Length - 5) && en.StartTime.ToString("yyyy") == selectedEvent.GetLast(4));
            }
        }
        private void buttonBulkExport_Click(object sender, EventArgs e)
        {
            BulkExportDialog bulkExport = new BulkExportDialog();
            bulkExport.FilePath = Path;
            bulkExport.Files = GetFilteredFiles().ToList();
            bulkExport.ShowDialog();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "DSLOG Files (.dslog)|*.dslog";
            fileDialog.Multiselect = false;
            var result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                DSLOGFileEntry entry = new DSLOGFileEntry(System.IO.Path.GetFileName(fileDialog.FileName).Replace(".dslog", ""), System.IO.Path.GetDirectoryName(fileDialog.FileName));
                DSLOGFiles.Add(entry.Name+" Opened", entry);
                FilterLogs(true);
                GraphLog(entry);
            }
            
        }
    }        
}
