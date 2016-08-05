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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dslog
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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

            for (int i = 0; i < 16; i++)
            {
                chartMain.Series.Add(new Series("PDP " + i));
                chartMain.Series["PDP " + i].XValueType = ChartValueType.DateTime;
                chartMain.Series["PDP " + i].YAxisType = AxisType.Secondary;
                chartMain.Series["PDP " + i].ChartType = SeriesChartType.FastLine;
                chartMain.Series["PDP " + i].Color = pdpColors[i];
                chartMain.Series["PDP " + i].Enabled = false;
            }

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
                    labelInfoPanel.Controls.Clear();
                    //Get x value form probe
                    DateTime xValue = DateTime.FromOADate(e.NewPosition);
                    //get entry from x value
                    Entry en = log.Entries.ElementAt((int)(xValue.Subtract(log.StartTime).TotalMilliseconds / 20));
                    int labelNum = 0;
                    double totalA = 0;
                    for (int seriesNum = 0; seriesNum < chartMain.Series.Count; seriesNum++)
                    {
                        if (chartMain.Series[seriesNum].Enabled)
                        {
                            Label seriesLabel = new Label();
                            seriesLabel.Text = varToLabel(chartMain.Series[seriesNum].Name, en);
                            seriesLabel.Visible = true;
                            seriesLabel.AutoSize = true;
                            seriesLabel.Location = new Point(7, (24 * labelNum++) + 7);
                            labelInfoPanel.Controls.Add(seriesLabel);
                            //Add pdp series to total current
                            if (chartMain.Series[seriesNum].Name.StartsWith("PDP")) totalA += en.getPDPChannel(int.Parse(chartMain.Series[seriesNum].Name.Split(' ')[1]));
                        }
                    }

                    Label totalAL = new Label();
                    totalAL.Text = "Total Current: " + totalA + "A";
                    totalAL.Visible = true;
                    totalAL.AutoSize = true;
                    totalAL.Location = new Point(7, (24 * labelNum++) + 7);
                    labelInfoPanel.Controls.Add(totalAL);

                }
            }

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
            return name+": idk??!?!?!?";
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
                            if (header.Contains("Robot") || header.Contains("DS") || header.Contains("Brownout"))  csv.Append(BoolNameToValue(header, en)+",");
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
                            if (s.Contains("Robot") || s.Contains("DS") || s.Contains("Brownout")) clipBoardText.Append(BoolNameToValue(s, en)+ "\t");
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
            if (!Double.IsNaN(e.NewPosition) && log != null)
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

        //listView
        #region
        //Folder path for the files in the list view
        string listviewFolderPath;
        
        //Adds items to listview (info will be added when item is in view)
        void addLogFilesToViewer(string path)
        {
            listViewDSLOGFolder.Items.Clear();
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
            lastIndexSelected = -1;
            timerScrollToBottom.Start();
        }

      
        //last Index Selected so we don't load the file again
        int lastIndexSelected = 0;

        //import file when selection changes
        private void listViewDSLOGFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDSLOGFolder.SelectedItems.Count != 0)
            {
                if (listViewDSLOGFolder.SelectedItems[0].Index != lastIndexSelected)
                {
                    lastIndexSelected = listViewDSLOGFolder.SelectedItems[0].Index;
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
            int topIndex = listViewDSLOGFolder.TopItem.Index;
            if (lastTop != topIndex)
            {
                for (int inx = topIndex; inx < topIndex + listViewDSLOGFolder.Height / 18; inx++)
                    if (inx < listViewDSLOGFolder.Items.Count)
                    {
                        //Check if we already added info
                        if (listViewDSLOGFolder.Items[inx].SubItems.Count < 2)
                        {
                            try
                            {
                                if (File.Exists(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dsevents")) 
                                {
                                    FileInfo file = new FileInfo(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dsevents");
                                    string txtF = File.ReadAllText(file.FullName);

                                    DateTime sTime;
                                    using (BinaryReader2 reader = new BinaryReader2(File.Open(file.FullName, FileMode.Open)))
                                    {
                                        reader.ReadInt32();

                                        sTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                                        listViewDSLOGFolder.Items[inx].SubItems.Add(sTime.ToString("MMM dd, yy hh:mm:ss tt"));
                                        reader.Close();
                                    }

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
                                }
                                else
                                {
                                    DateTime sTime;
                                    using (BinaryReader2 reader = new BinaryReader2(File.Open(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog", FileMode.Open)))
                                    {
                                        reader.ReadInt32();

                                        sTime = FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
                                        listViewDSLOGFolder.Items[inx].SubItems.Add(sTime.ToString("MMM dd, yy hh:mm:ss tt"));
                                        reader.Close();
                                    }
                                    listViewDSLOGFolder.Items[inx].SubItems.Add("" + ((new FileInfo(listviewFolderPath + "\\" + listViewDSLOGFolder.Items[inx].Text + ".dslog").Length - 19) / 35) / 50);
                                    listViewDSLOGFolder.Items[inx].SubItems.Add("NO");
                                    TimeSpan sub = DateTime.Now.Subtract(sTime);
                                    listViewDSLOGFolder.Items[inx].SubItems.Add(sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m");
                                    listViewDSLOGFolder.Items[inx].SubItems.Add("DSEVENT");
                                    
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    }
                lastTop = topIndex;
            }
        }

        #endregion

        //Import
        #region
        //Imports log from path
        void importLog(string path)
        {
            clearGraph();
            chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            log = null;
            try
            {
                log = new DSLOGReader(path);
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
                                chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), en.LostPackets * 100);
                            }
                            else
                            {
                                chartMain.Series["Lost Packets"].Points.AddXY(en.Time.ToOADate(), 0);
                            }
                            packetnum++;
                        }
                        if (log.Entries.ElementAt(w - 1).Voltage != en.Voltage || log.Entries.ElementAt(w + 1).Voltage != en.Voltage)
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

                    if (en.DSDisabled) chartMain.Series["DS Disabled"].Points.AddXY(en.Time.ToOADate(), 16);
                    if (en.DSAuto) chartMain.Series["DS Auto"].Points.AddXY(en.Time.ToOADate(), 16);
                    if (en.DSTele) chartMain.Series["DS Tele"].Points.AddXY(en.Time.ToOADate(), 16);

                    if (en.RobotDisabled) chartMain.Series["Robot Disabled"].Points.AddXY(en.Time.ToOADate(), 16.8);
                    if (en.RobotAuto) chartMain.Series["Robot Auto"].Points.AddXY(en.Time.ToOADate(), 16.4);
                    if (en.RobotTele) chartMain.Series["Robot Tele"].Points.AddXY(en.Time.ToOADate(), 16.4);

                    if (en.Brownout) chartMain.Series["Brownout"].Points.AddXY(en.Time.ToOADate(), 15.6);




                }
                chartMain.ChartAreas[0].AxisX.Maximum = log.Entries.Last().Time.ToOADate();
                menuStrip1.Items[menuStrip1.Items.Count - 1].Text = "Time Scale " + getTotalSecoundsInView() + " Sec";
                setColoumnLabelNumber();
                tabPage4.Enabled = true;
                chartMain.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Can't import file");
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
            epoch = epoch.AddHours(-4);

            return epoch.AddSeconds(((double)ummm / UInt64.MaxValue));
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/orangelight/dslog-reader/blob/master/Help.md");
        }

        


    }
}
