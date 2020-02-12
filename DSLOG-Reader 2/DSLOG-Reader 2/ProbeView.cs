using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSLOG_Reader_Library;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSLOG_Reader_2
{
    public partial class ProbeView : UserControl, SeriesViewObserver
    {
        private DSLOGEntry Entry;
        private Dictionary<string, int[]> IdToPDPGroup;
        private Dictionary<string, Tuple<string, Color>> EnabledSeries = new Dictionary<string, Tuple<string, Color>>();
        public ProbeView()
        {
            InitializeComponent();
        }

        public SeriesView SeriesViewObserving { get; set; }

      

        public void SetProbe(DSLOGEntry entry)
        {
            Entry = entry;
            DisplayEntry();
        }
        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            EnabledSeries.Clear();
            foreach (TreeNode group in groups)
            {
                foreach (TreeNode node in group.Nodes)
                {
                    if (node.Name == DSAttConstants.Messages || !node.Checked) continue;

                    EnabledSeries.Add(node.Name, new Tuple<string, Color>(node.Text, node.BackColor));
                }
            }
            DisplayEntry();
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
            DisplayEntry();
        }

        private void DisplayEntry()
        {
            Controls.Clear();
            if (Entry != null && IdToPDPGroup != null)
            {
                int labelNum = 0;
                Label timeLabel = new Label();
                timeLabel.Text = $"Time: {Entry.Time.ToString("HH:mm:ss.fff")}";
                timeLabel.Visible = true;
                timeLabel.AutoSize = true;
                timeLabel.Location = new Point(4, (24 * labelNum++) + 7);
                Controls.Add(timeLabel);
                foreach (var ser in EnabledSeries)
                {
                    Label seriesLabel = new Label();
                    seriesLabel.Text = $"{ser.Value.Item1}: { Entry.EntryAttToUnit(ser.Key, IdToPDPGroup)}";
                    seriesLabel.Visible = true;
                    seriesLabel.AutoSize = true;
                    //seriesLabel.BackColor = ser.Value.Item2;
                    seriesLabel.Location = new Point(4, ((seriesLabel.Height) * labelNum++) + 7);
                    Controls.Add(seriesLabel);
                }
            }
        }
    }
}
