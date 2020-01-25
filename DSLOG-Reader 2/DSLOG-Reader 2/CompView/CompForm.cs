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

namespace DSLOG_Reader_2.CompView
{
    public partial class CompForm : Form
    {
        public MainForm MForm { get; set; }
        private GroupProfiles Profiles;
        private SeriesGroupNodes NonEditGroups;
        private bool checkMode = false;

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
            foreach (var group in NonEditGroups)
            {
                if (group.Name == "robotMode") continue;
                foreach(var node in group.Childern)
                {
                    if (group.Name == "other" && node.Name == "messages") continue;
                    Series s = new Series(node.Name);
                    s.ChartType = SeriesChartType.BoxPlot;
                    s.BorderColor = node.Color;
                    s.Color = Color.Transparent;
                    s.BorderWidth = 1;
                    chart.Series.Add(s);

                }
            }

            foreach (var group in Profiles[comboBoxProfiles.SelectedIndex].Groups)
            {
                foreach (var node in group.Childern)
                {
                    Series s = new Series(node.Name);
                    s.ChartType = SeriesChartType.BoxPlot;
                    s.BorderColor = node.Color;
                    s.Color= Color.Transparent;
                    s.BorderWidth = 1;
                    if (node.Name.StartsWith("total") || node.Name.StartsWith("delta")) s.BorderDashStyle = ChartDashStyle.Dash;
                    chart.Series.Add(s);
                }
            }
            Random r = new Random();
            for (int i = 0; i < chart.Series.Count*5; i++)
            {
                var num = i% chart.Series.Count + i/ chart.Series.Count;
                var d = new DataPoint(i, new double[] { 0+ num, 10 + num, 2 + num, 8 + num, 4 + num, 6 + num });
                d.BorderColor = chart.Series[i% chart.Series.Count].BorderColor;
                d.BorderDashStyle = chart.Series[i % chart.Series.Count].BorderDashStyle;
                //d.Color = Color.Transparent;
                chart.Series[0].Points.Add(d);
                //chart.Series[0]["PointWidth"] = "2";

            }
        }
    }
}
