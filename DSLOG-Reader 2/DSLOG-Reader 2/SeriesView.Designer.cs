namespace DSLOG_Reader_2
{
    partial class SeriesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesView));
            this.treeView = new DSLOG_Reader_2.SeriesView.SeriesTreeView();
            this.buttonEditGroups = new System.Windows.Forms.Button();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.BackColor = System.Drawing.SystemColors.Control;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.CheckBoxes = true;
            this.treeView.Indent = 12;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(216, 431);
            this.treeView.TabIndex = 0;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeSelect);
            // 
            // buttonEditGroups
            // 
            this.buttonEditGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditGroups.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditGroups.Image")));
            this.buttonEditGroups.Location = new System.Drawing.Point(181, -1);
            this.buttonEditGroups.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEditGroups.Name = "buttonEditGroups";
            this.buttonEditGroups.Size = new System.Drawing.Size(36, 28);
            this.buttonEditGroups.TabIndex = 1;
            this.buttonEditGroups.UseVisualStyleBackColor = true;
            this.buttonEditGroups.Click += new System.EventHandler(this.buttonEditGroups_Click);
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Items.AddRange(new object[] {
            "Default"});
            this.comboBoxProfiles.Location = new System.Drawing.Point(0, 0);
            this.comboBoxProfiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(181, 24);
            this.comboBoxProfiles.TabIndex = 2;
            this.comboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfiles_SelectedIndexChanged);
            // 
            // SeriesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(119F, 119F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.buttonEditGroups);
            this.Controls.Add(this.treeView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SeriesView";
            this.Size = new System.Drawing.Size(216, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEditGroups;
        private SeriesTreeView treeView;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
    }
}
