namespace DSLOGMatchCollector
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxLoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonChangeLocation = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonCollectMatches = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorkerGet = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // textBoxLoc
            // 
            this.textBoxLoc.Location = new System.Drawing.Point(12, 29);
            this.textBoxLoc.Name = "textBoxLoc";
            this.textBoxLoc.ReadOnly = true;
            this.textBoxLoc.Size = new System.Drawing.Size(381, 20);
            this.textBoxLoc.TabIndex = 0;
            this.textBoxLoc.Text = "C:\\Users\\Public\\Documents\\FRC\\Log Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Step 1: Set Logs Location";
            // 
            // buttonChangeLocation
            // 
            this.buttonChangeLocation.Location = new System.Drawing.Point(399, 23);
            this.buttonChangeLocation.Name = "buttonChangeLocation";
            this.buttonChangeLocation.Size = new System.Drawing.Size(73, 26);
            this.buttonChangeLocation.TabIndex = 2;
            this.buttonChangeLocation.Text = "Change";
            this.buttonChangeLocation.UseVisualStyleBackColor = true;
            this.buttonChangeLocation.Click += new System.EventHandler(this.buttonChangeLocation_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 395);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(460, 41);
            this.progressBar1.TabIndex = 3;
            // 
            // buttonCollectMatches
            // 
            this.buttonCollectMatches.Location = new System.Drawing.Point(133, 55);
            this.buttonCollectMatches.Name = "buttonCollectMatches";
            this.buttonCollectMatches.Size = new System.Drawing.Size(103, 32);
            this.buttonCollectMatches.TabIndex = 4;
            this.buttonCollectMatches.Text = "Collect Matches";
            this.buttonCollectMatches.UseVisualStyleBackColor = true;
            this.buttonCollectMatches.Click += new System.EventHandler(this.buttonCollectMatches_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 93);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(460, 296);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Event";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Match #";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File Name";
            this.columnHeader3.Width = 260;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Matches Found:";
            // 
            // backgroundWorkerGet
            // 
            this.backgroundWorkerGet.WorkerReportsProgress = true;
            this.backgroundWorkerGet.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGet_DoWork);
            this.backgroundWorkerGet.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerGet_ProgressChanged);
            this.backgroundWorkerGet.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGet_RunWorkerCompleted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Step 2: Click Button ->";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Step 3: Upload MatchFiles.zip (located where this program is) to ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(314, 439);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(163, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "this form. (requires google sign in)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonCollectMatches);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonChangeLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLoc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "DSLOG Match Collector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonChangeLocation;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonCollectMatches;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

