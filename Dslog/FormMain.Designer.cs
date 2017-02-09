namespace Dslog
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControlChartCheckBox = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInfoPanel = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            this.labelColumns = new System.Windows.Forms.Label();
            this.checkBoxTotalCurrent = new System.Windows.Forms.CheckBox();
            this.comboBoxExport = new System.Windows.Forms.ComboBox();
            this.listViewDSLOGFolder = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSeconds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMatchNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTimeAgo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEventName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlProbeExport = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.timerScrollToBottom = new System.Windows.Forms.Timer(this.components);
            this.logUpdate = new System.Windows.Forms.Timer(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLogFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFlileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFilesPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlChart = new System.Windows.Forms.TabControl();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.listViewEvents = new System.Windows.Forms.ListView();
            this.columnHeaderDSTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEventText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refreshPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.tabControlChartCheckBox.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlProbeExport.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControlChart.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartMain
            // 
            this.chartMain.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.DimGray;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.LabelStyle.Interval = 1D;
            chartArea1.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(66)))));
            chartArea1.AxisY.MajorGrid.Interval = 1D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.MajorTickMark.Interval = 1D;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.Maximum = 17D;
            chartArea1.AxisY.MaximumAutoSize = 100F;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Battery Voltage";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY2.Interval = 10D;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorGrid.Interval = 10D;
            chartArea1.AxisY2.MajorTickMark.Interval = 10D;
            chartArea1.AxisY2.Maximum = 120D;
            chartArea1.AxisY2.Minimum = 0D;
            chartArea1.AxisY2.Title = "Latency ms, Packet Loss %, roboRIO CPU %, CAN %, PDP Currents A";
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.CursorX.Interval = 20D;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea1);
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Location = new System.Drawing.Point(3, 3);
            this.chartMain.Margin = new System.Windows.Forms.Padding(2);
            this.chartMain.Name = "chartMain";
            this.chartMain.Size = new System.Drawing.Size(722, 498);
            this.chartMain.TabIndex = 2;
            this.chartMain.Text = "chart1";
            this.chartMain.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.chartMain_CursorPositionChanged);
            this.chartMain.AxisViewChanging += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chartMain_AxisViewChanging);
            this.chartMain.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chartMain_AxisViewChanged);
            // 
            // tabControlChartCheckBox
            // 
            this.tabControlChartCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlChartCheckBox.Controls.Add(this.tabPage1);
            this.tabControlChartCheckBox.Controls.Add(this.tabPage2);
            this.tabControlChartCheckBox.Location = new System.Drawing.Point(173, 6);
            this.tabControlChartCheckBox.Name = "tabControlChartCheckBox";
            this.tabControlChartCheckBox.SelectedIndex = 0;
            this.tabControlChartCheckBox.Size = new System.Drawing.Size(159, 493);
            this.tabControlChartCheckBox.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.checkBox7);
            this.tabPage1.Controls.Add(this.checkBox6);
            this.tabPage1.Controls.Add(this.checkBox5);
            this.tabPage1.Controls.Add(this.checkBox4);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.checkBox2);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(151, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Groups";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(7, 151);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(114, 17);
            this.checkBox7.TabIndex = 6;
            this.checkBox7.Text = "Power (12-15) 40A";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(7, 127);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(108, 17);
            this.checkBox6.TabIndex = 5;
            this.checkBox6.Text = "Power (8-11) 30A";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(7, 103);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(102, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "Power (4-7) 30A";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(7, 79);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(102, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "Power (0-3) 40A";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(7, 55);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(60, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Comms";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(7, 31);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(52, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Basic";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(7, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(85, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Robot Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.groupCheckBox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(151, 467);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plots";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInfoPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 461);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // labelInfoPanel
            // 
            this.labelInfoPanel.AutoScroll = true;
            this.labelInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfoPanel.Location = new System.Drawing.Point(3, 16);
            this.labelInfoPanel.Name = "labelInfoPanel";
            this.labelInfoPanel.Size = new System.Drawing.Size(141, 442);
            this.labelInfoPanel.TabIndex = 0;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(6, 69);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(141, 22);
            this.buttonExport.TabIndex = 6;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(3, 26);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(89, 13);
            this.labelColumns.TabIndex = 11;
            this.labelColumns.Text = "Total  Columns: 0";
            // 
            // checkBoxTotalCurrent
            // 
            this.checkBoxTotalCurrent.AutoSize = true;
            this.checkBoxTotalCurrent.Location = new System.Drawing.Point(6, 6);
            this.checkBoxTotalCurrent.Name = "checkBoxTotalCurrent";
            this.checkBoxTotalCurrent.Size = new System.Drawing.Size(109, 17);
            this.checkBoxTotalCurrent.TabIndex = 7;
            this.checkBoxTotalCurrent.Text = "Add Total Current";
            this.checkBoxTotalCurrent.UseVisualStyleBackColor = true;
            this.checkBoxTotalCurrent.CheckedChanged += new System.EventHandler(this.checkBoxTotalCurrent_CheckedChanged);
            // 
            // comboBoxExport
            // 
            this.comboBoxExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExport.FormattingEnabled = true;
            this.comboBoxExport.Items.AddRange(new object[] {
            "CSV",
            "Clipboard",
            "PNG"});
            this.comboBoxExport.Location = new System.Drawing.Point(6, 42);
            this.comboBoxExport.Name = "comboBoxExport";
            this.comboBoxExport.Size = new System.Drawing.Size(141, 21);
            this.comboBoxExport.TabIndex = 8;
            // 
            // listViewDSLOGFolder
            // 
            this.listViewDSLOGFolder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnTime,
            this.columnSeconds,
            this.columnMatchNum,
            this.columnTimeAgo,
            this.columnEventName});
            this.listViewDSLOGFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDSLOGFolder.FullRowSelect = true;
            this.listViewDSLOGFolder.GridLines = true;
            this.listViewDSLOGFolder.HideSelection = false;
            this.listViewDSLOGFolder.Location = new System.Drawing.Point(3, 3);
            this.listViewDSLOGFolder.MultiSelect = false;
            this.listViewDSLOGFolder.Name = "listViewDSLOGFolder";
            this.listViewDSLOGFolder.Size = new System.Drawing.Size(332, 498);
            this.listViewDSLOGFolder.TabIndex = 9;
            this.listViewDSLOGFolder.UseCompatibleStateImageBehavior = false;
            this.listViewDSLOGFolder.View = System.Windows.Forms.View.Details;
            this.listViewDSLOGFolder.SelectedIndexChanged += new System.EventHandler(this.listViewDSLOGFolder_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.DisplayIndex = 5;
            this.columnName.Text = "Name";
            this.columnName.Width = 170;
            // 
            // columnTime
            // 
            this.columnTime.DisplayIndex = 0;
            this.columnTime.Text = "Time";
            this.columnTime.Width = 120;
            // 
            // columnSeconds
            // 
            this.columnSeconds.DisplayIndex = 1;
            this.columnSeconds.Text = "Seconds";
            this.columnSeconds.Width = 55;
            // 
            // columnMatchNum
            // 
            this.columnMatchNum.DisplayIndex = 2;
            this.columnMatchNum.Text = "Match #";
            this.columnMatchNum.Width = 53;
            // 
            // columnTimeAgo
            // 
            this.columnTimeAgo.DisplayIndex = 3;
            this.columnTimeAgo.Text = "Time Ago";
            this.columnTimeAgo.Width = 100;
            // 
            // columnEventName
            // 
            this.columnEventName.DisplayIndex = 4;
            this.columnEventName.Text = "Event";
            // 
            // tabControlProbeExport
            // 
            this.tabControlProbeExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlProbeExport.Controls.Add(this.tabPage3);
            this.tabControlProbeExport.Controls.Add(this.tabPage4);
            this.tabControlProbeExport.Location = new System.Drawing.Point(6, 6);
            this.tabControlProbeExport.Name = "tabControlProbeExport";
            this.tabControlProbeExport.SelectedIndex = 0;
            this.tabControlProbeExport.Size = new System.Drawing.Size(161, 493);
            this.tabControlProbeExport.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(153, 467);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Probe";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonExport);
            this.tabPage4.Controls.Add(this.labelColumns);
            this.tabPage4.Controls.Add(this.checkBoxTotalCurrent);
            this.tabPage4.Controls.Add(this.comboBoxExport);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(153, 467);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Export";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlMain.Controls.Add(this.tabPage5);
            this.tabControlMain.Controls.Add(this.tabPage6);
            this.tabControlMain.Location = new System.Drawing.Point(12, 25);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(346, 530);
            this.tabControlMain.TabIndex = 12;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listViewDSLOGFolder);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(338, 504);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Log Files";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tabControlProbeExport);
            this.tabPage6.Controls.Add(this.tabControlChartCheckBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(338, 504);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Graph";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // timerScrollToBottom
            // 
            this.timerScrollToBottom.Tick += new System.EventHandler(this.timerScrollToBottom_Tick);
            // 
            // logUpdate
            // 
            this.logUpdate.Enabled = true;
            this.logUpdate.Interval = 25;
            this.logUpdate.Tick += new System.EventHandler(this.logUpdate_Tick);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadLogToolStripMenuItem,
            this.changeLogFilePathToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadLogToolStripMenuItem
            // 
            this.loadLogToolStripMenuItem.Name = "loadLogToolStripMenuItem";
            this.loadLogToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.loadLogToolStripMenuItem.Text = "Load Log";
            this.loadLogToolStripMenuItem.Click += new System.EventHandler(this.loadLogToolStripMenuItem_Click);
            // 
            // changeLogFilePathToolStripMenuItem
            // 
            this.changeLogFilePathToolStripMenuItem.Name = "changeLogFilePathToolStripMenuItem";
            this.changeLogFilePathToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.changeLogFilePathToolStripMenuItem.Text = "Log Files Path";
            this.changeLogFilePathToolStripMenuItem.Click += new System.EventHandler(this.changeLogFilePathToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetZoomToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.resetZoomToolStripMenuItem.Text = "Reset Zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // loadFlileToolStripMenuItem
            // 
            this.loadFlileToolStripMenuItem.Name = "loadFlileToolStripMenuItem";
            this.loadFlileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadFlileToolStripMenuItem.Text = "Load Flile";
            // 
            // logFilesPathToolStripMenuItem
            // 
            this.logFilesPathToolStripMenuItem.Name = "logFilesPathToolStripMenuItem";
            this.logFilesPathToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logFilesPathToolStripMenuItem.Text = "Log Files Path";
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // resetZoomToolStripMenuItem1
            // 
            this.resetZoomToolStripMenuItem1.Name = "resetZoomToolStripMenuItem1";
            this.resetZoomToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.resetZoomToolStripMenuItem1.Text = "Reset Zoom";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // viewHelpToolStripMenuItem1
            // 
            this.viewHelpToolStripMenuItem1.Name = "viewHelpToolStripMenuItem1";
            this.viewHelpToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.viewHelpToolStripMenuItem1.Text = "View Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem2,
            this.viewToolStripMenuItem2,
            this.helpToolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1108, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem2
            // 
            this.fileToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadLogToolStripMenuItem1,
            this.fileToolStripMenuItem3,
            this.refreshPathToolStripMenuItem});
            this.fileToolStripMenuItem2.Name = "fileToolStripMenuItem2";
            this.fileToolStripMenuItem2.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem2.Text = "File";
            // 
            // loadLogToolStripMenuItem1
            // 
            this.loadLogToolStripMenuItem1.Name = "loadLogToolStripMenuItem1";
            this.loadLogToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.loadLogToolStripMenuItem1.Text = "Load Log";
            this.loadLogToolStripMenuItem1.Click += new System.EventHandler(this.loadLogToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem3
            // 
            this.fileToolStripMenuItem3.Name = "fileToolStripMenuItem3";
            this.fileToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.fileToolStripMenuItem3.Text = "Log Files Path";
            this.fileToolStripMenuItem3.Click += new System.EventHandler(this.changeLogFilePathToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem2
            // 
            this.viewToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetZoomToolStripMenuItem2});
            this.viewToolStripMenuItem2.Name = "viewToolStripMenuItem2";
            this.viewToolStripMenuItem2.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem2.Text = "View";
            // 
            // resetZoomToolStripMenuItem2
            // 
            this.resetZoomToolStripMenuItem2.Name = "resetZoomToolStripMenuItem2";
            this.resetZoomToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.resetZoomToolStripMenuItem2.Text = "Reset Zoom";
            this.resetZoomToolStripMenuItem2.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem2
            // 
            this.helpToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem2,
            this.aboutToolStripMenuItem2});
            this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
            this.helpToolStripMenuItem2.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem2.Text = "Help";
            // 
            // viewHelpToolStripMenuItem2
            // 
            this.viewHelpToolStripMenuItem2.Name = "viewHelpToolStripMenuItem2";
            this.viewHelpToolStripMenuItem2.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem2.Text = "View Help";
            this.viewHelpToolStripMenuItem2.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem2
            // 
            this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
            this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem2.Text = "About";
            this.aboutToolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControlChart
            // 
            this.tabControlChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlChart.Controls.Add(this.tabPageChart);
            this.tabControlChart.Controls.Add(this.tabPageMessages);
            this.tabControlChart.Location = new System.Drawing.Point(360, 25);
            this.tabControlChart.Name = "tabControlChart";
            this.tabControlChart.SelectedIndex = 0;
            this.tabControlChart.Size = new System.Drawing.Size(736, 530);
            this.tabControlChart.TabIndex = 14;
            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.chartMain);
            this.tabPageChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChart.Size = new System.Drawing.Size(728, 504);
            this.tabPageChart.TabIndex = 0;
            this.tabPageChart.Text = "Graph";
            this.tabPageChart.UseVisualStyleBackColor = true;
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.listViewEvents);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(728, 504);
            this.tabPageMessages.TabIndex = 1;
            this.tabPageMessages.Text = "Events";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // listViewEvents
            // 
            this.listViewEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDSTime,
            this.columnHeaderEventText});
            this.listViewEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEvents.FullRowSelect = true;
            this.listViewEvents.GridLines = true;
            this.listViewEvents.Location = new System.Drawing.Point(3, 3);
            this.listViewEvents.Name = "listViewEvents";
            this.listViewEvents.OwnerDraw = true;
            this.listViewEvents.Size = new System.Drawing.Size(722, 498);
            this.listViewEvents.TabIndex = 0;
            this.listViewEvents.UseCompatibleStateImageBehavior = false;
            this.listViewEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDSTime
            // 
            this.columnHeaderDSTime.Text = "DS Time";
            this.columnHeaderDSTime.Width = 90;
            // 
            // columnHeaderEventText
            // 
            this.columnHeaderEventText.Text = "Event Message Text";
            this.columnHeaderEventText.Width = 700;
            // 
            // refreshPathToolStripMenuItem
            // 
            this.refreshPathToolStripMenuItem.Name = "refreshPathToolStripMenuItem";
            this.refreshPathToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshPathToolStripMenuItem.Text = "Refresh Path";
            this.refreshPathToolStripMenuItem.Click += new System.EventHandler(this.refreshPathToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 567);
            this.Controls.Add(this.tabControlChart);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(770, 260);
            this.Name = "FormMain";
            this.Text = "DSLOG Reader";
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.tabControlChartCheckBox.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControlProbeExport.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlChart.ResumeLayout(false);
            this.tabPageChart.ResumeLayout(false);
            this.tabPageMessages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.TabControl tabControlChartCheckBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel labelInfoPanel;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ComboBox comboBoxExport;
        private System.Windows.Forms.CheckBox checkBoxTotalCurrent;
        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.ListView listViewDSLOGFolder;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ColumnHeader columnSeconds;
        private System.Windows.Forms.ColumnHeader columnMatchNum;
        private System.Windows.Forms.ColumnHeader columnTimeAgo;
        private System.Windows.Forms.TabControl tabControlProbeExport;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Timer timerScrollToBottom;
        private System.Windows.Forms.Timer logUpdate;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLogFilePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnEventName;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadFlileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFilesPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem loadLogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem2;
        private System.Windows.Forms.TabControl tabControlChart;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.ListView listViewEvents;
        private System.Windows.Forms.ColumnHeader columnHeaderDSTime;
        private System.Windows.Forms.ColumnHeader columnHeaderEventText;
        private System.Windows.Forms.ToolStripMenuItem refreshPathToolStripMenuItem;
    }
}

