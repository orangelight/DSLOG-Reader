namespace DSLOG_Reader_2
{
    partial class CompetitionView
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.backgroundWorkerReadMatches = new System.ComponentModel.BackgroundWorker();
            this.progressBarEvents = new System.Windows.Forms.ProgressBar();
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
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
            chartArea1.AxisY.LabelStyle.Interval = 1D;
            chartArea1.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.Enabled = false;
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
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Interval = 10D;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY2.Maximum = 120D;
            chartArea1.AxisY2.Minimum = -0.1D;
            chartArea1.AxisY2.Title = "Latency ms, Packet Loss %, roboRIO CPU %, CAN %, PDP A (Total PDP)";
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(0, 22);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(707, 460);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "Auto & Tele",
            "Auto",
            "Tele"});
            this.comboBoxMode.Location = new System.Drawing.Point(0, 0);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMode.TabIndex = 2;
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // backgroundWorkerReadMatches
            // 
            this.backgroundWorkerReadMatches.WorkerReportsProgress = true;
            this.backgroundWorkerReadMatches.WorkerSupportsCancellation = true;
            this.backgroundWorkerReadMatches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReadMatches_DoWork);
            this.backgroundWorkerReadMatches.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerReadMatches_ProgressChanged);
            this.backgroundWorkerReadMatches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerReadMatches_RunWorkerCompleted);
            // 
            // progressBarEvents
            // 
            this.progressBarEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarEvents.Location = new System.Drawing.Point(526, 0);
            this.progressBarEvents.Name = "progressBarEvents";
            this.progressBarEvents.Size = new System.Drawing.Size(181, 22);
            this.progressBarEvents.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarEvents.TabIndex = 3;
            // 
            // CompetitionView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.progressBarEvents);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.chart);
            this.Name = "CompetitionView";
            this.Size = new System.Drawing.Size(707, 482);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReadMatches;
        private System.Windows.Forms.ProgressBar progressBarEvents;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
