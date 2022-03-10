namespace DSLOG_Reader_2
{
    partial class MainGraphView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GraphCorsorLine = new System.Windows.Forms.Timer(this.components);
            this.labelFileInfo = new System.Windows.Forms.Label();
            this.timerStream = new System.Windows.Forms.Timer(this.components);
            this.buttonResetZoom = new System.Windows.Forms.Button();
            this.buttonFindMatch = new System.Windows.Forms.Button();
            this.buttonAutoScroll = new System.Windows.Forms.Button();
            this.buttonMatchTime = new System.Windows.Forms.Button();
            this.labelTimeInView = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea1.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.DimGray;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.LabelStyle.Interval = 1D;
            chartArea1.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.MajorGrid.Interval = 1D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.MajorTickMark.Interval = 1D;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.Maximum = 17D;
            chartArea1.AxisY.MaximumAutoSize = 100F;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Battery Voltage V";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY2.Interval = 10D;
            chartArea1.AxisY2.IsLabelAutoFit = false;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Interval = 10D;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY2.Maximum = 120D;
            chartArea1.AxisY2.Minimum = 0D;
            chartArea1.AxisY2.Title = "Latency ms, Packet Loss %, roboRIO CPU %, CAN %, PDP A (Total PDP)";
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY2.ScrollBar.BackColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            chartArea1.AxisY2.ScrollBar.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.CursorX.Interval = 20D;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineWidth = 2;
            chartArea1.CursorY.AxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            chartArea1.Name = "ChartArea";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(0, 22);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1104, 553);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart";
            this.chart.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.Chart_CursorPositionChanged);
            this.chart.AxisViewChanging += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart_AxisViewChanging);
            this.chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart_AxisViewChanged);
            this.chart.AxisScrollBarClicked += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ScrollBarEventArgs>(this.chart_AxisScrollBarClicked);
            this.chart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chart_KeyDown);
            this.chart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.chart_KeyUp);
            this.chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseMove);
            // 
            // GraphCorsorLine
            // 
            this.GraphCorsorLine.Interval = 500;
            this.GraphCorsorLine.Tick += new System.EventHandler(this.GraphCorsorLine_Tick);
            // 
            // labelFileInfo
            // 
            this.labelFileInfo.AutoSize = true;
            this.labelFileInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileInfo.Location = new System.Drawing.Point(3, 3);
            this.labelFileInfo.Name = "labelFileInfo";
            this.labelFileInfo.Size = new System.Drawing.Size(0, 13);
            this.labelFileInfo.TabIndex = 1;
            // 
            // timerStream
            // 
            this.timerStream.Interval = 200;
            this.timerStream.Tick += new System.EventHandler(this.timerStream_Tick);
            // 
            // buttonResetZoom
            // 
            this.buttonResetZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetZoom.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.Zoom_16x;
            this.buttonResetZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonResetZoom.Location = new System.Drawing.Point(1037, -1);
            this.buttonResetZoom.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetZoom.Name = "buttonResetZoom";
            this.buttonResetZoom.Size = new System.Drawing.Size(23, 23);
            this.buttonResetZoom.TabIndex = 4;
            this.buttonResetZoom.UseVisualStyleBackColor = true;
            this.buttonResetZoom.Click += new System.EventHandler(this.buttonResetZoom_Click);
            // 
            // buttonFindMatch
            // 
            this.buttonFindMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindMatch.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.ZoomToWidth_16x;
            this.buttonFindMatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFindMatch.FlatAppearance.BorderSize = 0;
            this.buttonFindMatch.Location = new System.Drawing.Point(1059, -1);
            this.buttonFindMatch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFindMatch.Name = "buttonFindMatch";
            this.buttonFindMatch.Size = new System.Drawing.Size(23, 23);
            this.buttonFindMatch.TabIndex = 3;
            this.buttonFindMatch.UseVisualStyleBackColor = true;
            this.buttonFindMatch.Click += new System.EventHandler(this.buttonFindMatch_Click);
            // 
            // buttonAutoScroll
            // 
            this.buttonAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAutoScroll.BackColor = System.Drawing.Color.Red;
            this.buttonAutoScroll.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.FindNext_16x1;
            this.buttonAutoScroll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAutoScroll.FlatAppearance.BorderSize = 0;
            this.buttonAutoScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAutoScroll.Location = new System.Drawing.Point(1081, -1);
            this.buttonAutoScroll.Name = "buttonAutoScroll";
            this.buttonAutoScroll.Size = new System.Drawing.Size(23, 23);
            this.buttonAutoScroll.TabIndex = 2;
            this.buttonAutoScroll.UseVisualStyleBackColor = false;
            this.buttonAutoScroll.Click += new System.EventHandler(this.buttonAutoScroll_Click);
            // 
            // buttonMatchTime
            // 
            this.buttonMatchTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMatchTime.BackColor = System.Drawing.Color.Gray;
            this.buttonMatchTime.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.Time_16x;
            this.buttonMatchTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMatchTime.FlatAppearance.BorderSize = 0;
            this.buttonMatchTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMatchTime.Location = new System.Drawing.Point(1014, -1);
            this.buttonMatchTime.Name = "buttonMatchTime";
            this.buttonMatchTime.Size = new System.Drawing.Size(23, 23);
            this.buttonMatchTime.TabIndex = 6;
            this.buttonMatchTime.UseVisualStyleBackColor = false;
            this.buttonMatchTime.Click += new System.EventHandler(this.buttonMatchTime_Click);
            // 
            // labelTimeInView
            // 
            this.labelTimeInView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeInView.Location = new System.Drawing.Point(856, 3);
            this.labelTimeInView.Name = "labelTimeInView";
            this.labelTimeInView.Size = new System.Drawing.Size(152, 15);
            this.labelTimeInView.TabIndex = 7;
            this.labelTimeInView.Text = "Time in View: 0.00";
            this.labelTimeInView.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainGraphView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.labelTimeInView);
            this.Controls.Add(this.buttonMatchTime);
            this.Controls.Add(this.buttonResetZoom);
            this.Controls.Add(this.buttonFindMatch);
            this.Controls.Add(this.buttonAutoScroll);
            this.Controls.Add(this.labelFileInfo);
            this.Controls.Add(this.chart);
            this.Name = "MainGraphView";
            this.Size = new System.Drawing.Size(1104, 575);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Timer GraphCorsorLine;
        private System.Windows.Forms.Label labelFileInfo;
        private System.Windows.Forms.Timer timerStream;
        private System.Windows.Forms.Button buttonAutoScroll;
        private System.Windows.Forms.Button buttonFindMatch;
        private System.Windows.Forms.Button buttonResetZoom;
        private System.Windows.Forms.Button buttonMatchTime;
        private System.Windows.Forms.Label labelTimeInView;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
