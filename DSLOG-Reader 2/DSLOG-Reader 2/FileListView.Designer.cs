namespace DSLOG_Reader_2
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSeconds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMatchNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTimeAgo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEventName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filterSelectorCombo = new System.Windows.Forms.ComboBox();
            this.timerScrollToBottom = new System.Windows.Forms.Timer(this.components);
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
            this.listView.Size = new System.Drawing.Size(330, 519);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
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
            this.filterSelectorCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterSelectorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterSelectorCombo.FormattingEnabled = true;
            this.filterSelectorCombo.Location = new System.Drawing.Point(209, 0);
            this.filterSelectorCombo.Name = "filterSelectorCombo";
            this.filterSelectorCombo.Size = new System.Drawing.Size(121, 21);
            this.filterSelectorCombo.TabIndex = 1;
            // 
            // timerScrollToBottom
            // 
            this.timerScrollToBottom.Tick += new System.EventHandler(this.timerScrollToBottom_Tick);
            // 
            // FileListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filterSelectorCombo);
            this.Controls.Add(this.listView);
            this.MinimumSize = new System.Drawing.Size(330, 540);
            this.Name = "FileListView";
            this.Size = new System.Drawing.Size(330, 540);
            this.ResumeLayout(false);

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
    }
}
