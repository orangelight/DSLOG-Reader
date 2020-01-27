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
            Series s = new Series("boxplot");
            s.ChartType = SeriesChartType.BoxPlot;
            s.BorderColor = Color.White;
            s.Color = Color.Transparent;
            s.BorderWidth = 1;
            chart.Series.Add(s);
           
           
        }

        private void PlotMatches(List<DSLOGFileEntry> matches)
        {
            
            Series boxPlots = chart.Series["boxplot"];
            Util.ClearPointsQuick(boxPlots);
            for(int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                DSLOGReader reader = new DSLOGReader($"{FileView.GetPath()}\\{match.Name}.dslog");
                reader.Read();
                List<double> data = reader.Entries.Where(en => en.RobotAuto || en.RobotTele).Select(en=> en.CANUtil).ToList();
                if (data.Count == 0)
                {
                    continue;
                }
                data.Sort();
                double min = data.Min();
                double max = data.Max();
                double mean = data.Average();
                double median = data[data.Count / 2];
                double q1 = data[data.Count / 4];
                double q3 = data[(3*data.Count) / 4];
                var d = new DataPoint(i, new double[] { min, max, q1, q3, mean, median });
                boxPlots.Points.Add(d);

            }
            //Random r = new Random();

            //for (int i = 0; i < 20; i++)
            //{
            //    var num = i % chart.Series.Count + i / chart.Series.Count;
            //    var d = new DataPoint(i, new double[] { 0 + num, 10 + num, 2 + num, 8 + num, 4 + num, 6 + num });
            //    d.BorderColor = chart.Series[i % chart.Series.Count].BorderColor;
            //    d.BorderDashStyle = chart.Series[i % chart.Series.Count].BorderDashStyle;
            //    //d.Color = Color.Transparent;
            //    chart.Series[0].Points.Add(d);
            //    //chart.Series[0]["PointWidth"] = "2";

            //}
        }
    }
}
