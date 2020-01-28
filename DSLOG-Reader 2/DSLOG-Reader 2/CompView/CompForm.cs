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
        private const double TotalPDPScale = 50;
        private List<DSLOGReader> MatchReaders;
        private List<DSLOGFileEntry> Matches;

        public CompForm()
        {
            InitializeComponent();
            treeView.Enabled = false;
            MatchReaders = new List<DSLOGReader>();
            Matches = new List<DSLOGFileEntry>();
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
                                PlotMatches();
                                return;
                            }
                        }
                        e.Parent.Checked = true;
                    }
                }
                PlotMatches();
                checkMode = false;
            }
        }

        private void ComboBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEvents.SelectedIndex != 0)
            {
                treeView.Enabled = true;
            }
            if (lastSelectedEvent != comboBoxEvents.SelectedIndex)
            {
                Matches = FileView.GetMatches(comboBoxEvents.SelectedItem.ToString());
                ReadMatches(Matches);
                PlotMatches();
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
            s.MarkerSize = 2;
            chart.Series.Add(s);

            Series sv = new Series("boxplotV");
            sv.ChartType = SeriesChartType.BoxPlot;
            sv.BorderColor = Color.White;
            sv.Color = Color.Transparent;
            sv.BorderWidth = 1;
            sv.YAxisType = AxisType.Secondary;
            chart.Series.Add(sv);


        }
        int xOffset = 0;

        private void ReadMatches(List<DSLOGFileEntry> matches)
        {
            MatchReaders.Clear();
            foreach (var match in matches)
            {
                DSLOGReader reader = new DSLOGReader($"{FileView.GetPath()}\\{match.Name}.dslog");
                reader.Read();
                MatchReaders.Add(reader);
            }
        }
        private void PlotMatches()
        {
            xOffset = 0;
            //Series boxPlots = chart.Series["boxplot"];
            Util.ClearPointsQuick(chart.Series[0]);
            Util.ClearPointsQuick(chart.Series[1]);
            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.Annotations.Clear();
            for (int i = 0; i < Matches.Count; i++)
            {
                var reader = MatchReaders[i];
                var match = Matches[i];
                if (reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout).Count() < 10)
                {
                    continue;
                }
                int oldOffset = xOffset-1;
                PlotMatch(reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout));
                var custom = new CustomLabel(oldOffset, xOffset, $"{match.MatchType} {match.FMSMatchNum}", 0, LabelMarkStyle.None);
                custom.ForeColor = match.GetMatchTypeColor();
                chart.ChartAreas[0].AxisX.CustomLabels.Add(custom);
                LineAnnotation annotation = new LineAnnotation();
                annotation.IsSizeAlwaysRelative = false;
                annotation.AxisX = chart.ChartAreas[0].AxisX;
                annotation.AxisY = chart.ChartAreas[0].AxisY;
                annotation.AnchorX = xOffset;
                annotation.AnchorY = 0;
                annotation.Height = 120;
                annotation.Width = 0;
                annotation.LineWidth = 1;
                annotation.StartCap = LineAnchorCapStyle.None;
                annotation.EndCap = LineAnchorCapStyle.None;
                annotation.LineColor = Color.White;
                
                chart.Annotations.Add(annotation);
                xOffset += 1;
            }

        }

        private void PlotMatch(IEnumerable<DSLOGEntry> data)
        {
            List<string> title = new List<string>();
            foreach(TreeNode group in treeView.Nodes)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    if (!node.Checked) continue;
                    title.Add(node.Text);
                    if (node.Name.StartsWith("pdp"))
                    {
                        int id = int.Parse(node.Name.Replace("pdp", ""));
                        var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en=>en.GetPDPChannel(id)).ToList()));
                        d.BorderColor = node.BackColor;
                        chart.Series[0].Points.Add(d);

                    }
                    else if (group.Name == "basic")
                    {
                        if (node.Name == "voltage")
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.Voltage).ToList()));
                            d.BorderColor = node.BackColor;
                            chart.Series[0].Points.Add(d);
                        }
                        else if (node.Name == "can")
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.CANUtil*100.0).ToList()));
                            d.BorderColor = node.BackColor;
                            chart.Series[0].Points.Add(d);
                        }
                        else
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.RoboRioCPU * 100.0).ToList()));
                            d.BorderColor = node.BackColor;
                            chart.Series[0].Points.Add(d);
                        }
                    }
                    else if (group.Name == "comms")
                    {
                        if (node.Name == "tripTime")
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.TripTime).ToList()));
                            d.BorderColor = node.BackColor;
                            chart.Series[0].Points.Add(d);
                        }
                        else
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => Math.Max(0, en.LostPackets)*100.0).ToList()));
                            d.BorderColor = node.BackColor;
                            chart.Series[0].Points.Add(d);
                        }
                    }
                    else if (node.Name == "totalPdp")
                    {
                        var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.GetDPDTotal() / (TotalPDPScale / 10.0)).ToList()));
                        d.BorderColor = node.BackColor;
                        chart.Series[0].Points.Add(d);
                    }
                    else if (group.Name.StartsWith("group"))
                    {
                        
                        List<int> pdpIds = new List<int>();
                        foreach(TreeNode n in group.Nodes)
                        {
                            if (n.Name.StartsWith("pdp")) pdpIds.Add(int.Parse(n.Name.Replace("pdp", "")));
                        }

                        if (node.Name.Contains("total") || node.Name.Contains("delta"))
                        {
                            
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.GetGroupPDPTotal(pdpIds.ToArray()) / (TotalPDPScale / 10.0)).ToList()));
                            d.BorderColor = node.BackColor;
                            d.BorderDashStyle = ChartDashStyle.Dash;
                            chart.Series[0].Points.Add(d);
                        }
                        else
                        {
                            var d = new DataPoint(xOffset, GenerateBoxPlotData(data.Select(en => en.GetGroupPDPSd(pdpIds.ToArray()) / (TotalPDPScale / 10.0)).ToList()));
                            d.BorderColor = node.BackColor;
                            d.BorderDashStyle = ChartDashStyle.Dash;
                            chart.Series[0].Points.Add(d);
                        }
                    }
                    xOffset += 1;
                }
            }
            chart.Titles.Clear();
            chart.Titles.Add(new Title(string.Join(", ", title), Docking.Top, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Color.White));
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //TODO Add save dialog
            chart.SaveImage("chart.png", ChartImageFormat.Png);
        }
    }
}
