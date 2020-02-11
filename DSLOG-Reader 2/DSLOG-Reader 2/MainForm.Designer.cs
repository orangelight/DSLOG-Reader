namespace DSLOG_Reader_2
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
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fileListView = new DSLOG_Reader_2.FileListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.probeView1 = new DSLOG_Reader_2.ProbeView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.exportView1 = new DSLOG_Reader_2.ExportView();
            this.richTextBoxGraph = new System.Windows.Forms.RichTextBox();
            this.seriesView = new DSLOG_Reader_2.SeriesView();
            this.tabControlRight = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.mainGraphView = new DSLOG_Reader_2.MainGraphView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.eventsView1 = new DSLOG_Reader_2.EventsView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.competitionView1 = new DSLOG_Reader_2.CompetitionView();
            this.timerCompMode = new System.Windows.Forms.Timer(this.components);
            this.buttonHelp = new System.Windows.Forms.Button();
            this.tabControlLeft.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabControlRight.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlLeft
            // 
            this.tabControlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlLeft.Controls.Add(this.tabPage1);
            this.tabControlLeft.Controls.Add(this.tabPage2);
            this.tabControlLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.SelectedIndex = 0;
            this.tabControlLeft.Size = new System.Drawing.Size(354, 561);
            this.tabControlLeft.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fileListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(346, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fileListView
            // 
            this.fileListView.CompView = null;
            this.fileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListView.EventView = null;
            this.fileListView.Location = new System.Drawing.Point(3, 3);
            this.fileListView.MainChart = null;
            this.fileListView.Margin = new System.Windows.Forms.Padding(4);
            this.fileListView.Name = "fileListView";
            this.fileListView.SeriesViewObserving = null;
            this.fileListView.Size = new System.Drawing.Size(340, 529);
            this.fileListView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl3);
            this.tabPage2.Controls.Add(this.richTextBoxGraph);
            this.tabPage2.Controls.Add(this.seriesView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(346, 535);
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
            this.tabControl3.Location = new System.Drawing.Point(3, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(174, 419);
            this.tabControl3.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.probeView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(166, 393);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Probe";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // probeView1
            // 
            this.probeView1.AutoScroll = true;
            this.probeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.probeView1.Location = new System.Drawing.Point(3, 3);
            this.probeView1.Margin = new System.Windows.Forms.Padding(4);
            this.probeView1.Name = "probeView1";
            this.probeView1.SeriesViewObserving = null;
            this.probeView1.Size = new System.Drawing.Size(160, 387);
            this.probeView1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.exportView1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(166, 393);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Export";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // exportView1
            // 
            this.exportView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportView1.DSEvents = null;
            this.exportView1.DSGraph = null;
            this.exportView1.Location = new System.Drawing.Point(3, 3);
            this.exportView1.Margin = new System.Windows.Forms.Padding(4);
            this.exportView1.Name = "exportView1";
            this.exportView1.SeriesViewObserving = null;
            this.exportView1.Size = new System.Drawing.Size(160, 387);
            this.exportView1.TabIndex = 0;
            // 
            // richTextBoxGraph
            // 
            this.richTextBoxGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxGraph.Location = new System.Drawing.Point(3, 422);
            this.richTextBoxGraph.Name = "richTextBoxGraph";
            this.richTextBoxGraph.Size = new System.Drawing.Size(337, 110);
            this.richTextBoxGraph.TabIndex = 2;
            this.richTextBoxGraph.Text = "";
            // 
            // seriesView
            // 
            this.seriesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seriesView.Location = new System.Drawing.Point(177, 3);
            this.seriesView.Margin = new System.Windows.Forms.Padding(4);
            this.seriesView.Name = "seriesView";
            this.seriesView.Size = new System.Drawing.Size(162, 419);
            this.seriesView.TabIndex = 1;
            // 
            // tabControlRight
            // 
            this.tabControlRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRight.Controls.Add(this.tabPage3);
            this.tabControlRight.Controls.Add(this.tabPage4);
            this.tabControlRight.Controls.Add(this.tabPage7);
            this.tabControlRight.Location = new System.Drawing.Point(350, 0);
            this.tabControlRight.Name = "tabControlRight";
            this.tabControlRight.SelectedIndex = 0;
            this.tabControlRight.Size = new System.Drawing.Size(758, 558);
            this.tabControlRight.TabIndex = 3;
            this.tabControlRight.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.mainGraphView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(750, 532);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Graph";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // mainGraphView
            // 
            this.mainGraphView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGraphView.EventsView = null;
            this.mainGraphView.Location = new System.Drawing.Point(3, 3);
            this.mainGraphView.Margin = new System.Windows.Forms.Padding(4);
            this.mainGraphView.MForm = null;
            this.mainGraphView.Name = "mainGraphView";
            this.mainGraphView.ProbeView = null;
            this.mainGraphView.SeriesViewObserving = null;
            this.mainGraphView.Size = new System.Drawing.Size(744, 526);
            this.mainGraphView.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.eventsView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(750, 532);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Events";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // eventsView1
            // 
            this.eventsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsView1.GraphView = null;
            this.eventsView1.Location = new System.Drawing.Point(3, 3);
            this.eventsView1.Margin = new System.Windows.Forms.Padding(2);
            this.eventsView1.MForm = null;
            this.eventsView1.Name = "eventsView1";
            this.eventsView1.Size = new System.Drawing.Size(744, 526);
            this.eventsView1.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.competitionView1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(750, 532);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Competition";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // competitionView1
            // 
            this.competitionView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.competitionView1.FileView = null;
            this.competitionView1.Location = new System.Drawing.Point(3, 3);
            this.competitionView1.Name = "competitionView1";
            this.competitionView1.SeriesViewObserving = null;
            this.competitionView1.Size = new System.Drawing.Size(744, 526);
            this.competitionView1.TabIndex = 0;
            // 
            // timerCompMode
            // 
            this.timerCompMode.Enabled = true;
            this.timerCompMode.Interval = 1000;
            this.timerCompMode.Tick += new System.EventHandler(this.TimerCompMode_Tick);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.StatusHelp_16xMD;
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHelp.Location = new System.Drawing.Point(1084, -1);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(22, 22);
            this.buttonHelp.TabIndex = 0;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 561);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.tabControlRight);
            this.Controls.Add(this.tabControlLeft);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(904, 257);
            this.Name = "MainForm";
            this.Text = "DSLOG Reader 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControlLeft.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabControlRight.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FileListView fileListView;
        private SeriesView seriesView;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBoxGraph;
        private System.Windows.Forms.TabControl tabControlRight;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button buttonHelp;
        private MainGraphView mainGraphView;
        private ProbeView probeView1;
        private ExportView exportView1;
        private System.Windows.Forms.Timer timerCompMode;
        private EventsView eventsView1;
        private System.Windows.Forms.TabPage tabPage7;
        private CompetitionView competitionView1;
    }
}