using DSLOG_Reader_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader_2
{
    public partial class BulkExportDialog : Form
    {
        public List<DSLOGFileEntry> Files { get; set; }
        private string FilePath;
        private List<string> CheckedFiles;
        public Dictionary<string, int[]> IdToPDPGroup { get; set; }
        public Dictionary<string, string> Series { get; set; }
        public bool UseFilledInEvents { get; set; }
        private volatile int TotalExported = 0;
        public BulkExportDialog()
        {
            InitializeComponent();
            CheckedFiles = new List<string>();
            listView.DoubleBuffered(true);
            textBox1.Text = Directory.GetCurrentDirectory();
            FilePath = textBox1.Text;
        }

        private void BulkExportDialog_Shown(object sender, EventArgs e)
        {

            if (Files == null) return;
            listView.BeginUpdate();
            foreach (var file in Files)
            {
                var item = file.ToListViewItem(UseFilledInEvents);
                item.Checked = true;
                listView.Items.Add(item);
            }
            listView.Columns[0].Width = -2;
            listView.Columns[3].Width = -2;
            listView.Columns[5].Width = -2;
            listView.EndUpdate();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            labelTotal.Text = $"Total Logs: {listView.CheckedItems.Count}";
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerExport.IsBusy) return;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();
            var result = folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                FilePath = textBox1.Text;
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            listView.BeginUpdate();
            foreach(ListViewItem item in listView.Items)
            {
                item.Checked = checkBoxAll.Checked;
            }
            listView.EndUpdate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerExport.IsBusy) return;
            CheckedFiles.Clear();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                CheckedFiles.Add(item.Text);
            }

            backgroundWorkerExport.RunWorkerAsync();
        }

        private void backgroundWorkerExport_DoWork(object sender, DoWorkEventArgs e)
        {
            var tempDict = new Dictionary<string, DSLOGFileEntry>();
            Files.ForEach(en => tempDict.Add(en.Name, en));
            TotalExported = 0;
            Parallel.ForEach(CheckedFiles, (file) =>
            {
                DSLOGFileEntry entry;
                if (tempDict.TryGetValue(file, out entry))
                {
                    DateTime matchTime = DateTime.Now;
                    if (checkBoxLogs.Checked)
                    {
                        string dsFile = $"{entry.FilePath}\\{entry.Name}.dslog";
                        if (File.Exists(dsFile))
                        {
                            DSLOGReader reader = new DSLOGReader(dsFile);
                            reader.Read();
                            

                            if (entry.IsFMSMatch)
                            {
                                string eventName = entry.EventName;
                                if (!UseFilledInEvents && entry.FMSFilledIn) eventName = "";
                                string data = Util.GetTableFromLogEntries(reader.Entries, Series, IdToPDPGroup, checkBoxMatchTime.Checked && reader.Entries.TryFindMatchStart(out matchTime), matchTime, ",");
                                var dir = $"{FilePath}\\{eventName}{entry.StartTime.Year}";
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.WriteAllText($"{dir}\\{entry.Name} {entry.MatchType}_{entry.FMSMatchNum}.csv", data);
                            }
                            else
                            {
                                string data = Util.GetTableFromLogEntries(reader.Entries, Series, IdToPDPGroup, false, matchTime, ",");
                                var dir = $"{FilePath}\\{entry.StartTime.Year}";
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.WriteAllText($"{dir}\\{entry.Name}.csv", data);
                            }


                        }
                    }
                    if (checkBoxEvents.Checked)
                    {
                        string dsFile = $"{entry.FilePath}\\{entry.Name}.dsevents";
                        if (File.Exists(dsFile))
                        {
                            DSEVENTSReader reader = new DSEVENTSReader(dsFile);
                            reader.Read();

                            if (entry.IsFMSMatch)
                            {
                                string eventName = entry.EventName;
                                if (!UseFilledInEvents && entry.FMSFilledIn) eventName = "";
                                string data = Util.GetTableFromEvents(reader.Entries, checkBoxMatchTime.Checked && checkBoxLogs.Checked, matchTime, ",");
                                var dir = $"{FilePath}\\{eventName}{entry.StartTime.Year}";
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.WriteAllText($"{dir}\\{entry.Name} {entry.MatchType}_{entry.FMSMatchNum}_Events.csv", data);
                            }
                            else
                            {
                                string data = Util.GetTableFromEvents(reader.Entries, false, matchTime, ",");
                                var dir = $"{FilePath}\\{entry.StartTime.Year}";
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.WriteAllText($"{dir}\\{entry.Name}_Events.csv", data);
                            }
                        }
                    }
                }
                int precent = (int)((double)++TotalExported / CheckedFiles.Count * 100.0);
                backgroundWorkerExport.ReportProgress(precent);
            });
        }

        



        private void backgroundWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Export Done!");
        }
    }
}
