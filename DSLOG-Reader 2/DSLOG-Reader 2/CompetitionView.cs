using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public partial class CompetitionView : UserControl, SeriesViewObserver
    {
        private enum GraphMode
        {
            Both,
            Auto,
            Tele
        }
        public FileListView FileView { get; set; }
        public SeriesView SeriesViewObserving { get; set; }

        private const double TotalPDPScale = 50;
        private Dictionary<string, Tuple<string, Color>> EnabledSeries = new Dictionary<string, Tuple<string, Color>>();
        private Dictionary<string, int[]> IdToPDPGroup;
        private int xLabelOffset = 0;
        private List<DSLOGReader> MatchReaders = new List<DSLOGReader>();
        private List<DSLOGFileEntry> Matches = new List<DSLOGFileEntry>();
        private GraphMode CurrentMode = GraphMode.Both;
        public CompetitionView()
        {
            InitializeComponent();
            for (double i = 0; i < 12; i++)
            {
                chart.ChartAreas[0].AxisY2.CustomLabels.Add(i * 10, i * 10 + .001, $"{ i * 10} ({i * TotalPDPScale})");
            }
            chart.ChartAreas[0].AxisY2.CustomLabels.Add(120, 120 - .001, $"{ 120} ({12 * TotalPDPScale})");
            SetSeries();
            comboBoxMode.SelectedIndex = 0;
        }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            EnabledSeries.Clear();
            foreach (TreeNode group in groups)
            {
                if (group.Text == "Robot Mode") continue;
                foreach (TreeNode node in group.Nodes)
                {
                    if (node.Name == DSAttConstants.Messages || !node.Checked) continue;

                    EnabledSeries.Add(node.Name, new Tuple<string, Color>(node.Text, node.BackColor));
                }
            }
            PlotMatches();

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
            PlotMatches();
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
            s.YAxisType = AxisType.Secondary;
            chart.Series.Add(s);

            Series sv = new Series("boxplotV");
            sv.ChartType = SeriesChartType.FastPoint;
            sv.BorderColor = Color.White;
            sv.Color = Color.Transparent;
            sv.BorderWidth = 1;
            sv.YAxisType = AxisType.Primary;
            chart.Series.Add(sv);


        }


        public void SetEventMatches(List<DSLOGFileEntry> matches)
        {
            
            if (backgroundWorkerReadMatches.IsBusy)
            {
                backgroundWorkerReadMatches.CancelAsync();
                while (backgroundWorkerReadMatches.IsBusy)
                    Application.DoEvents();
            }
            Matches.Clear();
            MatchReaders.Clear();
            Matches = matches;
            progressBarEvents.Value = 0;
            backgroundWorkerReadMatches.RunWorkerAsync();
        }

        private void PlotMatches()
        {
            if (backgroundWorkerReadMatches.IsBusy) return;
            xLabelOffset = 0;
            Util.ClearPointsQuick(chart.Series[0]);
            Util.ClearPointsQuick(chart.Series[1]);
            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.Annotations.Clear();
            chart.Titles.Clear();
            if (Matches.Count == 0) return;

            for (int i = 0; i < Matches.Count; i++)
            {
                var reader = MatchReaders[i];
                var match = Matches[i];
                if (reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout).Count() < 10)
                {
                    continue;
                }
                int oldOffset = xLabelOffset - 1;
                if (CurrentMode == GraphMode.Both) PlotMatch(reader.Entries.Where(en => en.RobotAuto || en.RobotTele || en.Brownout || en.DSTele || en.DSAuto));
                else if (CurrentMode == GraphMode.Tele) PlotMatch(reader.Entries.Where(en => en.RobotTele || en.DSTele || (en.Brownout && en.DSTele)));
                else if (CurrentMode == GraphMode.Auto) PlotMatch(reader.Entries.Where(en => en.RobotAuto || en.DSAuto || (en.Brownout && en.DSAuto)));

                var custom = new CustomLabel(oldOffset, xLabelOffset, $"{match.MatchType} {match.FMSMatchNum}", 0, LabelMarkStyle.None);
                custom.ForeColor = match.GetMatchTypeColor();
                chart.ChartAreas[0].AxisX.CustomLabels.Add(custom);
                if (i != Matches.Count - 1)
                {
                    LineAnnotation annotation = new LineAnnotation();
                    annotation.IsSizeAlwaysRelative = false;
                    annotation.AxisX = chart.ChartAreas[0].AxisX;
                    annotation.AxisY = chart.ChartAreas[0].AxisY2;
                    annotation.AnchorX = xLabelOffset;
                    annotation.AnchorY = 0;
                    annotation.Height = 113;
                    annotation.Width = 0;
                    annotation.LineWidth = 1;
                    annotation.StartCap = LineAnchorCapStyle.None;
                    annotation.EndCap = LineAnchorCapStyle.None;
                    annotation.LineColor = Color.White;


                    chart.Annotations.Add(annotation);
                }

                xLabelOffset += 1;
            }

            List<string> title = new List<string>();
            foreach (string node in EnabledSeries.Keys)
            {
                title.Add(EnabledSeries[node].Item1);
            }
            chart.Titles.Add(new Title(string.Join(", ", title), Docking.Top, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Color.White));
        }

        private void PlotMatch(IEnumerable<DSLOGEntry> data)
        {
            var dumpyData = new DataPoint(0, 0);
            dumpyData.BorderColor = Color.Transparent;
            chart.Series[1].Points.Add(dumpyData);

            
            foreach (string node in EnabledSeries.Keys)
            {

               
                if (node.StartsWith(DSAttConstants.PDPPrefix))
                {
                    int id = int.Parse(node.Replace(DSAttConstants.PDPPrefix, ""));
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.GetPDPChannel(id)).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);

                }

                else if (node == DSAttConstants.Voltage)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.Voltage*7.1).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }
                else if (node == DSAttConstants.CANUtil)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.CANUtil * 100.0).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }
                else if (node == DSAttConstants.RoboRIOCPU)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.RoboRioCPU * 100.0).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }


                else if (node == DSAttConstants.TripTime)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.TripTime).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }
                else if (node == DSAttConstants.LostPackets)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => Math.Max(0, en.LostPackets) * 100.0).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }

                else if (node == DSAttConstants.TotalPDP)
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.GetDPDTotal() / (TotalPDPScale / 10.0)).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    chart.Series[0].Points.Add(d);
                }

                else if (node.Contains(DSAttConstants.TotalPrefix))
                {

                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.GetGroupPDPTotal(IdToPDPGroup[node].ToArray()) / (TotalPDPScale / 10.0)).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    d.BorderDashStyle = ChartDashStyle.Dash;
                    chart.Series[0].Points.Add(d);
                }
                else if (node.Contains(DSAttConstants.DeltaPrefix))
                {
                    var d = new DataPoint(xLabelOffset, GenerateBoxPlotData(data.Select(en => en.GetGroupPDPSd(IdToPDPGroup[node].ToArray())).ToList()));
                    d.BorderColor = EnabledSeries[node].Item2;
                    d.BorderDashStyle = ChartDashStyle.Dash;
                    chart.Series[0].Points.Add(d);
                }

                xLabelOffset += 1;

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
            while (min < q1 - awayOff * iqr)
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
            output.AddRange(new double[] { min, max, q1, q3, mean, median });
            output.AddRange(outliers);
            return output.ToArray();
        }

        private void backgroundWorkerReadMatches_DoWork(object sender, DoWorkEventArgs e)
        {
            int num = 0;
            double matchCount = Matches.Count;
            foreach (var match in Matches)
            {
                DSLOGReader reader = new DSLOGReader($"{FileView.GetPath()}\\{match.Name}.dslog");
                reader.Read();
                MatchReaders.Add(reader);
                backgroundWorkerReadMatches.ReportProgress((int)((((double)++num)/Matches.Count)*100.0));
                if (backgroundWorkerReadMatches.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void backgroundWorkerReadMatches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled) PlotMatches();
        }

        private void backgroundWorkerReadMatches_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarEvents.Value = e.ProgressPercentage;
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMode.SelectedIndex == 0) CurrentMode = GraphMode.Both;
            if (comboBoxMode.SelectedIndex == 1) CurrentMode = GraphMode.Auto;
            if (comboBoxMode.SelectedIndex == 2) CurrentMode = GraphMode.Tele;
            PlotMatches();
        }
    }
}
