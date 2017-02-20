using DSLOG_Reader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dslog
{
    public partial class FormMain : Form
    {

        public static string VERSION;
        
        public FormMain()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            VERSION = AssemblyName.GetAssemblyName(assembly.Location).Version.ToString();
            addSeries();
            this.DoubleBuffered = true;
            //Select csv in exportBox
            comboBoxExport.SelectedIndex = 0;
            addLogFilesToViewer(@"C:\Users\Public\Documents\FRC\Log Files");
            menuStrip1.Items.Add(new ToolStripLabel("Current File: "));
            menuStrip1.Items.Add(new ToolStripLabel("Time Scale"));
            menuStrip1.Items[menuStrip1.Items.Count - 1].Alignment = ToolStripItemAlignment.Right;
            
        }
        DSLOGReader log;
        
        Color[] pdpColors = { Color.FromArgb(255, 113, 113), Color.FromArgb(255, 198, 89), Color.FromArgb(152, 255, 136), Color.FromArgb(136, 154, 255), Color.FromArgb(255, 52, 42), Color.FromArgb(255, 176, 42), Color.FromArgb(0, 255, 9), Color.FromArgb(0, 147, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(239, 139,0), Color.FromArgb(46,220,0), Color.FromArgb(57,42,255),Color.FromArgb(180,8,0), Color.FromArgb(200,132, 0), Color.FromArgb(42,159,0), Color.FromArgb(0,47,239) };
        

        //Chart Series
        #region
        void addSeries()
        {
            chartMain.Series.Add(new Series("Trip Time"));
            chartMain.Series["Trip Time"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Trip Time"].YAxisType = AxisType.Secondary;
            chartMain.Series["Trip Time"].ChartType = SeriesChartType.FastLine;
            chartMain.Series["Trip Time"].Color = Color.Lime;

            chartMain.Series.Add(new Series("Lost Packets"));
            chartMain.Series["Lost Packets"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Lost Packets"].YAxisType = AxisType.Secondary;
            chartMain.Series["Lost Packets"].ChartType = SeriesChartType.FastLine;
            chartMain.Series["Lost Packets"].Color = Color.Chocolate;

            chartMain.Series.Add(new Series("Voltage"));
            chartMain.Series["Voltage"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Voltage"].YAxisType = AxisType.Primary;
            chartMain.Series["Voltage"].ChartType = SeriesChartType.FastLine;
            chartMain.Series["Voltage"].Color = Color.Yellow;

            chartMain.Series.Add(new Series("roboRIO CPU"));
            chartMain.Series["roboRIO CPU"].XValueType = ChartValueType.DateTime;
            chartMain.Series["roboRIO CPU"].YAxisType = AxisType.Secondary;
            chartMain.Series["roboRIO CPU"].ChartType = SeriesChartType.FastLine;
            chartMain.Series["roboRIO CPU"].Color = Color.Red;

            chartMain.Series.Add(new Series("CAN"));
            chartMain.Series["CAN"].XValueType = ChartValueType.DateTime;
            chartMain.Series["CAN"].YAxisType = AxisType.Secondary;
            chartMain.Series["CAN"].ChartType = SeriesChartType.FastLine;
            chartMain.Series["CAN"].Color = Color.Silver;

            chartMain.Series.Add(new Series("DS Disabled"));
            chartMain.Series["DS Disabled"].XValueType = ChartValueType.DateTime;
            chartMain.Series["DS Disabled"].YAxisType = AxisType.Primary;
            chartMain.Series["DS Disabled"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["DS Disabled"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["DS Disabled"].Color = Color.DarkGray;

            chartMain.Series.Add(new Series("DS Auto"));
            chartMain.Series["DS Auto"].XValueType = ChartValueType.DateTime;
            chartMain.Series["DS Auto"].YAxisType = AxisType.Primary;
            chartMain.Series["DS Auto"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["DS Auto"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["DS Auto"].Color = Color.Lime;

            chartMain.Series.Add(new Series("DS Tele"));
            chartMain.Series["DS Tele"].XValueType = ChartValueType.DateTime;
            chartMain.Series["DS Tele"].YAxisType = AxisType.Primary;
            chartMain.Series["DS Tele"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["DS Tele"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["DS Tele"].Color = Color.Cyan;

            chartMain.Series.Add(new Series("Robot Disabled"));
            chartMain.Series["Robot Disabled"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Robot Disabled"].YAxisType = AxisType.Primary;
            chartMain.Series["Robot Disabled"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["Robot Disabled"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Robot Disabled"].Color = Color.DarkGray;

            chartMain.Series.Add(new Series("Robot Auto"));
            chartMain.Series["Robot Auto"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Robot Auto"].YAxisType = AxisType.Primary;
            chartMain.Series["Robot Auto"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["Robot Auto"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Robot Auto"].Color = Color.Lime;

            chartMain.Series.Add(new Series("Robot Tele"));
            chartMain.Series["Robot Tele"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Robot Tele"].YAxisType = AxisType.Primary;
            chartMain.Series["Robot Tele"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["Robot Tele"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Robot Tele"].Color = Color.Cyan;

            chartMain.Series.Add(new Series("Brownout"));
            chartMain.Series["Brownout"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Brownout"].YAxisType = AxisType.Primary;
            chartMain.Series["Brownout"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["Brownout"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Brownout"].Color = Color.OrangeRed;

            chartMain.Series.Add(new Series("Watchdog"));
            chartMain.Series["Watchdog"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Watchdog"].YAxisType = AxisType.Primary;
            chartMain.Series["Watchdog"].ChartType = SeriesChartType.FastPoint;
            chartMain.Series["Watchdog"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Watchdog"].Color = Color.FromArgb(249, 0, 255);

            

            for (int i = 0; i < 16; i++)
            {
                chartMain.Series.Add(new Series("PDP " + i));
                chartMain.Series["PDP " + i].XValueType = ChartValueType.DateTime;
                chartMain.Series["PDP " + i].YAxisType = AxisType.Secondary;
                chartMain.Series["PDP " + i].ChartType = SeriesChartType.FastLine;
                chartMain.Series["PDP " + i].Color = pdpColors[i];
                chartMain.Series["PDP " + i].Enabled = false;
            }

            chartMain.Series.Add(new Series("Messages"));
            chartMain.Series["Messages"].XValueType = ChartValueType.DateTime;
            chartMain.Series["Messages"].YAxisType = AxisType.Primary;
            chartMain.Series["Messages"].ChartType = SeriesChartType.Point;
            chartMain.Series["Messages"].MarkerStyle = MarkerStyle.Circle;
            chartMain.Series["Messages"].Color = Color.Gainsboro;

            //Checkboxs
            addSeriesPlotCheckBoxs();
        }

        void addSeriesPlotCheckBoxs()
        {
            for (int x = 0; x < chartMain.Series.Count; x++)
            {
                CheckBox cb = new CheckBox();
                cb.BackColor = chartMain.Series[x].Color;
                cb.Text = chartMain.Series[x].Name;
                cb.Visible = true;
                cb.Location = new Point(7, (x * 24) + 7);
                cb.Checked = chartMain.Series[x].Enabled;

                cb.CheckedChanged += plotCheckBox_CheckedChanged;
                tabPage2.Controls.Add(cb);
            }
        }

        void plotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetSeriesEnabledForIndv();
            setColoumnLabelNumber();
        }

        void SetSeriesEnabledForIndv()
        {
            foreach (Control con in tabPage2.Controls)
            {
                chartMain.Series[con.Text].Enabled = ((CheckBox)con).Checked;
            }
        }
        #endregion

        //probe
        #region
        private void chartMain_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e)
        {
            if (log != null)
            {
                if (!Double.IsNaN(e.NewPosition))
                {
                    setCursorLineRed();
                    probe(e.NewPosition);
                    
                } 
            }

        }

        private void setCursorLineRed()
        {
            chartMain.ChartAreas[0].CursorX.LineColor = Color.Red;
            GraphCorsorLine.Stop();
            GraphCorsorLine.Start();
        }

        private void probe(double pos)
        {
            
            tabPage3.Controls.Clear();
            //Get x value form probe
            DateTime xValue = DateTime.FromOADate(pos);
            //get entry from x value
            Entry en = log.Entries.ElementAt((int)(xValue.Subtract(log.StartTime).TotalMilliseconds / 20));
            int labelNum = 0;
            double totalA = 0;
            
            Label timeLabel = new Label();
            timeLabel.Text = "Time: "+DateTime.FromOADate(chartMain.ChartAreas[0].CursorX.Position).ToString("HH:mm:ss.fff");
            timeLabel.Visible = true;
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(7, (24 * labelNum++) + 7);
            tabPage3.Controls.Add(timeLabel);

            for (int seriesNum = 0; seriesNum < chartMain.Series.Count; seriesNum++)
            {
                if (chartMain.Series[seriesNum].Enabled)
                {
                    Label seriesLabel = new Label();
                    seriesLabel.Text = varToLabel(chartMain.Series[seriesNum].Name, en);
                    seriesLabel.Visible = true;
                    seriesLabel.AutoSize = true;
                    seriesLabel.Location = new Point(7, (24 * labelNum++) + 7);
                    tabPage3.Controls.Add(seriesLabel);
                    //Add pdp series to total current
                    if (chartMain.Series[seriesNum].Name.StartsWith("PDP"))
                    {
                        int channel = Int32.Parse(chartMain.Series[seriesNum].Name.Replace("PDP ", ""));
                        if (channel <= 3 || channel >= 12)
                        {
                            if (en.getPDPChannel(int.Parse(chartMain.Series[seriesNum].Name.Split(' ')[1])) >= 40) seriesLabel.BackColor = Color.LightCoral;
                        }
                        else
                        {
                            if (en.getPDPChannel(int.Parse(chartMain.Series[seriesNum].Name.Split(' ')[1])) >= 30) seriesLabel.BackColor = Color.LightCoral;
                        }
                        totalA += en.getPDPChannel(int.Parse(chartMain.Series[seriesNum].Name.Split(' ')[1]));
                    }
                }
            }

            Label totalAL = new Label();
            totalAL.Text = "Total Current: " + totalA + "A";
            totalAL.Visible = true;
            totalAL.AutoSize = true;
            totalAL.Location = new Point(7, (24 * labelNum++) + 7);
            tabPage3.Controls.Add(totalAL);
        }

        string varToLabel(String name, Entry en)
        {
            if (name.Equals("Trip Time"))
            {
                return name + ": " + en.TripTime+"ms";
            }
            else if (name.Equals("Lost Packets"))
            {
                return name + ": " + en.LostPackets * 100 + "%";
            }
            else if (name.Equals("Voltage"))
            {
                return name + ": " + en.Voltage+"V";
            }
            else if (name.Equals("roboRIO CPU"))
            {
                return name + ": " + en.RoboRioCPU * 100 + "%";
            }
            else if (name.Equals("CAN"))
            {
                return name + ": " + en.CANUtil*100 + "%";
            }
            else if (name.StartsWith("PDP"))
            {
                return name + ": " + en.getPDPChannel(int.Parse(name.Split(' ')[1])) + "A";
            }
            
            else if (name.Equals("DS Disabled"))
            {
                return name + ": " + en.DSDisabled;
            }
            else if (name.Equals("DS Auto"))
            {
                return name + ": " + en.DSAuto;
            }
            else if (name.Equals("DS Tele"))
            {
                return name + ": " + en.DSTele;
            }

            else if (name.Equals("Robot Disabled"))
            {
                return name + ": " + en.RobotDisabled;
            }
            else if (name.Equals("Robot Auto"))
            {
                return name + ": " + en.RobotAuto;
            }
            else if (name.Equals("Robot Tele"))
            {
                return name + ": " + en.RobotTele;
            }

            else if (name.Equals("Brownout"))
            {
                return name +": " + en.Brownout;
            }
            else if (name.Equals("Watchdog"))
            {
                return name + ": " + en.Watchdog;
            }
            return name+"";
        }

        #endregion

        //group checkbox
        #region
        //Checkbox event for group tab checkboxs
        private void groupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetSeriesEnabledForGroups();
            setColoumnLabelNumber();
        }

        void SetSeriesEnabledForGroups()
        {
           checkFromString("DS Disabled", checkBox1.Checked);
            checkFromString("DS Auto", checkBox1.Checked);
            checkFromString("DS Tele", checkBox1.Checked);

            checkFromString("Robot Disabled", checkBox1.Checked);
            checkFromString("Robot Auto", checkBox1.Checked);
            checkFromString("Robot Tele", checkBox1.Checked);

            checkFromString("Brownout", checkBox1.Checked);
            checkFromString("Watchdog", checkBox1.Checked);

            checkFromString("Voltage", checkBox2.Checked);
            checkFromString("roboRIO CPU", checkBox2.Checked);
            checkFromString("CAN", checkBox2.Checked);

            checkFromString("Trip Time", checkBox3.Checked);
            checkFromString("Lost Packets", checkBox3.Checked);
           
            for (int i = 0; i < 4; i++)
            {
                checkFromString("PDP " + i, checkBox4.Checked);
            }

            for (int i = 4; i < 8; i++)
            {
                checkFromString("PDP " + i, checkBox5.Checked);
            }
            for (int i = 8; i < 12; i++)
            {
                checkFromString("PDP " + i, checkBox6.Checked);
            }

            for (int i = 12; i < 16; i++)
            {
                checkFromString("PDP " + i, checkBox7.Checked);
            }

        }

        //Checks the plot checkbox named s
        void checkFromString(string s, bool b)
        {
            foreach (Control con in tabPage2.Controls)
            {
                if (con.Text.Equals(s))
                {
                    ((CheckBox)con).Checked = b;
                    break;
                }
                
            }
        }
        #endregion

        //Time scale and time scale label
        #region
        //Set timescale label when view is changed
        private void chartMain_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if(log != null) menuStrip1.Items[menuStrip1.Items.Count-1].Text =  "Time Scale " + getTotalSecoundsInView() + " Sec";
        }

        //gets time scale
        double getTotalSecoundsInView()
        {
            return ((getIndexOfMaxView() - getIndexOfMinView()) * 20) / 1000d;
        }

        int getIndexOfMinView()
        {
            return (int)(DateTime.FromOADate(chartMain.ChartAreas[0].AxisX.ScaleView.ViewMinimum).Subtract(log.StartTime).TotalMilliseconds / 20);
        }

        int getIndexOfMaxView()
        {
            return (int)(DateTime.FromOADate(chartMain.ChartAreas[0].AxisX.ScaleView.ViewMaximum).Subtract(log.StartTime).TotalMilliseconds / 20);
        }
        #endregion

        //Export
        #region
        //Event for export button
        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (log != null)
            {
                
                if (comboBoxExport.SelectedIndex != 1)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    if (comboBoxExport.SelectedIndex == 0)
                    {
                        saveFile.Filter = "CSV File|*.csv";
                        saveFile.Title = "Save the Exported CSV";
                    }
                    if (comboBoxExport.SelectedIndex == 2)
                    {
                        saveFile.Filter = "PNG FIle|*.png";
                        saveFile.Title = "Save the PNG Image";
                    }
                    saveFile.ShowDialog();
                    if (saveFile.FileName != "")
                    {
                        export(comboBoxExport.SelectedIndex, saveFile.FileName);
                    }
                }
                else
                {
                    export(comboBoxExport.SelectedIndex, "");
                }
                
            }
        }

        private double VarNameToValue(String name, Entry en)
        {
            if (name.Equals("Trip Time"))
            {
                return en.TripTime;
            }
            else if (name.Equals("Lost Packets"))
            {
                return en.LostPackets;
            }
            else if (name.Equals("Voltage"))
            {
                return en.Voltage;
            }
            else if (name.Equals("roboRIO CPU"))
            {
                return en.RoboRioCPU;
            }
            else if (name.Equals("CAN"))
            {
                return en.CANUtil;
            }
            else if (name.StartsWith("PDP"))
            {
                return en.getPDPChannel(int.Parse(name.Split(' ')[1]));
            }


            return -1;
        }
        private bool BoolNameToValue(String name, Entry en)
        {
            if (name.Equals("DS Disabled"))
            {
                return en.DSDisabled;
            }
            else if (name.Equals("DS Auto"))
            {
                return en.DSAuto;
            }
            else if (name.Equals("DS Tele"))
            {
                return en.DSTele;
            }
            else if (name.Equals("Robot Disabled"))
            {
                return en.RobotDisabled;
            }
            else if (name.Equals("Robot Auto"))
            {
                return en.RobotAuto;
            }
            else if (name.Equals("Robot Teled"))
            {
                return en.RobotTele;
            }
            else if (name.Equals("Brownout"))
            {
                return en.Brownout;
            }
            else if (name.Equals("Watchdog"))
            {
                return en.Brownout;
            }
            else return false;
        }

        //Export data in view
        void export(int dropIndex, string path)
        {
            //CSV
            if (dropIndex == 0)
            { 
                var csv = new StringBuilder();
                List<String> headerList = new List<String>();
                for (int w = getIndexOfMinView() - 1; w < getIndexOfMaxView(); w++)
                {
                    if (w == getIndexOfMinView() - 1)
                    {
                        csv.Append("Time,");
                        for (int x = 0; x < chartMain.Series.Count; x++)
                        {
                            if (chartMain.Series[x].Enabled)
                            {
                                csv.Append(chartMain.Series[x].Name + ",");
                                headerList.Add(chartMain.Series[x].Name);
                            }
                        }
                        if (checkBoxTotalCurrent.Checked) csv.Append("Total Current,");
                    }
                    else
                    {
                        double totalA = 0;
                        Entry en = log.Entries.ElementAt(w);
                        csv.Append(en.Time.ToString("HH:mm:ss.fff") + ",");
                        foreach (String header in headerList)
                        {
                            //Gets data from entry using the header name
                            if (header.Contains("Robot") || header.Contains("DS") || header.Contains("Brownout") || header.Contains("Watch"))  csv.Append(BoolNameToValue(header, en)+",");
                            else csv.Append(VarNameToValue(header, en) + ",");
                            
                            if (checkBoxTotalCurrent.Checked) if (header.StartsWith("PDP")) totalA += en.getPDPChannel(int.Parse(header.Split(' ')[1]));
                        }
                        if (checkBoxTotalCurrent.Checked) csv.Append(totalA + ",");
                    }
                    csv.AppendLine("");
                }
                File.WriteAllText(path, csv.ToString());
            }
            //ClipBoard
            else if (dropIndex == 1)
            {
                var clipBoardText = new StringBuilder();
                List<String> headerList = new List<String>();
                for (int w = getIndexOfMinView() - 1; w < getIndexOfMaxView(); w++)
                {
                    if (w == getIndexOfMinView() - 1)
                    {
                        clipBoardText.Append("Time" + "\t");
                        for (int x = 0; x < chartMain.Series.Count; x++)
                        {
                            if (chartMain.Series[x].Enabled)
                            {
                                clipBoardText.Append(chartMain.Series[x].Name + "\t");
                                headerList.Add(chartMain.Series[x].Name);
                            }
                        }
                        if (checkBoxTotalCurrent.Checked) clipBoardText.Append("Total Current" + "\t");

                    }
                    else
                    {
                        double totalA = 0;
                        Entry en = log.Entries.ElementAt(w);
                        clipBoardText.Append(en.Time.ToString("HH:mm:ss.fff") + "\t");
                        foreach (String s in headerList)
                        {
                            if (s.Contains("Robot") || s.Contains("DS") || s.Contains("Brownout") || s.Contains("Watch")) clipBoardText.Append(BoolNameToValue(s, en) + "\t");
                            else clipBoardText.Append(VarNameToValue(s, en) + "\t");
                           
                            if (checkBoxTotalCurrent.Checked) if (s.StartsWith("PDP")) totalA += en.getPDPChannel(int.Parse(s.Split(' ')[1]));
                        }
                        if (checkBoxTotalCurrent.Checked) clipBoardText.Append(totalA + "\t");
                    }

                    clipBoardText.AppendLine("");

                }
                Clipboard.SetText(clipBoardText.ToString());
            }
            //image
            else if (dropIndex == 2)
            {
                chartMain.SaveImage(path, ChartImageFormat.Png);
            }
        }


        //Change total coloumn label
        void setColoumnLabelNumber()
        {
            int num = 0;
            foreach (Series s in chartMain.Series)
            {
                if (s.Enabled) num++;

            }
            if (checkBoxTotalCurrent.Checked) num++;
            labelColumns.Text = "Total  Columns: " + num;
        }
        
       

        private void checkBoxTotalCurrent_CheckedChanged(object sender, EventArgs e)
        {
            setColoumnLabelNumber();
        }

       

        
        #endregion

        //Zoom
        #region
        //Make sure people don't zoom out of view
        private void chartMain_AxisViewChanging(object sender, ViewEventArgs e)
        {
            if (!Double.IsNaN(e.NewPosition) && log != null && !Double.IsNaN(e.NewSize))
            {
                if (((int)(DateTime.FromOADate(e.NewPosition + e.NewSize).Subtract(log.StartTime).TotalMilliseconds / 20)) > log.Entries.Count)
                {
                    e.NewPosition = log.Entries.Last().Time.ToOADate() - e.NewSize;
                }
            }
        }

        //Reset Zoom
        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset(100);
        }
        #endregion

        //file listView
        #region
        //Folder path for the files in the list view
        string listviewFolderPath;
        
        //Adds items to listview (info will be added when item is in view)
        void addLogFilesToViewer(string path)
        {
            listViewDSLOGFolder.Items.Clear();
            if (Directory.Exists(path)) { 
            DirectoryInfo dslogDir = new DirectoryInfo(path);
            listviewFolderPath = path;
            FileInfo[] Files = dslogDir.GetFiles("*.dslog");

            for (int y = 0; y < Files.Count(); y++)
            {
                    ListViewItem item = new ListViewItem();
                    item.Text = Files[y].Name.Replace(".dslog", "");
                    listViewDSLOGFolder.Items.Add(item);
                
            }
            //sroll down to bottom (need to use timer cuz it's weird
            lastIndexSelectedFiles = -1;
            timerScrollToBottom.Start();

            fileCreatedWatcher(path);
            }
        }

        private void fileCreatedWatcher(String path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = "*.dslog*";
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            ListViewItem item = new ListViewItem();
            item.Text = e.Name.Replace(".dslog", "");
            listViewDSLOGFolder.Invoke(new MethodInvoker(delegate { listViewDSLOGFolder.Items.Add(item); }));
            listViewDSLOGFolder.Invoke(new MethodInvoker(delegate { addItemFileInfo(listViewDSLOGFolder.Items.Count - 1); }));
        }

        

        private void refreshPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLogFilesToViewer(listviewFolderPath);
        }

        //last Index Selected so we don't load the file again
        int lastIndexSelectedFiles = 0;

        //import file when selection changes
        private void listViewDSLOGFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDSLOGFolder.SelectedItems.Count != 0)
            {
                if (listViewDSLOGFolder.SelectedItems[0].Index != lastIndexSelectedFiles)
                {
                    lastIndexSelectedFiles = listViewDSLOGFolder.SelectedItems[0].Index;
                    importLog(listviewFolderPath + "\\" + listViewDSLOGFolder.SelectedItems[0].Text + ".dslog");
                }
            }
        }
        //sroll down to bottom (need to use timer cuz it's weird
        private void timerScrollToBottom_Tick(object sender, EventArgs e)
        {
            listViewDSLOGFolder.EnsureVisible(listViewDSLOGFolder.Items.Count - 1);
            timerScrollToBottom.Stop();
        }

        //makes sure we don't keep looping through the same items after we already have
        int lastTop = 100;
        //Adds information to listview items when they scroll into view
        private void logUpdate_Tick(object sender, EventArgs e)
        {
            if (listViewDSLOGFolder.Items.Count != 0)
            {
                int topIndex = listViewDSLOGFolder.TopItem.Index;
                if (lastTop != topIndex)
                {
                    
                    for (int inx = topIndex; inx < topIndex + listViewDSLOGFolder.Height / 18; inx++)
                    {
                        addItemFileInfo(inx);
                    }
                    lastTop = topIndex;
                }
                for (int inx = topIndex; inx < topIndex + listViewDSLOGFolder.Height / 18; inx++)
                {
                    if (inx < listViewDSLOGFolder.Items.Count)
                    {
                        if (listViewDSLOGFolder.Items[inx].BackColor.Equals(Color.Lime))
                        {
                            try
                            {
                                File.OpenRead(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Close();
                                listViewDSLOGFolder.Items[inx].BackColor = SystemColors.Window;
                            }
                            catch (IOException ex)
                            {
                                //listViewDSLOGFolder.Items[inx].BackColor = Color.Lime;
                            }
                        }
                    }
                }
            }
        }

        private void addItemFileInfo(int inx) {
            if (inx < listViewDSLOGFolder.Items.Count)
            {
                //Check if we already added info
                if (listViewDSLOGFolder.Items[inx].SubItems.Count < 2)
                {
                    try
                    {
                        if (File.Exists(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dsevents"))
                        {
                            DSEVENTSReader dsevents = new DSEVENTSReader(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dsevents");
                            if (dsevents.Version == 3)
                            {
                                DateTime sTime = dsevents.StartTime;
                                listViewDSLOGFolder.Items[inx].SubItems.Add(sTime.ToString("MMM dd, HH:mm:ss ddd"));
                                StringBuilder sb = new StringBuilder();
                                foreach (InfoEntry en in dsevents.EntryList)
                                {
                                    sb.Append(en.Data);
                                }

                                String txtF = sb.ToString();
                                listViewDSLOGFolder.Items[inx].SubItems.Add("" + ((new FileInfo(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Length - 19) / 35) / 50);
                                if (txtF.Contains("FMS Connected:   Qualification"))
                                {
                                    listViewDSLOGFolder.Items[inx].SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Qualification - ") + 33, 5).Split(':')[0]);
                                    listViewDSLOGFolder.Items[inx].BackColor = Color.Khaki;
                                }
                                else if (txtF.Contains("FMS Connected:   Elimination"))
                                {
                                    listViewDSLOGFolder.Items[inx].SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Elimination - ") + 31, 5).Split(':')[0]);
                                    listViewDSLOGFolder.Items[inx].BackColor = Color.LightCoral;
                                }
                                else if (txtF.Contains("FMS Connected:   Practice"))
                                {
                                    listViewDSLOGFolder.Items[inx].SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Practice - ") + 28, 5).Split(':')[0]);
                                    listViewDSLOGFolder.Items[inx].BackColor = Color.LightGreen;
                                }
                                else if (txtF.Contains("FMS Connected:   None"))
                                {
                                    listViewDSLOGFolder.Items[inx].SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   None - ") + 24, 5).Split(':')[0]);
                                    listViewDSLOGFolder.Items[inx].BackColor = Color.LightSkyBlue;
                                }
                                else
                                {
                                    listViewDSLOGFolder.Items[inx].SubItems.Add("");
                                }

                                //Gets time since right now
                                TimeSpan sub = DateTime.Now.Subtract(sTime);
                                //sTime = sTime.Subtract(new TimeSpan(2, 0, 0));
                                listViewDSLOGFolder.Items[inx].SubItems.Add(sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m");

                                if (txtF.Contains("FMS Event Name: "))
                                {
                                    string[] sArray = txtF.Split(new string[] { "Info" }, StringSplitOptions.None);
                                    foreach (String ss in sArray)
                                    {
                                        if (ss.Contains("FMS Event Name: "))
                                        {
                                            listViewDSLOGFolder.Items[inx].SubItems.Add(ss.Replace("FMS Event Name: ", ""));
                                            //listViewDSLOGFolder.Items[inx].SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Event Name: ") + 16, 8).Replace("Info", "").Replace("Inf", "").Replace("Joy", ""));
                                            break;
                                        }
                                    }

                                }
                                try
                                {
                                    File.OpenRead(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Close();
                                }
                                catch (IOException ex)
                                {
                                    listViewDSLOGFolder.Items[inx].BackColor = Color.Lime;
                                }
                            }
                            else
                            {
                                listViewDSLOGFolder.Items[inx].BackColor = SystemColors.ControlDark;
                                listViewDSLOGFolder.Items[inx].SubItems.Add("VERSION");
                                listViewDSLOGFolder.Items[inx].SubItems.Add("NOT");
                                listViewDSLOGFolder.Items[inx].SubItems.Add("");
                                listViewDSLOGFolder.Items[inx].SubItems.Add("SUPPORTED");
                            }
                        }
                        else
                        {
                            //No DSEVENT file
                            DateTime sTime;
                            using (BinaryReader2 reader = new BinaryReader2(File.Open(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                            {
                                reader.ReadInt32();
                                sTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                                listViewDSLOGFolder.Items[inx].SubItems.Add(sTime.ToString("MMM dd, HH:mm:ss ddd"));
                                reader.Close();
                            }
                            listViewDSLOGFolder.Items[inx].SubItems.Add("" + ((new FileInfo(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Length - 19) / 35) / 50);
                            listViewDSLOGFolder.Items[inx].SubItems.Add("NO");
                            TimeSpan sub = DateTime.Now.Subtract(sTime);
                            listViewDSLOGFolder.Items[inx].SubItems.Add(sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m");
                            listViewDSLOGFolder.Items[inx].SubItems.Add("DSEVENT");
                            
                            try
                            {
                                File.OpenRead(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Close();
                            }
                            catch (IOException ex)
                            {
                                listViewDSLOGFolder.Items[inx].BackColor = Color.Lime;
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void changeLogFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Select the directory to load log files from";
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                addLogFilesToViewer(folder.SelectedPath);

            }

        }
        #endregion

        //Import
        #region
        //Imports log from path
        void importLog(string path)
        {
            if (File.Exists(path))
            {
                if (log != null)
                {
                    if (log is DSLOGStreamer) ((DSLOGStreamer)log).Close();
                }
                timerStream.Stop();
                clearGraph();
                chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                menuStrip1.BackColor = SystemColors.Control;
                Boolean stream = false;
                //check if file is open by ds
                try
                {
                    File.OpenRead(path).Close();
                }
                catch (IOException ex)
                {
                    stream = true;
                }

                log = null;
                if (!stream)
                {
                    autoScrollToolStripMenuItem.Enabled = false;
                    log = new DSLOGReader(path);
                }
                else
                {
                    autoScrollToolStripMenuItem.Enabled = true;
                    autoScrollToolStripMenuItem.Checked = true;
                    log = new DSLOGStreamer(path);
                }
                if (log.Version == 3)
                {
                    menuStrip1.Items[menuStrip1.Items.Count - 2].Text = "Current File: " + path;
                    chartMain.ChartAreas[0].AxisX.Minimum = log.StartTime.ToOADate();
                    chartMain.ChartAreas[0].CursorX.IntervalOffset = log.StartTime.Millisecond % 20;
                    //for lost packets
                    int packetnum = 0;

                    for (int w = 0; w < log.Entries.Count; w++)
                    {
                        Entry en = log.Entries.ElementAt(w);
                        //Adds points to first and last x values
                        if (w == 0 || w == log.Entries.Count - 1)
                        {
                            chartMain.Series["Trip Time"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                            chartMain.Series["Voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                            chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), en.LostPackets * 100);
                            chartMain.Series["roboRIO CPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                            chartMain.Series["CAN"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                            for (int i = 0; i < 16; i++)
                            {
                                chartMain.Series["PDP " + i].Points.AddXY(en.Time.ToOADate(), en.getPDPChannel(i));
                            }
                        }
                        else
                        {
                            //Checks if value is differnt around it so we don't plot everypoint
                            if (log.Entries.ElementAt(w - 1).TripTime != en.TripTime || log.Entries.ElementAt(w + 1).TripTime != en.TripTime)
                            {
                                chartMain.Series["Trip Time"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                            }
                            if ((log.Entries.ElementAt(w - 1).LostPackets != en.LostPackets || log.Entries.ElementAt(w + 1).LostPackets != en.LostPackets) || log.Entries.ElementAt(w - 1).LostPackets != 0)
                            {
                                //the bar graphs are too much so we have to do this
                                if (packetnum % 4 == 0)
                                {
                                    chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), (en.LostPackets < 1) ? en.LostPackets * 100 : 100);
                                }
                                else
                                {
                                    chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), 0);
                                }
                                packetnum++;
                            }
                            if ((log.Entries.ElementAt(w - 1).Voltage != en.Voltage || log.Entries.ElementAt(w + 1).Voltage != en.Voltage) && en.Voltage < 17)
                            {
                                chartMain.Series["Voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                            }
                            if (log.Entries.ElementAt(w - 1).RoboRioCPU != en.RoboRioCPU || log.Entries.ElementAt(w + 1).RoboRioCPU != en.RoboRioCPU)
                            {
                                chartMain.Series["roboRIO CPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                            }
                            if (log.Entries.ElementAt(w - 1).CANUtil != en.CANUtil || log.Entries.ElementAt(w + 1).CANUtil != en.CANUtil)
                            {
                                chartMain.Series["CAN"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                            }
                            for (int i = 0; i < 16; i++)
                            {
                                if (log.Entries.ElementAt(w - 1).getPDPChannel(i) != en.getPDPChannel(i) || log.Entries.ElementAt(w + 1).getPDPChannel(i) != en.getPDPChannel(i))
                                {
                                    chartMain.Series["PDP " + i].Points.AddXY(en.Time.ToOADate(), en.getPDPChannel(i));
                                }
                            }
                        }

                        if (en.DSDisabled) chartMain.Series["DS Disabled"].Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSAuto) chartMain.Series["DS Auto"].Points.AddXY(en.Time.ToOADate(), 15.9);
                        if (en.DSTele) chartMain.Series["DS Tele"].Points.AddXY(en.Time.ToOADate(), 15.9);

                        if (en.RobotDisabled) chartMain.Series["Robot Disabled"].Points.AddXY(en.Time.ToOADate(), 16.8);
                        if (en.RobotAuto) chartMain.Series["Robot Auto"].Points.AddXY(en.Time.ToOADate(), 16.5);
                        if (en.RobotTele) chartMain.Series["Robot Tele"].Points.AddXY(en.Time.ToOADate(), 16.2);

                        if (en.Brownout) chartMain.Series["Brownout"].Points.AddXY(en.Time.ToOADate(), 15.6);
                        if (en.Watchdog) chartMain.Series["Watchdog"].Points.AddXY(en.Time.ToOADate(), 15.3);
                    }
                    Dsevents = null;
                    EventsDict = null;
                    readDSEVENTS(path);
                    chartMain.ChartAreas[0].AxisX.Maximum = log.Entries.Last().Time.ToOADate();
                    menuStrip1.Items[menuStrip1.Items.Count - 1].Text = "Time Scale " + getTotalSecoundsInView() + " Sec";
                    setColoumnLabelNumber();
                    tabPage4.Enabled = true;
                    chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    lastIndexSelectedEvents = -1;
                    lastNumQueue = 0;
                    EventRichTextBox.Clear();
                    if (stream) timerStream.Start();
                }
                else
                {
                    MessageBox.Show("ERROR: dslog version not supported");
                }
            }
            
        }
        
        private DSEVENTSReader Dsevents;
        private Dictionary<Double, String> EventsDict;
        private void readDSEVENTS(string path)
        {
            Dsevents = new DSEVENTSReader(path.Replace(".dslog", ".dsevents"));
            EventsDict = new Dictionary<double, string>();
            listViewEvents.Items.Clear();
            addEventPointsAndListView();
        }

        private void addEventPointsAndListView()
        {
            foreach (InfoEntry en in Dsevents.EntryList)
            {
                if (en.Data.ToLower().Contains(toolStripTextBox1.Text.ToLower()))
                {
                    DataPoint po = new DataPoint(en.Time.ToOADate(), 15.2);
                    po.MarkerSize = 6;
                    ListViewItem item = new ListViewItem();
                    item.Text = en.TimeData;
                    item.SubItems.Add(en.Data);
                    
                    if (en.Data.Contains("ERROR") || en.Data.Contains("<flags> 1"))
                    {
                        item.BackColor = Color.Red;
                        po.Color = Color.Red;
                        po.YValues[0] = 14.9;
                    }
                    else if (en.Data.Contains("<Code> 44004 "))
                    {
                        item.BackColor = Color.SandyBrown;
                        po.Color = Color.SandyBrown;
                        po.MarkerStyle = MarkerStyle.Square;
                        po.YValues[0] = 14.9;
                    }
                    else if (en.Data.Contains("<Code> 44008 "))
                    {
                            string[] lostString = en.Data.Split(new string[] { " <radioSeenEvents>  " }, StringSplitOptions.None)[0].Split('>')[2].Split(',');
                            if (lostString.Length != 0)
                            {
                                double[] arrayLost = Array.ConvertAll(lostString, Double.Parse);
                                foreach (double d in arrayLost)
                                {
                                    DateTime newTime = en.Time.AddSeconds(-d);
                                    DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.9);
                                    pRL.Color = Color.Yellow;
                                    pRL.MarkerSize = 6;
                                    pRL.MarkerStyle = MarkerStyle.Square;
                                    pRL.YValues[0] = 14.9;
                                    chartMain.Series["Messages"].Points.Add(pRL);
                                    EventsDict[newTime.ToOADate()] = "Radio Lost";
                                }
                            }
                            string[] seenString = en.Data.Split(new string[] { " <radioSeenEvents>  " }, StringSplitOptions.None)[1].Split('<')[0].Split(',');
                            if (seenString.Length != 0)
                            {
                                double[] arraySeen = Array.ConvertAll(seenString, Double.Parse);
                                foreach (double d in arraySeen)
                                {
                                    DateTime newTime = en.Time.AddSeconds(-d);
                                    DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.9);
                                    pRL.Color = Color.Lime;
                                    pRL.MarkerSize = 6;
                                    pRL.MarkerStyle = MarkerStyle.Square;
                                    pRL.YValues[0] = 14.9;
                                    chartMain.Series["Messages"].Points.Add(pRL);
                                    EventsDict[newTime.ToOADate()] = "Radio Seen";
                                }
                            }
                        
                        item.BackColor = Color.Khaki;
                        po.Color = Color.Khaki;
                        po.YValues[0] = 14.9;
                    }
                    else if (en.Data.Contains("Warning") || en.Data.Contains("<flags> 2"))
                    {
                        item.BackColor = Color.Khaki;
                        po.Color = Color.Khaki;
                        po.YValues[0] = 14.9;
                    }
                    item.SubItems.Add("" + en.Time.ToOADate());
                    listViewEvents.Items.Add(item);
                    chartMain.Series["Messages"].Points.Add(po);
                    EventsDict[en.Time.ToOADate()] = en.Data;
                }
            }
        }

        //clears the graph of points
        void clearGraph()
        {
            foreach (Series ser in chartMain.Series)
            {
                ClearPointsQuick(ser);
            }
        }

        //Have to use this cuz .Clear() takes way to long
        public void ClearPointsQuick(Series s)
        {
            if (s.Points.Count != 0)
            {
                s.Points.SuspendUpdates();
                while (s.Points.Count > 0)
                    s.Points.RemoveAt(s.Points.Count - 1);
                s.Points.ResumeUpdates();
            }

        }

        //Load single log
        private void loadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "DSLOG Files (.dslog)|*.dslog";
            openFile.Multiselect = false;
            bool? userClickedOK = (DialogResult.OK == openFile.ShowDialog());
            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                importLog(openFile.FileName);
            }
        }

        #endregion

        //Reads labview datetime stamp format
        private DateTime FromLVTime(long unixTime, UInt64 ummm)
        {
            var epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds(unixTime);
            epoch = epoch.AddHours(-5);

            return epoch.AddSeconds(((double)ummm / UInt64.MaxValue));
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/orangelight/dslog-reader/blob/master/Help.md");
        }

        
        int lastIndexSelectedEvents = -1;
        private void listViewEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEvents.SelectedItems.Count != 0)
            {
                if (listViewEvents.SelectedItems[0].Index != lastIndexSelectedEvents)
                {
                    lastIndexSelectedEvents = listViewEvents.SelectedItems[0].Index;
                    EventRichTextBox.Text = listViewEvents.SelectedItems[0].SubItems[1].Text;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (log != null)
            {
                if (!Double.IsNaN(chartMain.ChartAreas[0].CursorX.Position))
                {
                    if (keyData == Keys.Left)
                    {
                        //(((getTotalSecoundsInView() / 300d) < .002) ? .002 : (getTotalSecoundsInView() / 300d))
                        chartMain.ChartAreas[0].CursorX.SetCursorPosition(DateTime.FromOADate(chartMain.ChartAreas[0].CursorX.Position).AddSeconds(-(((getTotalSecoundsInView() / 300d) < .02) ? .02 : (getTotalSecoundsInView() / 250d))).ToOADate());
                        probe(chartMain.ChartAreas[0].CursorX.Position);
                        setCursorLineRed();
                        return true;
                    }
                    if (keyData == Keys.Right)
                    {
                        chartMain.ChartAreas[0].CursorX.SetCursorPosition(DateTime.FromOADate(chartMain.ChartAreas[0].CursorX.Position).AddSeconds((((getTotalSecoundsInView() / 300d) < .02) ? .02 : (getTotalSecoundsInView() / 250d))).ToOADate());
                        probe(chartMain.ChartAreas[0].CursorX.Position);
                        setCursorLineRed();
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void listViewEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chartMain.ChartAreas[0].CursorX.SetCursorPosition(Double.Parse(listViewEvents.SelectedItems[0].SubItems[2].Text));
            probe(chartMain.ChartAreas[0].CursorX.Position);
            tabControlChart.SelectTab(0);
        }
        
        bool lineRed = true;
        private void GraphCorsorLine_Tick(object sender, EventArgs e)
        {
            if (lineRed)
            {
                chartMain.ChartAreas[0].CursorX.LineColor = Color.Transparent;
                GraphCorsorLine.Interval = 500;
                lineRed = false;
            }
            else
            {
                chartMain.ChartAreas[0].CursorX.LineColor = Color.Red;
                lineRed = true;
                GraphCorsorLine.Interval = 500;
            }
        }
        Point? prevPosition = null;
        private void chartMain_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value) return;
            prevPosition = pos;
            var results = chartMain.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        if (prop.YValues[0] == 15.2 || prop.YValues[0] == 14.9)
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (25 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 25 &&
                                Math.Abs(pos.Y - pointYPixel) < 25)
                            {
                                try
                                {
                                    String data = EventsDict[prop.XValue];
                                    GraphRichTextBox.BackColor = messageColor(data);
                                    GraphRichTextBox.Text = EventsDict[prop.XValue];
                                }
                                catch (KeyNotFoundException ex)
                                {

                                }
                            }
                        }
                    }
                }
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Dsevents != null)
            {
                listViewEvents.BeginUpdate();
                ClearPointsQuick(chartMain.Series["Messages"]);
                listViewEvents.Items.Clear();
                addEventPointsAndListView();
                listViewEvents.EndUpdate();         
            } 
        }

        private Color messageColor(String s)
        {
            if (s.Contains("ERROR") || s.Contains("<flags> 1"))
            {
                return Color.LightCoral;
            }
            else if (s.Contains("<Code> 44004 "))
            {
                return Color.SandyBrown;
            }
            else if (s.Equals("Radio Seen"))
            {
                return Color.Lime;
            }
            else if (s.Equals("Radio Lost"))
            {
                return Color.Yellow;
            }
            else if (s.Contains("Warning") || s.Contains("<flags> 2"))
            {
                return Color.Khaki;
            }
            else
            {
                return SystemColors.Window;
            }
        }
        int lastNumQueue = 0;
        private void timerStream_Tick(object sender, EventArgs e)
        {
            DSLOGStreamer StreamDS = (DSLOGStreamer)log;
            if (StreamDS.Queue.Count-lastNumQueue >= 2)
            {
                menuStrip1.BackColor = Color.Lime;
                if (lastNumQueue != 0)
                {
                    chartMain.Series["Trip Time"].Points.RemoveAt(chartMain.Series["Trip Time"].Points.Count - 1);
                    chartMain.Series["Voltage"].Points.RemoveAt(chartMain.Series["Voltage"].Points.Count - 1);
                    chartMain.Series["Lost Packets"].Points.RemoveAt(chartMain.Series["Lost Packets"].Points.Count - 1);
                    chartMain.Series["roboRIO CPU"].Points.RemoveAt(chartMain.Series["roboRIO CPU"].Points.Count - 1);
                    chartMain.Series["CAN"].Points.RemoveAt(chartMain.Series["CAN"].Points.Count - 1);
                    for (int i = 0; i < 16; i++)
                    {
                        chartMain.Series["PDP " + i].Points.RemoveAt(chartMain.Series["PDP " + i].Points.Count-1);
                    }
                }
                int packetnum = 0;
                for (int w = lastNumQueue; w < StreamDS.Queue.Count; w++)
                {
                    Entry en = StreamDS.Queue.ElementAt(w);
                    //Adds points to first and last x values
                    if (w == StreamDS.Queue.Count-1 || w ==0)
                    {
                        chartMain.Series["Trip Time"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        chartMain.Series["Voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), en.LostPackets * 100);
                        chartMain.Series["roboRIO CPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        chartMain.Series["CAN"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        for (int i = 0; i < 16; i++)
                        {
                            chartMain.Series["PDP " + i].Points.AddXY(en.Time.ToOADate(), en.getPDPChannel(i));
                        }
                    }
                    else
                    {
                        //Checks if value is differnt around it so we don't plot everypoint
                        if (StreamDS.Queue.ElementAt(w - 1).TripTime != en.TripTime || StreamDS.Queue.ElementAt(w + 1).TripTime != en.TripTime)
                        {
                            chartMain.Series["Trip Time"].Points.AddXY(en.Time.ToOADate(), en.TripTime);
                        }
                        if ((StreamDS.Queue.ElementAt(w - 1).LostPackets != en.LostPackets || StreamDS.Queue.ElementAt(w + 1).LostPackets != en.LostPackets) || StreamDS.Queue.ElementAt(w - 1).LostPackets != 0)
                        {
                            //the bar graphs are too much so we have to do this
                            if (packetnum % 4 == 0)
                            {
                                chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), (en.LostPackets < 1) ? en.LostPackets * 100 : 100);
                            }
                            else
                            {
                                chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), 0);
                            }
                            packetnum++;
                        }
                        if ((StreamDS.Queue.ElementAt(w - 1).Voltage != en.Voltage || StreamDS.Queue.ElementAt(w + 1).Voltage != en.Voltage) && en.Voltage < 17)
                        {
                            chartMain.Series["Voltage"].Points.AddXY(en.Time.ToOADate(), en.Voltage);
                        }
                        if (StreamDS.Queue.ElementAt(w - 1).RoboRioCPU != en.RoboRioCPU || StreamDS.Queue.ElementAt(w + 1).RoboRioCPU != en.RoboRioCPU)
                        {
                            chartMain.Series["roboRIO CPU"].Points.AddXY(en.Time.ToOADate(), en.RoboRioCPU * 100);
                        }
                        if (StreamDS.Queue.ElementAt(w - 1).CANUtil != en.CANUtil || StreamDS.Queue.ElementAt(w + 1).CANUtil != en.CANUtil)
                        {
                            chartMain.Series["CAN"].Points.AddXY(en.Time.ToOADate(), en.CANUtil * 100);
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            if (StreamDS.Queue.ElementAt(w - 1).getPDPChannel(i) != en.getPDPChannel(i) || StreamDS.Queue.ElementAt(w + 1).getPDPChannel(i) != en.getPDPChannel(i))
                            {
                                chartMain.Series["PDP " + i].Points.AddXY(en.Time.ToOADate(), en.getPDPChannel(i));
                            }
                        }
                    }

                    if (en.DSDisabled) chartMain.Series["DS Disabled"].Points.AddXY(en.Time.ToOADate(), 15.9);
                    if (en.DSAuto) chartMain.Series["DS Auto"].Points.AddXY(en.Time.ToOADate(), 15.9);
                    if (en.DSTele) chartMain.Series["DS Tele"].Points.AddXY(en.Time.ToOADate(), 15.9);

                    if (en.RobotDisabled) chartMain.Series["Robot Disabled"].Points.AddXY(en.Time.ToOADate(), 16.8);
                    if (en.RobotAuto) chartMain.Series["Robot Auto"].Points.AddXY(en.Time.ToOADate(), 16.5);
                    if (en.RobotTele) chartMain.Series["Robot Tele"].Points.AddXY(en.Time.ToOADate(), 16.2);

                    if (en.Brownout) chartMain.Series["Brownout"].Points.AddXY(en.Time.ToOADate(), 15.6);
                    if (en.Watchdog) chartMain.Series["Watchdog"].Points.AddXY(en.Time.ToOADate(), 15.3);
                }
                menuStrip1.Items[menuStrip1.Items.Count - 1].Text = "Time Scale " + getTotalSecoundsInView() + " Sec";
                chartMain.ChartAreas[0].AxisX.Maximum = StreamDS.Queue.Last().Time.ToOADate();
                
                //chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                //double pos = chartMain.ChartAreas[0].AxisX.ScaleView.Position;
                if (autoScrollToolStripMenuItem.Checked) chartMain.ChartAreas[0].AxisX.ScaleView.Scroll(StreamDS.Queue.Last().Time.ToOADate());
                new Thread(menuColorBack).Start();
                //chartMain.ChartAreas[0].AxisX.ScaleView.Scroll(pos);
            }
            lastNumQueue = StreamDS.Queue.Count;
            
            //timerMenuColor.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (log != null)
            {
                timerStream.Stop();
                if (log is DSLOGStreamer) ((DSLOGStreamer)log).Close();
            }
        }

        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoScrollToolStripMenuItem.Checked = !autoScrollToolStripMenuItem.Checked;
        }

        private void menuColorBack()
        {
            Thread.Sleep(200);
            try
            {
                menuStrip1.Invoke(new MethodInvoker(delegate { menuStrip1.BackColor = SystemColors.Control; }));
            }
            catch (Exception ex)
            {

            }
        }


    }
}
