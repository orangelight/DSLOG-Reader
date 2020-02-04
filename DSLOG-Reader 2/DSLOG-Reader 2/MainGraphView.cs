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
using System.IO;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public partial class MainGraphView : UserControl, SeriesViewObserver
    {

        private Dictionary<string, Series> SeriesSettings;
        private DateTime StartTime;
        private DateTime EndTime;
        private string LastPath, LastFile;
        private const double TotalPDPScale = 50;
        private Dictionary<string, int[]> IdToPDPGroup;
        private Point? prevPosition = null;
        public MainForm MForm {get;set; }
        public EventsView EventsView { get; set; }
        public ProbeView ProbeView { get; set; }
        private int PointCount = 1;
        private List<DSLOGEntry> LogEntries;
        private DSLOGFileEntry LogInfo;
        private DSLOGStreamer LogStreamer;
        private int LastEntry = 0;
        private bool AutoScrollLive = false;
        public MainGraphView()
        {
            InitializeComponent();
            InitSeriesSettings();
            for (double i = 0; i < 12; i++)
            {
                chart.ChartAreas[0].AxisY2.CustomLabels.Add(i*10, i*10+.001, $"{ i*10} ({i * TotalPDPScale})");
            }
            chart.ChartAreas[0].AxisY2.CustomLabels.Add(120, 120 - .001, $"{ 120} ({12 * TotalPDPScale})");
        }

        public void InitSeriesSettings()
        {
            SeriesSettings = new Dictionary<string, Series>();
            Series modes = new Series();
            modes.XValueType = ChartValueType.DateTime;
            modes.YAxisType = AxisType.Primary;
            modes.ChartType = SeriesChartType.Line;
            modes.BorderWidth = 5;
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

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            IdToPDPGroup = new Dictionary<string, int[]>();
            ClearGraph();
            chart.Series.Clear();
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
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith(DSAttConstants.PDPPrefix)).Select(n => int.Parse(n.Name.Replace(DSAttConstants.PDPPrefix, ""))).ToArray();
                foreach (var node in group.Childern)
                {
                    
                    if (node.Name.StartsWith(DSAttConstants.TotalPrefix)|| node.Name.Contains(DSAttConstants.DeltaPrefix))
                    {
                       
                        var newSeries = MakeSeriesFromSettings(SeriesSettings["totallines"], node);
                        IdToPDPGroup[node.Name] = pdpIds;
                        newSeries.BorderDashStyle = ChartDashStyle.Dash;
                        chart.Series.Add(newSeries);
                    }
                    else
                    {
                        var newSeries = MakeSeriesFromSettings(SeriesSettings["lines"], node);
                        chart.Series.Add(newSeries);
                    }
                    
                }
            }

            if (LogInfo != null) LoadLog(LogInfo, LastPath);
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

        public void LoadLog(DSLOGFileEntry logInfo, string dir)
        {
            StopStreaming();
            LogStreamer = null;
            LastPath = dir;
            LastFile = logInfo.Name;
            LogInfo = logInfo;
            string dslogFile = $"{dir}\\{logInfo.Name}.dslog";
            InitChart();
            LogEntries = null;
            ClearInfoLabel();
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
                    reader.Read();
                }
                
                
                if (reader.Version != 3)
                {
                    return;
                }

                if (logInfo.Live) LogStreamer.Stream();
                
                ChartArea area = chart.ChartAreas[0];
                StartTime = reader.StartTime;
                EndTime = reader.Entries.Last().Time;
                LogEntries = reader.Entries;
                area.AxisX.Minimum = StartTime.ToOADate();
                area.AxisX.Maximum = EndTime.ToOADate();
                area.CursorX.IntervalOffset = reader.StartTime.Millisecond % 20;
                PlotLog();
                PointCount = LogEntries.Count;
                
                
                area.AxisX.ScaleView.ZoomReset();
                labelFileInfo.Text = $"{logInfo.Name}.dslog";

                if (logInfo.IsFMSMatch)
                {
                    labelFileInfo.Text = labelFileInfo.Text + $" ({logInfo.EventName} {logInfo.MatchType.ToString()} {logInfo.FMSMatchNum})";
                    labelFileInfo.BackColor = logInfo.GetMatchTypeColor();
                }
                if (logInfo.Live)
                {
                    labelFileInfo.BackColor = Color.Lime;
                    timerStream.Start();
                }
            }
        }

        private void ClearInfoLabel()
        {
            labelFileInfo.Text = "";
            labelFileInfo.BackColor = SystemColors.Control;
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

        private void ChangeChartLabels()
        {
            if (GetTotalSecoundsInView() < 5)
            {
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss.fff";
            }
            else
            {
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";

            }
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
            if (LogEntries != null)
            {
                var tripTimeSeries = chart.Series[DSAttConstants.TripTime];
                var lostPacketsSeries = chart.Series[DSAttConstants.LostPackets];
                var voltageSeries = chart.Series[DSAttConstants.Voltage];
                var canUtilSeries = chart.Series[DSAttConstants.CANUtil];
                var roborioCPUSeries = chart.Series[DSAttConstants.RoboRIOCPU];
                var dsDisabledSeries = chart.Series[DSAttConstants.DSDisabled];
                var dsAutoSeries = chart.Series[DSAttConstants.DSAuto];
                var dsTeleSeries = chart.Series[DSAttConstants.DSTele];
                var robotDisabledSeries = chart.Series[DSAttConstants.RobotDisabled];
                var robotAutoSeries = chart.Series[DSAttConstants.RobotAuto];
                var robotTeleSeries = chart.Series[DSAttConstants.RobotTele];
                var brownoutSeries = chart.Series[DSAttConstants.Brownout];
                var watchdogSeries = chart.Series[DSAttConstants.Watchdog];
                var totalPDPSeries = chart.Series[DSAttConstants.TotalPDP];

                int packetnum = 0;
                for (; LastEntry < LogEntries.Count; LastEntry++)
                {
                    DSLOGEntry en = LogEntries.ElementAt(LastEntry);
                    //Adds points to first and last x values
                    if (LastEntry == 0 || LastEntry == LogEntries.Count - 1)
                    {
                        if (LastEntry == LogEntries.Count - 1)
                        {
                            chart.ChartAreas[0].AxisX.Maximum = en.Time.ToOADate();
                            if (AutoScrollLive) chart.ChartAreas[0].AxisX.ScaleView.Scroll(en.Time.ToOADate());
                        }
                        tripTimeSeries.Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        voltageSeries.Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        lostPacketsSeries.Points.AddXY(en.Time.ToOADate(), en.LostPackets * 100);
                        roborioCPUSeries.Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        canUtilSeries.Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        for (int i = 0; i < 16; i++)
                        {
                            chart.Series[DSAttConstants.PDPPrefix + i].Points.AddXY(en.Time.ToOADate(), en.GetPDPChannel(i));
                        }
                        if (en.DSDisabled) dsDisabledSeries.Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSAuto) dsAutoSeries.Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSTele) dsTeleSeries.Points.AddXY(en.Time.ToOADate(), 15.9);

                        if (en.RobotDisabled) robotDisabledSeries.Points.AddXY(en.Time.ToOADate(), 16.8);
                        if (en.RobotAuto) robotAutoSeries.Points.AddXY(en.Time.ToOADate(), 16.5);
                        if (en.RobotTele) robotTeleSeries.Points.AddXY(en.Time.ToOADate(), 16.2);

                        if (en.Brownout) brownoutSeries.Points.AddXY(en.Time.ToOADate(), 15.6);
                        if (en.Watchdog) watchdogSeries.Points.AddXY(en.Time.ToOADate(), 15.3);
                        totalPDPSeries.Points.AddXY(en.Time.ToOADate(), en.GetDPDTotal() / (TotalPDPScale/10.0));

                        foreach(var kv in IdToPDPGroup)
                        {
                            if(kv.Key.StartsWith(DSAttConstants.TotalPrefix))
                            {
                                chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPTotal(kv.Value) / (TotalPDPScale / 10.0));
                            }
                            else
                            {
                                chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPSd(kv.Value)/ (TotalPDPScale / 10.0));
                            }
                            
                        }
                    }
                    else
                    {
                        //Checks if value is differnt around it so we don't plot everypoint
                        if (LogEntries.ElementAt(LastEntry - 1).TripTime != en.TripTime || LogEntries.ElementAt(LastEntry + 1).TripTime != en.TripTime)
                        {
                            tripTimeSeries.Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        }
                        if ((LogEntries.ElementAt(LastEntry - 1).LostPackets != en.LostPackets || LogEntries.ElementAt(LastEntry + 1).LostPackets != en.LostPackets) || LogEntries.ElementAt(LastEntry - 1).LostPackets != 0)
                        {
                            //the bar graphs are too much so we have to do this
                            if (packetnum % 4 == 0)
                            {
                                lostPacketsSeries.Points.AddXY(en.Time.ToOADate(), 0);
                            }
                            else
                            {
                                lostPacketsSeries.Points.AddXY(en.Time.ToOADate(), (en.LostPackets < 1) ? en.LostPackets * 100 : 100);

                            }
                            packetnum++;
                        }

                        
                        if (LogEntries.ElementAt(LastEntry - 1).Voltage != en.Voltage || LogEntries.ElementAt(LastEntry + 1).Voltage != en.Voltage && en.Voltage < 17)
                        {
                            voltageSeries.Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        }
                            
                        
                        


                        if (LogEntries.ElementAt(LastEntry - 1).RoboRioCPU != en.RoboRioCPU || LogEntries.ElementAt(LastEntry + 1).RoboRioCPU != en.RoboRioCPU)
                        {
                            roborioCPUSeries.Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        }
                        if (LogEntries.ElementAt(LastEntry - 1).CANUtil != en.CANUtil || LogEntries.ElementAt(LastEntry + 1).CANUtil != en.CANUtil)
                        {
                            canUtilSeries.Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            if (LogEntries.ElementAt(LastEntry - 1).GetPDPChannel(i) != en.GetPDPChannel(i) || LogEntries.ElementAt(LastEntry + 1).GetPDPChannel(i) != en.GetPDPChannel(i))
                            {
                                chart.Series[DSAttConstants.PDPPrefix + i].Points.AddXY(en.Time.ToOADate(), en.GetPDPChannel(i));
                            }
                        }

                        if (LogEntries.ElementAt(LastEntry - 1).GetDPDTotal() != en.GetDPDTotal() || LogEntries.ElementAt(LastEntry + 1).GetDPDTotal() != en.GetDPDTotal())
                        {
                            totalPDPSeries.Points.AddXY(en.Time.ToOADate(), en.GetDPDTotal()/ (TotalPDPScale / 10.0));
                        }

                        foreach (var kv in IdToPDPGroup)
                        {
                            if (kv.Key.StartsWith(DSAttConstants.TotalPrefix))
                            {
                                if (LogEntries.ElementAt(LastEntry - 1).GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value) || LogEntries.ElementAt(LastEntry + 1).GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value))
                                {
                                    chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPTotal(kv.Value) / (TotalPDPScale / 10.0));
                                }
                                
                            }
                            else
                            {
                                if (LogEntries.ElementAt(LastEntry - 1).GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value) || LogEntries.ElementAt(LastEntry + 1).GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value))
                                {
                                    chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPSd(kv.Value) / (TotalPDPScale / 10.0));
                                }
                            }

                        }


                        if (LogEntries.ElementAt(LastEntry - 1).DSDisabled != en.DSDisabled || LogEntries.ElementAt(LastEntry + 1).DSDisabled != en.DSDisabled)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSDisabled || !LogEntries.ElementAt(LastEntry - 1).DSDisabled) po.Color = Color.Transparent;
                                dsDisabledSeries.Points.Add(po);
                            }
                            if (LogEntries.ElementAt(LastEntry - 1).DSAuto != en.DSAuto || LogEntries.ElementAt(LastEntry + 1).DSAuto != en.DSAuto)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSAuto || LogEntries.ElementAt(LastEntry - 1).DSAuto == false) po.Color = Color.Transparent;
                                dsAutoSeries.Points.Add(po);
                            }
                            if (LogEntries.ElementAt(LastEntry - 1).DSTele != en.DSTele || LogEntries.ElementAt(LastEntry + 1).DSTele != en.DSTele)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSTele || LogEntries.ElementAt(LastEntry - 1).DSTele == false) po.Color = Color.Transparent;
                                dsTeleSeries.Points.Add(po);
                            }

                            if (LogEntries.ElementAt(LastEntry - 1).RobotDisabled != en.RobotDisabled || LogEntries.ElementAt(LastEntry + 1).RobotDisabled != en.RobotDisabled)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.8);
                                if (!en.RobotDisabled || !LogEntries.ElementAt(LastEntry - 1).RobotDisabled) po.Color = Color.Transparent;
                                robotDisabledSeries.Points.Add(po);
                            }
                            if (LogEntries.ElementAt(LastEntry - 1).RobotAuto != en.RobotAuto || LogEntries.ElementAt(LastEntry + 1).RobotAuto != en.RobotAuto)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.5);
                                if (!en.RobotAuto || LogEntries.ElementAt(LastEntry - 1).RobotAuto == false) po.Color = Color.Transparent;
                                robotAutoSeries.Points.Add(po);
                            }
                            if (LogEntries.ElementAt(LastEntry - 1).RobotTele != en.RobotTele || LogEntries.ElementAt(LastEntry + 1).RobotTele != en.RobotTele)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.2);
                                if (!en.RobotTele || LogEntries.ElementAt(LastEntry - 1).RobotTele == false) po.Color = Color.Transparent;
                                robotTeleSeries.Points.Add(po);
                            }

                            if (LogEntries.ElementAt(LastEntry - 1).Brownout != en.Brownout || LogEntries.ElementAt(LastEntry + 1).Brownout != en.Brownout)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.6);
                                if (!en.Brownout || LogEntries.ElementAt(LastEntry - 1).Brownout == false) po.Color = Color.Transparent;
                                brownoutSeries.Points.Add(po);
                            }
                            if (LogEntries.ElementAt(LastEntry - 1).Watchdog != en.Watchdog || LogEntries.ElementAt(LastEntry + 1).Watchdog != en.Watchdog)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.3);
                                if (!en.Watchdog || LogEntries.ElementAt(LastEntry - 1).Watchdog == false) po.Color = Color.Transparent;
                                watchdogSeries.Points.Add(po);
                            }
                        

                    }
                }
                ChangeChartLabels();
                
            }
        }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            foreach(TreeNode group in groups)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    chart.Series[node.Name].Enabled = node.Checked;
                }
            }
        }

        public void ClearMessages()
        {
            Util.ClearPointsQuick(chart.Series[DSAttConstants.Messages]);
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
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
            if (LogEntries != null)
            {
                if (!Double.IsNaN(e.NewPosition))
                {
                    SetCursorLineRed();
                    DateTime xValue = DateTime.FromOADate(e.NewPosition);
                    ProbeView.SetProbe(LogEntries.ElementAt((int)(xValue.Subtract(StartTime).TotalMilliseconds / 20)), chart.Series, IdToPDPGroup);
                }
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
                while (LogStreamer.QueueCount() != 0)
                {
                    DSLOGEntry entry = LogStreamer.PopEntry();
                    if (entry != null) LogEntries.Add(entry);
                }
                PlotLog();
            }
        }

        public void StopStreaming()
        {
            labelFileInfo.BackColor = SystemColors.Control;
            timerStream.Stop();
            if (LogStreamer != null) LogStreamer.StopStreaming();
            LogStreamer = null;
        }

        private void buttonAutoScroll_Click(object sender, EventArgs e)
        {
            AutoScrollLive = !AutoScrollLive;
            if (AutoScrollLive) 
            {
                buttonAutoScroll.BackColor = Color.Lime;
            }
            else
            {
                buttonAutoScroll.BackColor = Color.Red;
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
            ZoomIntoMatch();
        }

        private void buttonResetZoom_Click(object sender, EventArgs e)
        {
            ResetZoom();
        }

        public List<DSLOGEntry> GetInViewEntries()
        {
            if (LogEntries == null) return null;
            return LogEntries.Where(en => en.Time >= DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum) && en.Time <= DateTime.FromOADate(chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum)).ToList();
        }

    
    }

    
}
