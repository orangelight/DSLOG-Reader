namespace DSLOG_Reader_2
{
    partial class GroupEditorDialog
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
            this.treeViewPDP = new System.Windows.Forms.TreeView();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.buttonAddProfile = new System.Windows.Forms.Button();
            this.buttonRemoveProfile = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // treeViewPDP
            // 
            this.treeViewPDP.AllowDrop = true;
            this.treeViewPDP.HideSelection = false;
            this.treeViewPDP.LabelEdit = true;
            this.treeViewPDP.Location = new System.Drawing.Point(192, 6);
            this.treeViewPDP.Name = "treeViewPDP";
            this.treeViewPDP.ShowPlusMinus = false;
            this.treeViewPDP.ShowRootLines = false;
            this.treeViewPDP.Size = new System.Drawing.Size(193, 365);
            this.treeViewPDP.TabIndex = 0;
            this.treeViewPDP.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewPDP_ItemDrag);
            this.treeViewPDP.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPDP_BeforeSelect);
            this.treeViewPDP.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragDrop);
            this.treeViewPDP.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragEnter);
            this.treeViewPDP.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragOver);
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(6, 6);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(171, 21);
            this.comboBoxProfiles.TabIndex = 1;
            this.comboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfiles_SelectedIndexChanged);
            // 
            // buttonAddProfile
            // 
            this.buttonAddProfile.Location = new System.Drawing.Point(6, 33);
            this.buttonAddProfile.Name = "buttonAddProfile";
            this.buttonAddProfile.Size = new System.Drawing.Size(70, 23);
            this.buttonAddProfile.TabIndex = 2;
            this.buttonAddProfile.Text = "Add Profile";
            this.buttonAddProfile.UseVisualStyleBackColor = true;
            this.buttonAddProfile.Click += new System.EventHandler(this.buttonAddProfile_Click);
            // 
            // buttonRemoveProfile
            // 
            this.buttonRemoveProfile.Location = new System.Drawing.Point(82, 33);
            this.buttonRemoveProfile.Name = "buttonRemoveProfile";
            this.buttonRemoveProfile.Size = new System.Drawing.Size(95, 23);
            this.buttonRemoveProfile.TabIndex = 3;
            this.buttonRemoveProfile.Text = "Remove Profile";
            this.buttonRemoveProfile.UseVisualStyleBackColor = true;
            this.buttonRemoveProfile.Click += new System.EventHandler(this.buttonRemoveProfile_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(78, 62);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(99, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Profile Name";
            // 
            // buttonColor
            // 
            this.buttonColor.Enabled = false;
            this.buttonColor.Location = new System.Drawing.Point(6, 149);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(98, 23);
            this.buttonColor.TabIndex = 6;
            this.buttonColor.Text = "Change Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // GroupEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 389);
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonRemoveProfile);
            this.Controls.Add(this.buttonAddProfile);
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.treeViewPDP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GroupEditorDialog";
            this.Text = "GroupEditorDialog";
            this.Load += new System.EventHandler(this.GroupEditorDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewPDP;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.Button buttonAddProfile;
        private System.Windows.Forms.Button buttonRemoveProfile;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}