using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public partial class MainGraphView : UserControl, SeriesViewObserver
    {

        private Dictionary<string, Series> SeriesSettings;
        private DateTime StartTime;
        private DateTime EndTime;
        private const double TotalPDPScale = 50;
        private Dictionary<string, int[]> IdToPDPGroup;
        private Point? prevPosition = null;
        public MainForm MForm {get;set; }
        public EventsView EventsView { get; set; }
        public ProbeView ProbeView { get; set; }
        public EnergyView EnergyView { get; set; }
        public SeriesView SeriesViewObserving { get; set; }

        private int PointCount = 1;
        private List<DSLOGEntry> LogEntries;
        private DSLOGFileEntry LogInfo;
        private DSLOGStreamer LogStreamer;
        private int LastEntry = 0;
        private bool AutoScrollLive = false;
        public DateTime MatchTime { get; private set; }
        public bool UseMatchTime { get; private set; }
        public bool CanUseMatchTime { get; private set; }
        private double lastInviewSecs = -1;
        private volatile bool LoadingLog = false, PlottingLog = false;
        private bool InitBasic = false;
        private DateTime LastEnergyAdd;
        public MainGraphView()
        {
            InitializeComponent();
            CanUseMatchTime = false;
            UseMatchTime = false;
            InitSeriesSettings();
            toolTip.SetToolTip(buttonAutoScroll, "Stream Log Autoscroll");
            toolTip.SetToolTip(buttonFindMatch, "Zoom into Match");
            toolTip.SetToolTip(buttonMatchTime, "Use Match Time");
            toolTip.SetToolTip(buttonResetZoom, "Reset Zoom");
            
            
        }

        public void InitSeriesSettings()
        {
            SeriesSettings = new Dictionary<string, Series>();
            Series modes = new Series();
            modes.XValueType = ChartValueType.DateTime;
            modes.YAxisType = AxisType.Primary;
            modes.ChartType = SeriesChartType.FastPoint;
            //modes.BorderWidth = 5;
            modes.MarkerStyle = MarkerStyle.Circle;
            SeriesSettings["modes"] = modes;

            Series lines = new Series();
            lines.XValueType = ChartValueType.DateTime;
            lines.YAxisType = AxisType.Secondary;
            lines.ChartType = SeriesChartType.FastLine;
            SeriesSettings["lines"] = lines;

            Series totallines = new Series();
            totallines.XValueType = ChartValueType.DateTime;
            totallines.YAxisType = AxisType.Secondary;
            totallines.ChartType = SeriesChartType.FastLine;
            SeriesSettings["totallines"] = totallines;

            Series voltage = new Series();
            voltage.XValueType = ChartValueType.DateTime;
            voltage.YAxisType = AxisType.Primary;
            voltage.ChartType = SeriesChartType.FastLine;
            SeriesSettings["voltage"] = voltage;

            Series messages = new Series();
            messages.XValueType = ChartValueType.DateTime;
            messages.YAxisType = AxisType.Primary;
            messages.ChartType = SeriesChartType.Point;
            messages.MarkerStyle = MarkerStyle.Circle;
            SeriesSettings["messages"] = messages;
        }

        public async void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            WaitForLoadingPlotting();
            IdToPDPGroup = new Dictionary<string, int[]>();
            //ClearGraph();
            RemoveDeltaTotalSeries();

            //chart.Series.Clear();
            if (!InitBasic)
            {
                foreach (var group in basic)
                {
                    foreach (var node in group.Childern)
                    {
                        if (group.Name == "robotMode")
                        {
                            var newSeries = MakeSeriesFromSettings(SeriesSettings["modes"], node);
                            chart.Series.Add(newSeries);
                        }
                        else if (group.Name == "basic")
                        {
                            if (node.Name == DSAttConstants.Voltage)
                            {
                                var newSeries = MakeSeriesFromSettings(SeriesSettings["voltage"], node);
                                chart.Series.Add(newSeries);
                            }
                            else
                            {
                                var newSeries = MakeSeriesFromSettings(SeriesSettings["lines"], node);
                                chart.Series.Add(newSeries);
                            }
                        }
                        else if (group.Name == "comms")
                        {
                            var newSeries = MakeSeriesFromSettings(SeriesSettings["lines"], node);
                            chart.Series.Add(newSeries);
                        }
                        else if (group.Name == "other")
                        {
                            if (node.Name == DSAttConstants.Messages)
                            {
                                var newSeries = MakeSeriesFromSettings(SeriesSettings["messages"], node);
                                chart.Series.Add(newSeries);
                            }
                            else
                            {
                                var newSeries = MakeSeriesFromSettings(SeriesSettings["totallines"], node);
                                chart.Series.Add(newSeries);
                            }
                        }
                    }
                }

                foreach (var group in pdp)
                {
                    foreach (var node in group.Childern)
                    {

                        if (!node.Name.StartsWith(DSAttConstants.TotalPrefix) && !node.Name.Contains(DSAttConstants.DeltaPrefix))
                        {

                            var newSeries = MakeSeriesFromSettings(SeriesSettings["lines"], node);
                            chart.Series.Add(newSeries);
                        }
                    }
                }
                InitBasic = true;
            }

            Dictionary<string, Series> deltaTotalSeries = new Dictionary<string, Series>();
            foreach (var group in pdp)
            {
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith(DSAttConstants.PDPPrefix)).Select(n => int.Parse(n.Name.Replace(DSAttConstants.PDPPrefix, ""))).ToArray();
                foreach (var node in group.Childern)
                {
                    
                    if (node.Name.StartsWith(DSAttConstants.TotalPrefix)|| node.Name.Contains(DSAttConstants.DeltaPrefix))
                    {
                       
                        var newSeries = MakeSeriesFromSettings(SeriesSettings["totallines"], node);
                        IdToPDPGroup[node.Name] = pdpIds;
                        newSeries.BorderDashStyle = ChartDashStyle.Dash;
                        deltaTotalSeries[node.Name] = newSeries;
                    }
                    else
                    {
                       // var newSeries = MakeSeriesFromSettings(SeriesSettings["lines"], node);
                        //chart.Series.Add(newSeries);
                    }
                    
                }
            }

            if (LogInfo != null)
            {
                PlottingLog = true;
                PlotTotalDelta(deltaTotalSeries, 0, LogEntries.Count);
                
                /// LastEntry = 0;
                //RemoveDeltaTotalSeries();
                //var t = Task.Run(() => { PlotLog(); });
                //await t;
                //EventsView.AddEvents();
            }
            foreach(var s in deltaTotalSeries)
            {
                chart.Series.Add(s.Value);
            }
            PlottingLog = false;
        }

        private void RemoveDeltaTotalSeries()
        {
            List<Series> toRemove = new List<Series>();
            foreach(var s in chart.Series)
            {
                if (s.Name.StartsWith(DSAttConstants.DeltaPrefix) || s.Name.StartsWith(DSAttConstants.TotalPrefix))
                {
                    toRemove.Add(s);
                }
            }
            foreach(var s in toRemove)
            {
                chart.Series.Remove(s);
            }
        }

        private Series MakeSeriesFromSettings(Series setting, SeriesChildNode node)
        {
            Series newMode = new Series(node.Name);
            newMode.LegendText = node.Text;
            newMode.Color = node.Color;
            newMode.XValueType = setting.XValueType;
            newMode.YAxisType = setting.YAxisType;
            newMode.ChartType = setting.ChartType;
            newMode.BorderWidth = setting.BorderWidth;
            newMode.MarkerStyle = setting.MarkerStyle;
            return newMode;
        }

        private void WaitForLoadingPlotting()
        {
            while (LoadingLog || PlottingLog) Application.DoEvents();
        }
        public void LoadLog(DSLOGFileEntry logInfo)
        {
            if (logInfo == null) return;
            WaitForLoadingPlotting();

            LoadingLog = true;
            LogInfo = logInfo;
            string dslogFile = $"{logInfo.FilePath}\\{logInfo.Name}.dslog";
            chart.Invoke((Action)(() =>
            {
                StopStreaming();
                LogStreamer = null;
                InitChart();
                LogEntries = null;
                ClearInfoLabel();
                ProbeView.SetProbe(null);
            }));

            if (File.Exists(dslogFile))
            {
                DSLOGReader reader = null;
                LastEntry = 0;
                if (logInfo.Live)
                {
                    LogStreamer = new DSLOGStreamer(dslogFile);
                    reader = LogStreamer;
                    LogStreamer.Stream();
                    
                }
                else
                {
                    reader = new DSLOGReader(dslogFile);
                    try
                    {
                        reader.Read();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Log Corrupted!");
                        LoadingLog = false;
                        return;
                    }

                }
               
                

                if (reader.Version != 4)
                {
                    LoadingLog = false;
                    return;
                }
                chart.Invoke((Action)(() =>
                {

                   

                    ChartArea area = chart.ChartAreas[0];
                    StartTime = reader.StartTime;
                    EndTime = reader.Entries.Last().Time;
                    LogEntries = reader.Entries;
                    area.AxisX.Minimum = StartTime.ToOADate();
                    area.AxisX.Maximum = EndTime.ToOADate();
                    area.CursorX.IntervalOffset = reader.StartTime.Millisecond % 20;


                    labelFileInfo.Text = $"{logInfo.Name}.dslog";


                    if (logInfo.IsFMSMatch)
                    {
                        labelFileInfo.Text = labelFileInfo.Text + $" ({logInfo.EventName} {logInfo.MatchType.ToString()} {logInfo.FMSMatchNum})";
                        labelFileInfo.BackColor = logInfo.GetMatchTypeColor();
                        //buttonAnalysis.Enabled = true;
                        SetUpMatchTime();
                    }
                    else
                    {
                        CanUseMatchTime = false;
                        ChangeUseMatchTime(false);
                    }
                }));


                PlotLog();
                PointCount = LogEntries.Count;

                chart.Invoke((Action)(() =>
                {
                    SetEnergy();

                    chart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    
                }));
                LoadingLog = false;
                labelFileInfo.Invoke((Action)(() => {
                    if (logInfo.Live)
                    {
                        labelFileInfo.BackColor = Color.Lime;
                        BackColor = Color.Lime;
                        timerStream.Start();
                    }
                }));
                SetAutoScrollColor();
            }
            else
            {
                LoadingLog = false;
                MessageBox.Show("Log Corrupted!");
            }
           
        }

        private void SetUpMatchTime()
        {
            DateTime matchTime;
            bool foundMatchTime = LogEntries.TryFindMatchStart(out matchTime);
            if (!foundMatchTime)
            {
                CanUseMatchTime = false;
                ChangeUseMatchTime(false);
            }
            else
            {
                MatchTime = matchTime;
                CanUseMatchTime = true;
                ChangeUseMatchTime(UseMatchTime);
            }
        }

        private void ClearInfoLabel()
        {
            labelFileInfo.Text = "";
            labelFileInfo.BackColor = SystemColors.Control;
            BackColor = SystemColors.Control;
        }
        private void InitChart()
        {
            chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(100);
            ClearGraph();
        }
        private void ClearGraph()
        {
            foreach (Series ser in chart.Series)
            {
                Util.ClearPointsQuick(ser);
            }
        }
        
        private void ChangeChartLabels(bool force = false)
        {
            
            labelTimeInView.Text = $"Time in View: {GetTotalSecoundsInView().ToString("0.##")} Sec";
            if (UseMatchTime && CanUseMatchTime)
            {
                if (Math.Abs(lastInviewSecs - GetTotalSecoundsInView()) > .1 || force)
                {
                    chart.ChartAreas[0].AxisX.CustomLabels.Clear();
                    lastInviewSecs = GetTotalSecoundsInView();
                    double secInv = GetNiceIntervalNumber( lastInviewSecs / 10.0);// Try to keep at leat 5 lines in view

                    double offSet = ((StartTime.AddMilliseconds(-StartTime.Millisecond).AddMilliseconds(MatchTime.Millisecond) - MatchTime).TotalSeconds)%(secInv);
                    

                    DateTime startTime = StartTime.AddMilliseconds(-StartTime.Millisecond).AddSeconds(secInv).AddMilliseconds(MatchTime.Millisecond).AddSeconds(-offSet).AddSeconds(-secInv);
                    while (startTime < EndTime)
                    {
                        double newTime = (startTime - MatchTime).TotalMilliseconds / 1000.0;
                        CustomLabel l = new CustomLabel(startTime.AddSeconds(secInv).ToOADate(), startTime.AddSeconds(-secInv).ToOADate(), $"{newTime.ToString("0.###")}", 0, LabelMarkStyle.None);

                        if (newTime >= 0 && newTime <= 15)
                        {
                            l.ForeColor = Color.Lime;
                            //l.Text = (15.0 - newTime).ToString("0.###");
                        }
                        else if (newTime > 15 && newTime <= 150)
                        {
                            l.ForeColor = Color.Cyan;
                            //l.Text = (135.0 - (newTime-15.0)).ToString("0.###");
                        }
                        l.GridTicks = GridTickTypes.All;
                        chart.ChartAreas[0].AxisX.CustomLabels.Add(l);
                        startTime = startTime.AddSeconds(secInv);
                    }
                }
            }
            else
            {
                lastInviewSecs = -1;
                if (GetTotalSecoundsInView() < 5)
                {
                    chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss.fff";
                }
                else
                {
                    chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";

                }
            }
        }

        private void SetYLabels()
        {
            chart.ChartAreas[0].AxisY2.CustomLabels.Clear();
            foreach (var series in chart.Series)
            {
                if ((series.Name.StartsWith(DSAttConstants.TotalPrefix) || series.Name == DSAttConstants.TotalPDP) && series.Enabled)
                {
                    for (double i = 0; i < 12; i++)
                    {
                        chart.ChartAreas[0].AxisY2.CustomLabels.Add(i * 10, i * 10 + .001, $"{ i * 10} ({i * TotalPDPScale})");
                    }
                    chart.ChartAreas[0].AxisY2.CustomLabels.Add(120, 120 - .001, $"{ 120} ({12 * TotalPDPScale})");
                    break;
                }
            }
        }

        private double GetNiceIntervalNumber(double num)
        {
            double niceInv = 0.025, lastNiceInv = 0.025;
            int i = 1;
            while (num > niceInv)
            {
                lastNiceInv = niceInv;
                if (i % 3 == 0)
                {
                    niceInv *= 2.5;
                    i = 0;
                }
                else
                {
                    niceInv *= 2.0;
                }
                i++;
            }
            return niceInv;

        }

        private double GetTotalSecoundsInView()
        {
            return ((GetIndexOfMaxView() - GetIndexOfMinView()) * 20) / 1000d;
        }

        int GetIndexOfMinView()
        {
            return (int)(DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum).Subtract(StartTime).TotalMilliseconds / 20);
        }

        int GetIndexOfMaxView()
        {
            return (int)(DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum).Subtract(StartTime).TotalMilliseconds / 20);
        }

        private void PlotLog()
        {
            PlottingLog = true;
            if (LogEntries != null)
            {
                Dictionary<string, Series> series = new Dictionary<string, Series>();
                
                
                chart.Invoke((Action)(() =>
                {
                    foreach (var s in chart.Series)
                    {
                        s.Enabled = false;
                        series.Add(s.Name, s);
                    }
                    chart.Series.Clear();
                    chart.Series.Add(series[DSAttConstants.Messages]);
                    series.Remove(DSAttConstants.Messages);
                }));

                var t1 = Task.Run(() =>
                {
                    PlotBasic(series, 0, LogEntries.Count);
                });

                var t2 = Task.Run(() =>
                {
                    PlotModes(series, 0, LogEntries.Count);
                });

                var t3 = Task.Run(() =>
                {
                    PlotPDP(series, 0, LogEntries.Count);
                });

                var t4 = Task.Run(() =>
                {
                    PlotTotalDelta(series, 0, LogEntries.Count);
                });
                
                Task.WaitAll(t1, t2, t3, t4);
                
                chart.Invoke((Action)(() =>
                {
                    chart.ChartAreas[0].AxisX.Maximum = LogEntries.Last().Time.ToOADate();
                    if (AutoScrollLive && buttonAutoScroll.BackColor == Color.Lime) chart.ChartAreas[0].AxisX.ScaleView.Scroll(LogEntries.Last().Time.ToOADate());
                    foreach (var s in series)
                    {
                        chart.Series[s.Key] = s.Value;
                    }
                    SetEnabledSeriesPrivate();
                    ChangeChartLabels();
                    SetYLabels();
                }));
                LastEntry = LogEntries.Count;
                LastEnergyAdd = DateTime.Now;
                PlottingLog = false;
            }
        }

        private void PlotBasic(Dictionary<string, Series> series, int start, int end)
        {
            var tripTimeSeries = series[DSAttConstants.TripTime];
            var lostPacketsSeries = series[DSAttConstants.LostPackets];
            var voltageSeries = series[DSAttConstants.Voltage];
            var canUtilSeries = series[DSAttConstants.CANUtil];
            var roborioCPUSeries = series[DSAttConstants.RoboRIOCPU];
            var totalPDPSeries = series[DSAttConstants.TotalPDP];
            int packetnum = 0;
            for (int i = start; i < end; i++)
            {
                DSLOGEntry en = LogEntries.ElementAt(i);
                //Adds points to first and last x values
                double entryTime = en.Time.ToOADate();



                if (i == 0 || i == end - 1)
                {
                    tripTimeSeries.Points.AddXY(entryTime, en.TripTime);
                    voltageSeries.Points.AddXY(entryTime, en.Voltage);
                    lostPacketsSeries.Points.AddXY(entryTime, en.LostPackets * 100);
                    roborioCPUSeries.Points.AddXY(entryTime, en.RoboRioCPU * 100);
                    canUtilSeries.Points.AddXY(entryTime, en.CANUtil * 100);
                    totalPDPSeries.Points.AddXY(entryTime, en.GetDPDTotal() / (TotalPDPScale / 10.0));
                }
                else
                {
                    var lastEn = LogEntries.ElementAt(i - 1);
                    var nextEn = LogEntries.ElementAt(i + 1);
                    //Checks if value is differnt around it so we don't plot everypoint
                    if (lastEn.TripTime != en.TripTime || nextEn.TripTime != en.TripTime)
                    {
                        tripTimeSeries.Points.AddXY(entryTime, en.TripTime);
                    }
                    if ((lastEn.LostPackets != en.LostPackets || nextEn.LostPackets != en.LostPackets) || lastEn.LostPackets != 0)
                    {
                        //the bar graphs are too much so we have to do this
                        if (packetnum % 4 == 0)
                        {
                            lostPacketsSeries.Points.AddXY(entryTime, 0);
                        }
                        else
                        {
                            lostPacketsSeries.Points.AddXY(entryTime, (en.LostPackets < 1) ? en.LostPackets * 100 : 100);

                        }
                        packetnum++;
                    }
                    if (lastEn.Voltage != en.Voltage || nextEn.Voltage != en.Voltage && en.Voltage < 17)
                    {
                        voltageSeries.Points.AddXY(entryTime, en.Voltage);
                    }
                    if (lastEn.RoboRioCPU != en.RoboRioCPU || nextEn.RoboRioCPU != en.RoboRioCPU)
                    {
                        roborioCPUSeries.Points.AddXY(entryTime, en.RoboRioCPU * 100);
                    }
                    if (lastEn.CANUtil != en.CANUtil || nextEn.CANUtil != en.CANUtil)
                    {
                        canUtilSeries.Points.AddXY(entryTime, en.CANUtil * 100);
                    }
                    if (lastEn.GetDPDTotal() != en.GetDPDTotal() || nextEn.GetDPDTotal() != en.GetDPDTotal())
                    {
                        totalPDPSeries.Points.AddXY(entryTime, en.GetDPDTotal() / (TotalPDPScale / 10.0));
                    }

                }
            }
        }

        private void PlotPDP(Dictionary<string, Series> series, int start, int end)
        {
            for (int index_l = start; index_l < end; index_l++)
            {
                DSLOGEntry en = LogEntries.ElementAt(index_l);
                //Adds points to first and last x values
                double entryTime = en.Time.ToOADate();

                if (index_l == 0 || index_l == end - 1)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        series[DSAttConstants.PDPPrefix + i].Points.AddXY(entryTime, en.GetPDPChannel(i));
                    }
                }
                else
                {
                    var lastEn = LogEntries.ElementAt(index_l - 1);
                    var nextEn = LogEntries.ElementAt(index_l + 1);
                    //Checks if value is differnt around it so we don't plot everypoint

                    for (int i = 0; i < 24; i++)
                    {
                        if (lastEn.GetPDPChannel(i) != en.GetPDPChannel(i) || nextEn.GetPDPChannel(i) != en.GetPDPChannel(i))
                        {
                            series[DSAttConstants.PDPPrefix + i].Points.AddXY(entryTime, en.GetPDPChannel(i));
                        }
                    }

                }

            }
        }

        private void PlotModes(Dictionary<string, Series> series, int start, int end)
        {
            var dsDisabledSeries = series[DSAttConstants.DSDisabled];
            var dsAutoSeries = series[DSAttConstants.DSAuto];
            var dsTeleSeries = series[DSAttConstants.DSTele];
            var robotDisabledSeries = series[DSAttConstants.RobotDisabled];
            var robotAutoSeries = series[DSAttConstants.RobotAuto];
            var robotTeleSeries = series[DSAttConstants.RobotTele];
            var brownoutSeries = series[DSAttConstants.Brownout];
            var watchdogSeries = series[DSAttConstants.Watchdog];
            for (int i = start; i < end; i++)
            {
                DSLOGEntry en = LogEntries.ElementAt(i);
                //Adds points to first and last x values
                double entryTime = en.Time.ToOADate();
                if (en.DSDisabled) dsDisabledSeries.Points.AddXY(entryTime, 15.9);
                if (en.DSAuto) dsAutoSeries.Points.AddXY(entryTime, 15.9);
                if (en.DSTele) dsTeleSeries.Points.AddXY(entryTime, 15.9);

                if (en.RobotDisabled) robotDisabledSeries.Points.AddXY(entryTime, 16.8);
                if (en.RobotAuto) robotAutoSeries.Points.AddXY(entryTime, 16.5);
                if (en.RobotTele) robotTeleSeries.Points.AddXY(entryTime, 16.2);

                if (en.Brownout) brownoutSeries.Points.AddXY(entryTime, 15.6);
                if (en.Watchdog) watchdogSeries.Points.AddXY(entryTime, 15.3);
            }
        }
        private void PlotTotalDelta(Dictionary<string, Series> series, int start, int end)
        {
            for (int index_l = start; index_l < end; index_l++)
            {
                DSLOGEntry en = LogEntries.ElementAt(index_l);
                //Adds points to first and last x values
                double entryTime = en.Time.ToOADate();
                if (index_l == 0 || index_l == end - 1)
                {
                    foreach (var kv in IdToPDPGroup)
                    {
                        if (kv.Key.StartsWith(DSAttConstants.TotalPrefix))
                        {
                            series[kv.Key].Points.AddXY(entryTime, en.GetGroupPDPTotal(kv.Value) / (TotalPDPScale / 10.0));
                        }
                        else
                        {
                            series[kv.Key].Points.AddXY(entryTime, en.GetGroupPDPSd(kv.Value) / (TotalPDPScale / 10.0));
                        }

                    }
                }
                else
                {
                    foreach (var kv in IdToPDPGroup)
                    {

                        var lastEn = LogEntries.ElementAt(index_l - 1);
                        var nextEn = LogEntries.ElementAt(index_l + 1);
                        if (kv.Key.StartsWith(DSAttConstants.TotalPrefix))
                        {
                            if (lastEn.GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value) || nextEn.GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value))
                            {
                                series[kv.Key].Points.AddXY(entryTime, en.GetGroupPDPTotal(kv.Value) / (TotalPDPScale / 10.0));
                            }

                        }
                        else
                        {
                            if (lastEn.GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value) || nextEn.GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value))
                            {
                                series[kv.Key].Points.AddXY(entryTime, en.GetGroupPDPSd(kv.Value));
                            }
                        }

                    }
                }
            }
        }


        private void SetEnabledSeriesPrivate()
        {
            foreach (TreeNode group in SeriesViewObserving.GetSeries())
            {
                foreach (TreeNode node in group.Nodes)
                {
                    chart.Series[node.Name].Enabled = node.Checked;
                }
            }
        }
        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            WaitForLoadingPlotting();
            SetEnabledSeriesPrivate();
            SetYLabels();
        }

        public void ClearMessages()
        {
            Util.ClearPointsQuick(chart.Series[DSAttConstants.Messages]);
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (PlottingLog || LoadingLog) return;
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value) return;
            prevPosition = pos;
            var results = chart.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        if (prop.YValues[0] == 15 || prop.YValues[0] == 14.7)
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (25 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 25 &&
                                Math.Abs(pos.Y - pointYPixel) < 25)
                            {
                                string data = "";
                                if (EventsView.TryGetEvent(prop.XValue, out data))
                                {
                                    MForm.SetGraphRichText(data, Util.MessageColor(data));
                                }
                                else
                                {
                                    MForm.SetGraphRichText("", SystemColors.Window);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (LogEntries != null)
            {
                if (chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum < chart.ChartAreas[0].AxisX.Minimum || chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum > chart.ChartAreas[0].AxisX.Maximum) chart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                ChangeChartLabels();
                
            }
        }

        private void SetEnergy()
        {
            if (LogEntries != null)
            {
                EnergyView.SetEnergy(LogEntries);
            }
            else
            {
                EnergyView.SetEnergy(null);
            }
        }

        private void Chart_AxisViewChanging(object sender, ViewEventArgs e)
        {
            if (!Double.IsNaN(e.NewPosition) && StartTime != null && !Double.IsNaN(e.NewSize))
            {
                if (((int)(DateTime.FromOADate(e.NewPosition + e.NewSize).Subtract(StartTime).TotalMilliseconds / 20)) > PointCount)
                {
                    e.NewPosition = EndTime.ToOADate() - e.NewSize;
                }
            }
        }

        private void Chart_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            if (LoadingLog || PlottingLog) return;
            if (LogEntries != null)
            {
                SetCursorPosition(e.NewPosition);
            }
        }

        private void SetCursorLineRed()
        {
            chart.ChartAreas[0].CursorX.LineColor = Color.Red;
            GraphCorsorLine.Stop();
            GraphCorsorLine.Start();
        }

        private void GraphCorsorLine_Tick(object sender, EventArgs e)
        {
            if (chart.ChartAreas[0].CursorX.LineColor == Color.Red)
            {
                chart.ChartAreas[0].CursorX.LineColor = Color.Transparent;
                GraphCorsorLine.Interval = 500;

            }
            else
            {
                chart.ChartAreas[0].CursorX.LineColor = Color.Red;
                GraphCorsorLine.Interval = 500;
            }
        }
        
        public void AddMessage(DataPoint dp)
        {
            chart.Series[DSAttConstants.Messages].Points.Add(dp);
        }

        private void ZoomIntoMatch()
        {
            if (LogEntries != null)
            {
                var index = GetAutoIndex();
                if (index != -1)
                {
                    chart.ChartAreas[0].AxisX.ScaleView.Zoom(LogEntries[index].Time.AddSeconds(-2).ToOADate(), 156000, DateTimeIntervalType.Milliseconds);
                    ChangeChartLabels();
                }
            }
        }

        private void ResetZoom()
        {
            chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(100);
        }

        private void timerStream_Tick(object sender, EventArgs e)
        {
            if (LogStreamer == null || (LogInfo != null && !LogInfo.Live))
            {
                StopStreaming();
                return;
            }

            if (LogStreamer.QueueCount() >= 3)
            {
                WaitForLoadingPlotting();
                PlottingLog = true;
                List<DSLOGEntry> newEntries = new List<DSLOGEntry>();
                while (LogStreamer.QueueCount() != 0)
                {
                    DSLOGEntry entry = LogStreamer.PopEntry();
                    if (entry != null)
                    {
                        LogEntries.Add(entry);
                        newEntries.Add(entry);
                    }
                }
                StreamPlot();
                DateTime now = DateTime.Now;
                if ((now - LastEnergyAdd).Seconds > 2)
                {
                    EnergyView.AddEnergy(newEntries);
                    LastEnergyAdd = now;
                }
                PlottingLog = false;
            }
        }

        private void StreamPlot()
        {
            
            Dictionary<string, Series> series = new Dictionary<string, Series>();
            foreach (var s in chart.Series)
            {
                series.Add(s.Name, s);
            }
            PlotBasic(series, LastEntry, LogEntries.Count);
            PlotModes(series, LastEntry, LogEntries.Count);
            PlotPDP(series, LastEntry, LogEntries.Count);
            PlotTotalDelta(series, LastEntry, LogEntries.Count);
            LastEntry = LogEntries.Count;
            
            chart.ChartAreas[0].AxisX.Maximum = LogEntries.Last().Time.ToOADate();
            EndTime = LogEntries.Last().Time;
            if (AutoScrollLive) chart.ChartAreas[0].AxisX.ScaleView.Scroll(LogEntries.Last().Time.ToOADate());
            ChangeChartLabels();
            

        }

        public void StopStreaming()
        {
            labelFileInfo.BackColor = SystemColors.Control;
            BackColor = SystemColors.Control;
            timerStream.Stop();
            SetAutoScrollColor();
            if (LogStreamer != null) LogStreamer.StopStreaming();
            LogStreamer = null;
        }

        private void buttonAutoScroll_Click(object sender, EventArgs e)
        {
            AutoScrollLive = !AutoScrollLive;
            SetAutoScrollColor();
        }

        private void SetAutoScrollColor()
        {
            if(timerStream.Enabled)
            {
                if (AutoScrollLive)
                {
                    buttonAutoScroll.BackColor = Color.Lime;
                }
                else
                {
                    buttonAutoScroll.BackColor = Color.Red;
                }
            }
            else
            {
                buttonAutoScroll.BackColor = Color.Gray;
            }
            
        }

        private int GetAutoIndex()
        {
            if (LogEntries != null)
            {
                for (int w = 0; w < LogEntries.Count; w++)
                {
                    if (LogEntries[w].DSAuto)
                    {
                        return w;
                    }
                }
            }
            return -1;
        }

        private void buttonFindMatch_Click(object sender, EventArgs e)
        {
            WaitForLoadingPlotting();
            ZoomIntoMatch();
            ChangeChartLabels();
        }

        private void buttonResetZoom_Click(object sender, EventArgs e)
        {
            WaitForLoadingPlotting();
            ResetZoom();
            ChangeChartLabels();
        }

        public List<DSLOGEntry> GetInViewEntries()
        {
            if (LogEntries == null) return null;
            return LogEntries.Where(en => en.Time >= DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum) && en.Time <= DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum)).ToList();
        }

        public void SaveChartImage(string file)
        {
            WaitForLoadingPlotting();
            chart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart.SaveImage(file, ChartImageFormat.Png);
            chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
        }

        private void buttonMatchTime_Click(object sender, EventArgs e)
        {
            WaitForLoadingPlotting();
            ChangeUseMatchTime(!UseMatchTime,true);
        }

        private void ChangeUseMatchTime(bool use, bool force = false)
        {
            if (use != UseMatchTime)
            {
                UseMatchTime = use;
                EventsView.AddEvents();
            }

            UseMatchTime = use;
            
            if (!CanUseMatchTime)
            {
                UseMatchTime = false;
                buttonMatchTime.BackColor = SystemColors.ControlDark;
                chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            }
            else if (UseMatchTime)
            {
                buttonMatchTime.BackColor = Color.Lime;
                ChangeChartLabels(force);
            }
            else
            {
                buttonMatchTime.BackColor = Color.Red;
                chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            }
        }

        private void chart_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            if (LogEntries != null)
            {
                ChangeChartLabels();
            }
        }

        public void SetCursorPosition(double d)
        {
           //TODO move view to see position when not in view

                chart.ChartAreas[0].CursorX.SetCursorPosition(d);
                SetCursorLineRed();
               
                ProbeView.SetProbe(GetEntryAt(d));
                MoveViewToCursor();


        }

        private void MoveViewToCursor()
        {
            var area = chart.ChartAreas[0];
            double pos = chart.ChartAreas[0].CursorX.Position;
            if (LogEntries != null && !Double.IsNaN(chart.ChartAreas[0].CursorX.Position) && pos >= area.AxisX.Minimum && pos <= area.AxisX.Maximum)
            {
                if (pos <= area.AxisX.ScaleView.ViewMinimum || pos >= area.AxisX.ScaleView.ViewMaximum)
                {
                    double size = area.AxisX.ScaleView.Size;
                    if (Double.IsNaN(size))
                    {
                        return;
                    }
                    if (size/2+pos > area.AxisX.Maximum)
                    {
                        area.AxisX.ScaleView.Zoom(area.AxisX.Maximum - size, area.AxisX.Maximum - .000001);
                    }
                    else if ( pos - size / 2 < area.AxisX.Minimum)
                    {
                        area.AxisX.ScaleView.Zoom(area.AxisX.Minimum + .000001, area.AxisX.Minimum+size);
                    }
                    else
                    {
                        area.AxisX.ScaleView.Zoom(pos - size / 2, pos + size / 2);
                    }
                }
            }
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            DiagnosticDialog diagnosticDialog = new DiagnosticDialog();
            diagnosticDialog.Show();
        }

        public DSLOGEntry GetEntryAt(double d)
        {
            if (LogEntries != null && !Double.IsNaN(d) && d >= StartTime.ToOADate() && d <= EndTime.ToOADate())
            {
                DateTime xValue = DateTime.FromOADate(d);
                return LogEntries.ElementAt((int)(xValue.Subtract(StartTime).TotalMilliseconds / 20));
            }
            return null;
        }
    
    }

    
}
