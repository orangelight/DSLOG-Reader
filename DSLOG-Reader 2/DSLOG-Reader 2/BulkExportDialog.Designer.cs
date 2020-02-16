namespace DSLOG_Reader_2
{
    partial class BulkExportDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkExportDialog));
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEvent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.backgroundWorkerExport = new System.ComponentModel.BackgroundWorker();
            this.checkBoxEvents = new System.Windows.Forms.CheckBox();
            this.checkBoxLogs = new System.Windows.Forms.CheckBox();
            this.labelIntro = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonPath = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.timerUpdateTotal = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxMatchTime = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeader4,
            this.columnHeaderSec,
            this.columnHeaderNum,
            this.columnHeader1,
            this.columnHeaderEvent});
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 52);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(415, 263);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 0;
            // 
            // columnHeaderSec
            // 
            this.columnHeaderSec.Text = "";
            this.columnHeaderSec.Width = 0;
            // 
            // columnHeaderNum
            // 
            this.columnHeaderNum.Text = "Match #";
            this.columnHeaderNum.Width = 55;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // columnHeaderEvent
            // 
            this.columnHeaderEvent.Text = "Event";
            this.columnHeaderEvent.Width = 70;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 321);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(595, 23);
            this.progressBar.TabIndex = 1;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(436, 292);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(517, 292);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // backgroundWorkerExport
            // 
            this.backgroundWorkerExport.WorkerReportsProgress = true;
            this.backgroundWorkerExport.WorkerSupportsCancellation = true;
            this.backgroundWorkerExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExport_DoWork);
            this.backgroundWorkerExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerExport_ProgressChanged);
            this.backgroundWorkerExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerExport_RunWorkerCompleted);
            // 
            // checkBoxEvents
            // 
            this.checkBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEvents.AutoSize = true;
            this.checkBoxEvents.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxEvents.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBoxEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEvents.Location = new System.Drawing.Point(263, 29);
            this.checkBoxEvents.Name = "checkBoxEvents";
            this.checkBoxEvents.Size = new System.Drawing.Size(122, 17);
            this.checkBoxEvents.TabIndex = 4;
            this.checkBoxEvents.Text = "Export DSEvents";
            this.checkBoxEvents.UseVisualStyleBackColor = false;
            // 
            // checkBoxLogs
            // 
            this.checkBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLogs.AutoSize = true;
            this.checkBoxLogs.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxLogs.Checked = true;
            this.checkBoxLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLogs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBoxLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLogs.Location = new System.Drawing.Point(151, 29);
            this.checkBoxLogs.Name = "checkBoxLogs";
            this.checkBoxLogs.Size = new System.Drawing.Size(110, 17);
            this.checkBoxLogs.TabIndex = 5;
            this.checkBoxLogs.Text = "Export DSLogs";
            this.checkBoxLogs.UseVisualStyleBackColor = false;
            // 
            // labelIntro
            // 
            this.labelIntro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIntro.Location = new System.Drawing.Point(433, 62);
            this.labelIntro.Name = "labelIntro";
            this.labelIntro.Size = new System.Drawing.Size(156, 77);
            this.labelIntro.TabIndex = 6;
            this.labelIntro.Text = "All logs on left will be exported to CSV using the profile selected in the Graph " +
    "tab with all series enabled";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(71, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(447, 20);
            this.textBox1.TabIndex = 7;
            // 
            // buttonPath
            // 
            this.buttonPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPath.Location = new System.Drawing.Point(524, 4);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(67, 22);
            this.buttonPath.TabIndex = 8;
            this.buttonPath.Text = "Change";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(437, 273);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(60, 13);
            this.labelTotal.TabIndex = 9;
            this.labelTotal.Text = "Total Logs:";
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Checked = true;
            this.checkBoxAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAll.Location = new System.Drawing.Point(13, 29);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(120, 17);
            this.checkBoxAll.TabIndex = 10;
            this.checkBoxAll.Text = "Check/Uncheck All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // timerUpdateTotal
            // 
            this.timerUpdateTotal.Enabled = true;
            this.timerUpdateTotal.Interval = 500;
            this.timerUpdateTotal.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Export To:";
            // 
            // checkBoxMatchTime
            // 
            this.checkBoxMatchTime.AutoSize = true;
            this.checkBoxMatchTime.Location = new System.Drawing.Point(426, 29);
            this.checkBoxMatchTime.Name = "checkBoxMatchTime";
            this.checkBoxMatchTime.Size = new System.Drawing.Size(163, 17);
            this.checkBoxMatchTime.TabIndex = 12;
            this.checkBoxMatchTime.Text = "Use Match Time for Matches";
            this.checkBoxMatchTime.UseVisualStyleBackColor = true;
            // 
            // BulkExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 344);
            this.Controls.Add(this.checkBoxMatchTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelIntro);
            this.Controls.Add(this.checkBoxLogs);
            this.Controls.Add(this.checkBoxEvents);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BulkExportDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Export";
            this.Shown += new System.EventHandler(this.BulkExportDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExport;
        private System.Windows.Forms.CheckBox checkBoxEvents;
        private System.Windows.Forms.CheckBox checkBoxLogs;
        private System.Windows.Forms.Label labelIntro;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderSec;
        private System.Windows.Forms.ColumnHeader columnHeaderNum;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeaderEvent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Timer timerUpdateTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxMatchTime;
    }
}