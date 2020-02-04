using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                if (CurrentMode == ExportMode.Chart && saveFile.ShowDialog() == DialogResult.OK) ExportChartCSV(saveFile.FileName);
            }
            else if (selectedItem == "Clipboard")
            {
                if (CurrentMode == ExportMode.Chart) ExportChartClipboard();
            }
            else if (selectedItem == "Chart Image")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "PNG FIle|*.png";
                saveFile.Title = "Save the PNG Image";
                if (CurrentMode == ExportMode.Chart && saveFile.ShowDialog() == DialogResult.OK) ExportChartImage(saveFile.FileName);
            }
        }

        private void ExportChartCSV(string file)
        {
            StringBuilder csv = new StringBuilder();
            var entries = DSGraph.GetInViewEntries();
            if (entries == null) return;
            
            foreach(var entry in entries)
            {
                //List<string>
            }
        }

        private void ExportChartClipboard()
        {

        }

        private void ExportChartImage(string file)
        {

        }

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            //throw new NotImplementedException();
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

            labelTotalCol.Text = $"Total  Columns: {EnabledSeries.Count}";
        }
    }
}
