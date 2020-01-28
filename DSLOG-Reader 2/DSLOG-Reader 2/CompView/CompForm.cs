using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2.CompView
{
    public partial class CompForm : Form
    {
        public MainForm MForm { get; set; }
        public FileListView FileView { get; set; }
        private GroupProfiles Profiles;
        private SeriesGroupNodes NonEditGroups;
        private bool checkMode = false;
        private int lastSelectedEvent = -1;

        public CompForm()
        {
            InitializeComponent();
        }

        public void SetProfiles(GroupProfiles profiles)
        {
            Profiles = profiles;
            comboBoxProfiles.Items.Clear();
            comboBoxProfiles.Items.AddRange(Profiles.Select(e => e.Name).ToArray());
            comboBoxProfiles.SelectedIndex = 0;
        }

        public void SetNonEditGroups(SeriesGroupNodes groups)
        {
            NonEditGroups = groups;
        }

        private void ComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            foreach (var group in NonEditGroups)
            {
                if (group.Name == "robotMode") continue;
                var tree = group.ToTreeNode();
                if (group.Name == "other") tree.Nodes.RemoveByKey("messages");
                treeView.Nodes.Add(tree);

            }
            
            foreach (var group in Profiles[comboBoxProfiles.SelectedIndex].Groups)
            {
                treeView.Nodes.Add(group.ToTreeNode());
            }
            treeView.ExpandAll();
            SetSeries();
        }

        private void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Checked = !e.Node.Checked;
            DoChecking(e.Node);
            e.Cancel = true;
        }

        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown) return;

            DoChecking(e.Node);
        }

        private void DoChecking(TreeNode e)
        {
            if (!checkMode)
            {
                checkMode = true;
                if (e.Nodes.Count > 0 || e.Parent == null)//parent
                {
                    foreach (TreeNode node in e.Nodes)
                    {
                        node.Checked = e.Checked;
                    }
                }
                else//child
                {
                    if (!e.Checked)
                    {
                        e.Parent.Checked = false;
                    }
                    if (e.Checked)
                    {
                        foreach (TreeNode node in e.Parent.Nodes)
                        {
                            if (!node.Checked)
                            {
                                e.Parent.Checked = false;
                                checkMode = false;
                                //SetChartSeriesEnabled();
                                return;
                            }
                        }
                        e.Parent.Checked = true;
                    }
                }
                //SetChartSeriesEnabled();
                checkMode = false;
            }
        }

        private void ComboBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastSelectedEvent != comboBoxEvents.SelectedIndex)
            {
                PlotMatches(FileView.GetMatches(comboBoxEvents.SelectedItem.ToString()));
               lastSelectedEvent = comboBoxEvents.SelectedIndex;
            }
        }

        public void SetEvents(List<string> events)
        {
            comboBoxEvents.Items.Clear();
            comboBoxEvents.Items.AddRange(events.ToArray());
        }

        private void CompForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void SetSeries()
        {
            chart.Series.Clear();
            Series s = new Series("boxplotPDP");
            s.ChartType = SeriesChartType.BoxPlot;
            s.BorderColor = Color.White;
            s.Color = Color.Transparent;
            s.BorderWidth = 1;
            chart.Series.Add(s);

            Series sv = new Series("boxplotV");
            sv.ChartType = SeriesChartType.BoxPlot;
            sv.BorderColor = Color.White;
            sv.Color = Color.Transparent;
            sv.BorderWidth = 1;
            chart.Series.Add(sv);


        }
        int xOffset = 0;
        private void PlotMatches(List<DSLOGFileEntry> matches)
        {
            xOffset = 0;
            //Series boxPlots = chart.Series["boxplot"];
            Util.ClearPointsQuick(chart.Series[0]);
            Util.ClearPointsQuick(chart.Series[1]);

            foreach (var match in matches)
            {
                DSLOGReader reader = new DSLOGReader($"{FileView.GetPath()}\\{match.Name}.dslog");
                reader.Read();
                if (reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout).Count() < 10)
                {
                    continue;
                }
                PlotMatch(reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout));
                xOffset += 1;
            }

        }

        private void PlotMatch(IEnumerable<DSLOGEntry> data)
        {
            foreach(TreeNode group in treeView.Nodes)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    if (!node.Checked) continue;
                    if (node.Name.StartsWith("pdp"))
                    {
                        int id = int.Parse(node.Name.Replace("pdp", ""));
                        var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en=>en.GetPDPChannel(id)).ToList()));
                        d.BorderColor = node.BackColor;
                        chart.Series[0].Points.Add(d);
                    }
                   
                    xOffset += 1;
                }
            }
        }

        private double[] GenerateBoxPlotData(List<double> data)
        {
            data.Sort();
            List<double> outliers = new List<double>();
            double min = data.Min();
            double max = data.Max();
            double mean = data.Average();
            double median = data[data.Count / 2];
            double q1 = data[data.Count / 4];
            double q3 = data[(3 * data.Count) / 4];
            double iqr = q3 - q1;
            int i = 0;
            double awayOff = 2;
            while(min < q1 - awayOff * iqr)
            {
                outliers.Add(data[i++]);
                min = data[i];
            }
            i = data.Count - 1;
            while (max > q3 + awayOff * iqr)
            {
                outliers.Add(data[i--]);
                max = data[i];
            }
            List<double> output = new List<double>();
            output.AddRange(new double[]{ min, max, q1, q3, mean, median});
            output.AddRange(outliers);
            return output.ToArray();
        }
    }
}
