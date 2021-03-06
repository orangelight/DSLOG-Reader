﻿namespace DSLOG_Reader_2
{
    partial class FileListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileListView));
            this.listView = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSeconds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMatchNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTimeAgo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEventName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filterSelectorCombo = new System.Windows.Forms.ComboBox();
            this.timerScrollToBottom = new System.Windows.Forms.Timer(this.components);
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerFileUpdate = new System.Windows.Forms.Timer(this.components);
            this.buttonChangePath = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonBulkExport = new System.Windows.Forms.Button();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName,
            this.columnTime,
            this.columnSeconds,
            this.columnMatchNum,
            this.columnTimeAgo,
            this.columnEventName});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 21);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(293, 344);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnFileName
            // 
            this.columnFileName.DisplayIndex = 5;
            this.columnFileName.Text = "File Name";
            this.columnFileName.Width = 170;
            // 
            // columnTime
            // 
            this.columnTime.DisplayIndex = 0;
            this.columnTime.Text = "Time";
            this.columnTime.Width = 123;
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
            this.columnMatchNum.Width = 55;
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
            // filterSelectorCombo
            // 
            this.filterSelectorCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterSelectorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterSelectorCombo.FormattingEnabled = true;
            this.filterSelectorCombo.Location = new System.Drawing.Point(114, 0);
            this.filterSelectorCombo.Name = "filterSelectorCombo";
            this.filterSelectorCombo.Size = new System.Drawing.Size(120, 21);
            this.filterSelectorCombo.TabIndex = 1;
            this.filterSelectorCombo.SelectedIndexChanged += new System.EventHandler(this.filterSelectorCombo_SelectedIndexChanged);
            // 
            // timerScrollToBottom
            // 
            this.timerScrollToBottom.Interval = 10;
            this.timerScrollToBottom.Tick += new System.EventHandler(this.timerScrollToBottom_Tick);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPath.Location = new System.Drawing.Point(0, 367);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(293, 21);
            this.textBoxPath.TabIndex = 3;
            // 
            // timerFileUpdate
            // 
            this.timerFileUpdate.Interval = 500;
            this.timerFileUpdate.Tick += new System.EventHandler(this.timerFileUpdate_Tick);
            // 
            // buttonChangePath
            // 
            this.buttonChangePath.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.OpenFolder_16x;
            this.buttonChangePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonChangePath.Location = new System.Drawing.Point(28, -1);
            this.buttonChangePath.Name = "buttonChangePath";
            this.buttonChangePath.Size = new System.Drawing.Size(29, 23);
            this.buttonChangePath.TabIndex = 9;
            this.buttonChangePath.UseVisualStyleBackColor = true;
            this.buttonChangePath.Click += new System.EventHandler(this.buttonChangePath_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.OpenFile_16x;
            this.buttonOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonOpenFile.Location = new System.Drawing.Point(0, -1);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(29, 23);
            this.buttonOpenFile.TabIndex = 8;
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonBulkExport
            // 
            this.buttonBulkExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBulkExport.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.ExportTableToFile_16x;
            this.buttonBulkExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBulkExport.Location = new System.Drawing.Point(235, -1);
            this.buttonBulkExport.Name = "buttonBulkExport";
            this.buttonBulkExport.Size = new System.Drawing.Size(29, 23);
            this.buttonBulkExport.TabIndex = 7;
            this.buttonBulkExport.UseVisualStyleBackColor = true;
            this.buttonBulkExport.Click += new System.EventHandler(this.buttonBulkExport_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.RunFilter_16x;
            this.buttonFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFilter.Location = new System.Drawing.Point(84, -1);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(29, 23);
            this.buttonFilter.TabIndex = 6;
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = global::DSLOG_Reader_2.Properties.Resources.Refresh_grey_16xMD;
            this.buttonRefresh.Location = new System.Drawing.Point(56, -1);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(29, 23);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefreash_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.Location = new System.Drawing.Point(263, -1);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(29, 23);
            this.buttonSettings.TabIndex = 2;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // FileListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.buttonChangePath);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonBulkExport);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.filterSelectorCombo);
            this.Controls.Add(this.listView);
            this.Name = "FileListView";
            this.Size = new System.Drawing.Size(293, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ComboBox filterSelectorCombo;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ColumnHeader columnSeconds;
        private System.Windows.Forms.ColumnHeader columnMatchNum;
        private System.Windows.Forms.ColumnHeader columnTimeAgo;
        private System.Windows.Forms.ColumnHeader columnEventName;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.Timer timerScrollToBottom;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerFileUpdate;
        private System.Windows.Forms.Button buttonBulkExport;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonChangePath;
    }
}
