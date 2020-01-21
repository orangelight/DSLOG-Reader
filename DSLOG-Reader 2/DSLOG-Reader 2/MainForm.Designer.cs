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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.seriesView = new DSLOG_Reader_2.SeriesView();
            this.fileListView = new DSLOG_Reader_2.FileListView();
            this.SuspendLayout();
            // 
            // seriesView
            // 
            this.seriesView.Location = new System.Drawing.Point(353, 12);
            this.seriesView.Name = "seriesView";
            this.seriesView.Size = new System.Drawing.Size(150, 494);
            this.seriesView.TabIndex = 1;
            // 
            // fileListView
            // 
            this.fileListView.Location = new System.Drawing.Point(12, 12);
            this.fileListView.MinimumSize = new System.Drawing.Size(335, 540);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(335, 540);
            this.fileListView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 558);
            this.Controls.Add(this.seriesView);
            this.Controls.Add(this.fileListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private FileListView fileListView;
        private SeriesView seriesView;
    }
}