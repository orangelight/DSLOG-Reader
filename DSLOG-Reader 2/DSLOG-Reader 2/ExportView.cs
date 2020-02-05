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

namespace DSLOG_Reader_2
{
    public partial class ExportView : UserControl, SeriesViewObserver
    {
        public enum ExportMode
        {
            Chart,
            Events,
            Compititon
        }

        public ExportMode CurrentMode { get; private set; }
        public MainGraphView DSGraph { get; set; }
        public EventsView DSEvents { get; set; }
        private Dictionary<string, string> EnabledSeries = new Dictionary<string, string>();
        private Dictionary<string, int[]> IdToPDPGroup;

        public ExportView()
        {
            InitializeComponent();
            comboBoxExportType.SelectedIndex = 0;
        }

        public void SetMode(ExportMode mode)
        {
            CurrentMode = mode;
            labelMode.Text = $"Export Mode: {CurrentMode.ToString()}";
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            string selectedItem = comboBoxExportType.Items[comboBoxExportType.SelectedIndex].ToString();
            if (selectedItem == "CSV")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "CSV File|*.csv";
                saveFile.Title = "Save the Exported CSV";
                if ((CurrentMode == ExportMode.Chart && saveFile.ShowDialog() == DialogResult.OK && ExportChartCSV(saveFile.FileName))
                    || (CurrentMode == ExportMode.Events && saveFile.ShowDialog() == DialogResult.OK &&  ExportEventsCSV(saveFile.FileName)))
                {
                    MessageBox.Show("Export to CSV Complete!");
                    return;
                }
                MessageBox.Show("Failed to write CSV!");
            }
            else if (selectedItem == "Clipboard")
            {
                if ((CurrentMode == ExportMode.Chart && ExportChartClipboard()) 
                    || (CurrentMode == ExportMode.Events && ExportEventsClipboard()))
                {
                    MessageBox.Show("Export to Clipboard Complete!");
                    return;
                }
                MessageBox.Show("Export to Clipboard Failed!");
            }
            else if (selectedItem == "Chart Image")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "PNG FIle|*.png";
                saveFile.Title = "Save the PNG Image";
                if (CurrentMode == ExportMode.Chart && saveFile.ShowDialog() == DialogResult.OK && ExportChartImage(saveFile.FileName))
                {
                    MessageBox.Show("Export to Image Complete!");
                    return;
                }
                MessageBox.Show("Failed Export to Image!");
            }
        }

        private bool ExportChartCSV(string file)
        {

            var csv = GetTableFromView(",");
            if (string.IsNullOrWhiteSpace(csv))
            {
                return false;
            }
            File.WriteAllText(file, csv);
            return true;
        }

        private string GetTableFromView(string sep = ",")
        {
            var entries = DSGraph.GetInViewEntries();
            if (entries == null) return "";
            StringBuilder csv = new StringBuilder();

            List<string> headers = new List<string>();
            headers.Add("Time");
            foreach (string value in EnabledSeries.Values)
            {
                headers.Add(value);
            }
            csv.Append(string.Join(sep, headers) + "\n");
            foreach (var entry in entries)
            {
                List<string> row = new List<string>();
                row.Add(entry.Time.ToString("HH:mm:ss.fff"));
                foreach (string key in EnabledSeries.Keys)
                {
                    row.Add(entry.EntryAttToUnit(key, IdToPDPGroup, false));
                }
                csv.Append(string.Join(sep, row) + "\n");
            }
            return csv.ToString();
        }

        private bool ExportChartClipboard()
        {
            var table = GetTableFromView("\t");
            if (string.IsNullOrWhiteSpace(table))
            {
                
                return false;
            }
            Clipboard.SetText(table);
            return true;
        }

        private bool ExportChartImage(string file)
        {
            DSGraph.SaveChartImage(file);
            return true;
        }

        private bool ExportEventsCSV(string file)
        {
            var events = GetEventsFromView(",");
            if (string.IsNullOrWhiteSpace(events))
            {
                return false;
            }
            File.WriteAllText(file, events);
            return true;
        }

        private bool ExportEventsClipboard()
        {
            var events = GetEventsFromView("\t");
            if (string.IsNullOrWhiteSpace(events))
            {
                return false;
            }
            Clipboard.SetText(events);
            return true;
        }

        private string GetEventsFromView(string sep = ",")
        {
            var events = DSEvents.GetEntries();
            StringBuilder table = new StringBuilder();
            table.Append($"Time{sep}Data\n");
            foreach (var eventE in events)
            {
                table.Append($"{eventE.Item1}{sep}{eventE.Item2}\n");
            }
            return table.ToString();
        }

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            IdToPDPGroup = new Dictionary<string, int[]>();
            foreach (var group in pdp)
            {
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith(DSAttConstants.PDPPrefix)).Select(n => int.Parse(n.Name.Replace(DSAttConstants.PDPPrefix, ""))).ToArray();
                foreach (var node in group.Childern)
                {
                    if (node.Name.StartsWith(DSAttConstants.TotalPrefix) || node.Name.Contains(DSAttConstants.DeltaPrefix))
                    {
                        IdToPDPGroup[node.Name] = pdpIds;
                    }
                }
            }
        }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            EnabledSeries.Clear();
            foreach(TreeNode group in groups)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    if (node.Name == DSAttConstants.Messages || !node.Checked) continue;

                    EnabledSeries.Add(node.Name, node.Text);
                }
            }

            labelTotalCol.Text = $"Total  Columns: {EnabledSeries.Count+1}";//+1 for time
        }
    }
}
