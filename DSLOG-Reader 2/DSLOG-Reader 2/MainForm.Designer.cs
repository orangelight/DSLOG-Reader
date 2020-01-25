﻿namespace DSLOG_Reader_2
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.richTextBoxGraph = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonCompMode = new System.Windows.Forms.Button();
            this.timerCompMode = new System.Windows.Forms.Timer(this.components);
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.mainGraphView = new DSLOG_Reader_2.MainGraphView();
            this.eventsView1 = new DSLOG_Reader_2.EventsView();
            this.fileListView = new DSLOG_Reader_2.FileListView();
            this.probeView1 = new DSLOG_Reader_2.ProbeView();
            this.exportView1 = new DSLOG_Reader_2.ExportView();
            this.seriesView = new DSLOG_Reader_2.SeriesView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(472, 690);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fileListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(464, 661);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl3);
            this.tabPage2.Controls.Add(this.richTextBoxGraph);
            this.tabPage2.Controls.Add(this.seriesView);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(464, 661);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graph";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Controls.Add(this.tabPage6);
            this.tabControl3.Location = new System.Drawing.Point(4, 4);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(232, 516);
            this.tabControl3.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.probeView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(224, 487);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Probe";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.exportView1);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage6.Size = new System.Drawing.Size(224, 487);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Export";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // richTextBoxGraph
            // 
            this.richTextBoxGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxGraph.Location = new System.Drawing.Point(4, 519);
            this.richTextBoxGraph.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxGraph.Name = "richTextBoxGraph";
            this.richTextBoxGraph.Size = new System.Drawing.Size(448, 134);
            this.richTextBoxGraph.TabIndex = 2;
            this.richTextBoxGraph.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(467, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1011, 690);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.mainGraphView);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1003, 661);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Graph";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.eventsView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1003, 661);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Events";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.Image = global::DSLOG_Reader_2.Properties.Resources.StatusHelp_16xMD;
            this.buttonHelp.Location = new System.Drawing.Point(1445, -1);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(31, 26);
            this.buttonHelp.TabIndex = 0;
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // buttonCompMode
            // 
            this.buttonCompMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCompMode.Location = new System.Drawing.Point(1221, -1);
            this.buttonCompMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCompMode.Name = "buttonCompMode";
            this.buttonCompMode.Size = new System.Drawing.Size(223, 26);
            this.buttonCompMode.TabIndex = 1;
            this.buttonCompMode.Text = "Switch to Competition Mode";
            this.buttonCompMode.UseVisualStyleBackColor = true;
            this.buttonCompMode.Click += new System.EventHandler(this.ButtonCompMode_Click);
            // 
            // timerCompMode
            // 
            this.timerCompMode.Enabled = true;
            this.timerCompMode.Interval = 1500;
            this.timerCompMode.Tick += new System.EventHandler(this.TimerCompMode_Tick);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(944, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(276, 23);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // mainGraphView
            // 
            this.mainGraphView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGraphView.Location = new System.Drawing.Point(4, 4);
            this.mainGraphView.Margin = new System.Windows.Forms.Padding(5);
            this.mainGraphView.Name = "mainGraphView";
            this.mainGraphView.Size = new System.Drawing.Size(995, 653);
            this.mainGraphView.TabIndex = 0;
            // 
            // eventsView1
            // 
            this.eventsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsView1.GraphView = null;
            this.eventsView1.Location = new System.Drawing.Point(4, 4);
            this.eventsView1.Name = "eventsView1";
            this.eventsView1.Size = new System.Drawing.Size(995, 653);
            this.eventsView1.TabIndex = 0;
            // 
            // fileListView
            // 
            this.fileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListView.EventView = null;
            this.fileListView.Location = new System.Drawing.Point(4, 4);
            this.fileListView.MainChart = null;
            this.fileListView.Margin = new System.Windows.Forms.Padding(5);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(456, 653);
            this.fileListView.TabIndex = 0;
            // 
            // probeView1
            // 
            this.probeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.probeView1.Location = new System.Drawing.Point(4, 4);
            this.probeView1.Margin = new System.Windows.Forms.Padding(5);
            this.probeView1.Name = "probeView1";
            this.probeView1.Size = new System.Drawing.Size(216, 479);
            this.probeView1.TabIndex = 0;
            // 
            // exportView1
            // 
            this.exportView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.exportView1.Location = new System.Drawing.Point(4, 4);
            this.exportView1.Margin = new System.Windows.Forms.Padding(5);
            this.exportView1.Name = "exportView1";
            this.exportView1.Size = new System.Drawing.Size(219, 473);
            this.exportView1.TabIndex = 0;
            // 
            // seriesView
            // 
            this.seriesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seriesView.Location = new System.Drawing.Point(236, 4);
            this.seriesView.MainChart = null;
            this.seriesView.Margin = new System.Windows.Forms.Padding(5);
            this.seriesView.Name = "seriesView";
            this.seriesView.Size = new System.Drawing.Size(216, 516);
            this.seriesView.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1477, 690);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonCompMode);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1021, 307);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FileListView fileListView;
        private SeriesView seriesView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBoxGraph;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button buttonHelp;
        private MainGraphView mainGraphView;
        private ProbeView probeView1;
        private ExportView exportView1;
        private System.Windows.Forms.Button buttonCompMode;
        private System.Windows.Forms.Timer timerCompMode;
        private EventsView eventsView1;
        private System.Windows.Forms.TextBox textBoxSearch;
    }
}