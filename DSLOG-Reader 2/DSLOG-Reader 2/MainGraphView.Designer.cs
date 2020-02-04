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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GraphCorsorLine = new System.Windows.Forms.Timer(this.components);
            this.labelFileInfo = new System.Windows.Forms.Label();
            this.timerStream = new System.Windows.Forms.Timer(this.components);
            this.buttonAutoScroll = new System.Windows.Forms.Button();
            this.buttonFindMatch = new System.Windows.Forms.Button();
            this.buttonResetZoom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea2.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea2.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.MajorGrid.Interval = 0D;
            chartArea2.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MaximumAutoSize = 100F;
            chartArea2.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.AxisX.ScrollBar.BackColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.ScrollBar.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea2.AxisY.LabelStyle.Interval = 1D;
            chartArea2.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(66)))));
            chartArea2.AxisY.MajorGrid.Interval = 1D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea2.AxisY.MajorTickMark.Interval = 1D;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGoldenrod;
            chartArea2.AxisY.Maximum = 17D;
            chartArea2.AxisY.MaximumAutoSize = 100F;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.Title = "Battery Voltage V";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea2.AxisY2.Interval = 10D;
            chartArea2.AxisY2.IsLabelAutoFit = false;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorTickMark.Interval = 10D;
            chartArea2.AxisY2.Maximum = 120D;
            chartArea2.AxisY2.Minimum = 0D;
            chartArea2.AxisY2.Title = "Latency ms, Packet Loss %, roboRIO CPU %, CAN %, PDP Currents A";
            chartArea2.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.CursorX.Interval = 20D;
            chartArea2.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Milliseconds;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorX.LineWidth = 2;
            chartArea2.Name = "ChartArea";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 100F;
            chartArea2.Position.Width = 100F;
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(0, 22);
            this.chart.Name = "chart";
            series2.ChartArea = "ChartArea";
            series2.Name = "Series1";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(1104, 553);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart";
            this.chart.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.Chart_CursorPositionChanged);
            this.chart.AxisViewChanging += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart_AxisViewChanging);
            this.chart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.Chart_AxisViewChanged);
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
            // MainGraphView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
