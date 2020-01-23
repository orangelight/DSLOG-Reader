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
    public partial class MainGraphView : UserControl
    {

        private Dictionary<string, Series> SeriesSettings;
        private DateTime StartTime;
        private DateTime EndTime;
        private string LastPath, LastFile;
        private const double TotalPDPScale = 50;
        private Dictionary<string, int[]> IdToPDPGroup;
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
                        if (node.Name == "voltage")
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
                        if (node.Name == "messages")
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
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith("pdp")).Select(n => int.Parse(n.Name.Replace("pdp", ""))).ToArray();
                foreach (var node in group.Childern)
                {
                    
                    if (node.Name.Contains("total") || node.Name.Contains("delta"))
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

            if (LastFile != null) LoadLog(LastFile, LastPath);
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

        public void LoadLog(string name, string dir)
        {
            LastPath = dir;
            LastFile = name;
            string dslogFile = $"{dir}\\{name}.dslog";
            InitChart();
            if (File.Exists(dslogFile))
            {
                DSLOGReader reader = new DSLOGReader(dslogFile);
                reader.Read();
                if (reader.Version != 3)
                {
                    return;
                }

                ChartArea area = chart.ChartAreas[0];
                StartTime = reader.StartTime;
                EndTime = reader.Entries.Last().Time;
                area.AxisX.Minimum = StartTime.ToOADate();
                area.CursorX.IntervalOffset = reader.StartTime.Millisecond % 20;
                PlotLog(reader);
                area.AxisX.Maximum = EndTime.ToOADate();
                ChangeChartLabels();
                area.AxisX.ScaleView.ZoomReset();
            }
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
            //if (log != null) menuStrip1.Items[menuStrip1.Items.Count - 1].Text = "Time Scale " + getTotalSecoundsInView() + " Sec";
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

        private void PlotLog(DSLOGReader reader)
        {
            if (reader != null && reader.Version == 3)
            {
                int packetnum = 0;
                for (int w = 0; w < reader.Entries.Count; w++)
                {
                    DSLOGEntry en = reader.Entries.ElementAt(w);


                    //Adds points to first and last x values
                    if (w == 0 || w == reader.Entries.Count - 1)
                    {
                        chart.Series["tripTime"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        chart.Series["voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        chart.Series["lostPackets"].Points.AddXY(en.Time.ToOADate(), en.LostPackets * 100);
                        chart.Series["roboRIOCPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        chart.Series["can"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        for (int i = 0; i < 16; i++)
                        {
                            chart.Series["pdp" + i].Points.AddXY(en.Time.ToOADate(), en.GetPDPChannel(i));
                        }
                        if (en.DSDisabled) chart.Series["dsDisabled"].Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSAuto) chart.Series["dsAuto"].Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSTele) chart.Series["dsTele"].Points.AddXY(en.Time.ToOADate(), 15.9);

                        if (en.RobotDisabled) chart.Series["robotDisabled"].Points.AddXY(en.Time.ToOADate(), 16.8);
                        if (en.RobotAuto) chart.Series["robotAuto"].Points.AddXY(en.Time.ToOADate(), 16.5);
                        if (en.RobotTele) chart.Series["robotTele"].Points.AddXY(en.Time.ToOADate(), 16.2);

                        if (en.Brownout) chart.Series["brownout"].Points.AddXY(en.Time.ToOADate(), 15.6);
                        if (en.Watchdog) chart.Series["watchdog"].Points.AddXY(en.Time.ToOADate(), 15.3);
                        chart.Series["totalPdp"].Points.AddXY(en.Time.ToOADate(), en.GetDPDTotal() / (TotalPDPScale/10.0));

                        foreach(var kv in IdToPDPGroup)
                        {
                            if(kv.Key.StartsWith("total"))
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
                        if (reader.Entries.ElementAt(w - 1).TripTime != en.TripTime || reader.Entries.ElementAt(w + 1).TripTime != en.TripTime)
                        {
                            chart.Series["tripTime"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        }
                        if ((reader.Entries.ElementAt(w - 1).LostPackets != en.LostPackets || reader.Entries.ElementAt(w + 1).LostPackets != en.LostPackets) || reader.Entries.ElementAt(w - 1).LostPackets != 0)
                        {
                            //the bar graphs are too much so we have to do this
                            if (packetnum % 4 == 0)
                            {
                                chart.Series["lostPackets"].Points.AddXY(en.Time.ToOADate(), 0);
                            }
                            else
                            {
                                chart.Series["lostPackets"].Points.AddXY(en.Time.ToOADate(), (en.LostPackets < 1) ? en.LostPackets * 100 : 100);

                            }
                            packetnum++;
                        }

                        
                        if (reader.Entries.ElementAt(w - 1).Voltage != en.Voltage || reader.Entries.ElementAt(w + 1).Voltage != en.Voltage && en.Voltage < 17)
                        {
                            chart.Series["voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        }
                            
                        
                        


                        if (reader.Entries.ElementAt(w - 1).RoboRioCPU != en.RoboRioCPU || reader.Entries.ElementAt(w + 1).RoboRioCPU != en.RoboRioCPU)
                        {
                            chart.Series["roboRIOCPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        }
                        if (reader.Entries.ElementAt(w - 1).CANUtil != en.CANUtil || reader.Entries.ElementAt(w + 1).CANUtil != en.CANUtil)
                        {
                            chart.Series["can"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            if (reader.Entries.ElementAt(w - 1).GetPDPChannel(i) != en.GetPDPChannel(i) || reader.Entries.ElementAt(w + 1).GetPDPChannel(i) != en.GetPDPChannel(i))
                            {
                                chart.Series["pdp" + i].Points.AddXY(en.Time.ToOADate(), en.GetPDPChannel(i));
                            }
                        }

                        if (reader.Entries.ElementAt(w - 1).GetDPDTotal() != en.GetDPDTotal() || reader.Entries.ElementAt(w + 1).GetDPDTotal() != en.GetDPDTotal())
                        {
                            chart.Series["totalPdp"].Points.AddXY(en.Time.ToOADate(), en.GetDPDTotal()/ (TotalPDPScale / 10.0));
                        }

                        foreach (var kv in IdToPDPGroup)
                        {
                            if (kv.Key.StartsWith("total"))
                            {
                                if (reader.Entries.ElementAt(w - 1).GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value) || reader.Entries.ElementAt(w + 1).GetGroupPDPTotal(kv.Value) != en.GetGroupPDPTotal(kv.Value))
                                {
                                    chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPTotal(kv.Value) / (TotalPDPScale / 10.0));
                                }
                                
                            }
                            else
                            {
                                if (reader.Entries.ElementAt(w - 1).GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value) || reader.Entries.ElementAt(w + 1).GetGroupPDPSd(kv.Value) != en.GetGroupPDPSd(kv.Value))
                                {
                                    chart.Series[kv.Key].Points.AddXY(en.Time.ToOADate(), en.GetGroupPDPSd(kv.Value) / (TotalPDPScale / 10.0));
                                }
                            }

                        }


                        if (reader.Entries.ElementAt(w - 1).DSDisabled != en.DSDisabled || reader.Entries.ElementAt(w + 1).DSDisabled != en.DSDisabled)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSDisabled || !reader.Entries.ElementAt(w - 1).DSDisabled) po.Color = Color.Transparent;
                                chart.Series["dsDisabled"].Points.Add(po);
                                //chart.Series["dsDisabled"].Points.AddXY(en.Time.ToOADate(), 15.9);
                            }
                            if (reader.Entries.ElementAt(w - 1).DSAuto != en.DSAuto || reader.Entries.ElementAt(w + 1).DSAuto != en.DSAuto)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSAuto || reader.Entries.ElementAt(w - 1).DSAuto == false) po.Color = Color.Transparent;
                                chart.Series["dsAuto"].Points.Add(po);
                            }
                            if (reader.Entries.ElementAt(w - 1).DSTele != en.DSTele || reader.Entries.ElementAt(w + 1).DSTele != en.DSTele)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.9);
                                if (!en.DSTele || reader.Entries.ElementAt(w - 1).DSTele == false) po.Color = Color.Transparent;
                                chart.Series["dsTele"].Points.Add(po);
                            }

                            if (reader.Entries.ElementAt(w - 1).RobotDisabled != en.RobotDisabled || reader.Entries.ElementAt(w + 1).RobotDisabled != en.RobotDisabled)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.8);
                                if (!en.RobotDisabled || !reader.Entries.ElementAt(w - 1).RobotDisabled) po.Color = Color.Transparent;
                                chart.Series["robotDisabled"].Points.Add(po);
                                //chart.Series["robotDisabled"].Points.AddXY(en.Time.ToOADate(), 15.9);
                            }
                            if (reader.Entries.ElementAt(w - 1).RobotAuto != en.RobotAuto || reader.Entries.ElementAt(w + 1).RobotAuto != en.RobotAuto)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.5);
                                if (!en.RobotAuto || reader.Entries.ElementAt(w - 1).RobotAuto == false) po.Color = Color.Transparent;
                                chart.Series["robotAuto"].Points.Add(po);
                            }
                            if (reader.Entries.ElementAt(w - 1).RobotTele != en.RobotTele || reader.Entries.ElementAt(w + 1).RobotTele != en.RobotTele)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 16.2);
                                if (!en.RobotTele || reader.Entries.ElementAt(w - 1).RobotTele == false) po.Color = Color.Transparent;
                                chart.Series["robotTele"].Points.Add(po);
                            }

                            if (reader.Entries.ElementAt(w - 1).Brownout != en.Brownout || reader.Entries.ElementAt(w + 1).Brownout != en.Brownout)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.6);
                                if (!en.Brownout || reader.Entries.ElementAt(w - 1).Brownout == false) po.Color = Color.Transparent;
                                chart.Series["brownout"].Points.Add(po);
                            }
                            if (reader.Entries.ElementAt(w - 1).Watchdog != en.Watchdog || reader.Entries.ElementAt(w + 1).Watchdog != en.Watchdog)
                            {
                                DataPoint po = new DataPoint(en.Time.ToOADate(), 15.3);
                                if (!en.Watchdog || reader.Entries.ElementAt(w - 1).Watchdog == false) po.Color = Color.Transparent;
                                chart.Series["watchdog"].Points.Add(po);
                            }
                        

                    }
                }
            }
        }

        public void SetSeriesEnabled(TreeNodeCollection groups)
        {
            foreach(TreeNode group in groups)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    chart.Series[node.Name].Enabled = node.Checked;
                }
            }
        }

    }

    
}
