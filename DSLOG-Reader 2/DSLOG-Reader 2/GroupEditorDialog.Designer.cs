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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupEditorDialog));
            this.treeViewPDP = new System.Windows.Forms.TreeView();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.buttonAddProfile = new System.Windows.Forms.Button();
            this.buttonRemoveProfile = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.buttonRemoveGroup = new System.Windows.Forms.Button();
            this.checkBoxTotal = new System.Windows.Forms.CheckBox();
            this.checkBoxDelta = new System.Windows.Forms.CheckBox();
            this.labelPDPSlot = new System.Windows.Forms.Label();
            this.buttonCopyProfile = new System.Windows.Forms.Button();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewPDP
            // 
            this.treeViewPDP.AllowDrop = true;
            this.treeViewPDP.HideSelection = false;
            this.treeViewPDP.LabelEdit = true;
            this.treeViewPDP.Location = new System.Drawing.Point(193, 6);
            this.treeViewPDP.Name = "treeViewPDP";
            this.treeViewPDP.ShowPlusMinus = false;
            this.treeViewPDP.ShowRootLines = false;
            this.treeViewPDP.Size = new System.Drawing.Size(207, 305);
            this.treeViewPDP.TabIndex = 0;
            this.treeViewPDP.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPDP_BeforeCollapse);
            this.treeViewPDP.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewPDP_ItemDrag);
            this.treeViewPDP.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.treeViewPDP_NodeMouseHover);
            this.treeViewPDP.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPDP_BeforeSelect);
            this.treeViewPDP.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPDP_AfterSelect);
            this.treeViewPDP.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragDrop);
            this.treeViewPDP.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragEnter);
            this.treeViewPDP.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewPDP_DragOver);
            this.treeViewPDP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeViewPDP_MouseMove);
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(6, 19);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(171, 21);
            this.comboBoxProfiles.TabIndex = 1;
            this.comboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfiles_SelectedIndexChanged);
            // 
            // buttonAddProfile
            // 
            this.buttonAddProfile.Location = new System.Drawing.Point(6, 46);
            this.buttonAddProfile.Name = "buttonAddProfile";
            this.buttonAddProfile.Size = new System.Drawing.Size(70, 23);
            this.buttonAddProfile.TabIndex = 2;
            this.buttonAddProfile.Text = "Add Profile";
            this.buttonAddProfile.UseVisualStyleBackColor = true;
            this.buttonAddProfile.Click += new System.EventHandler(this.buttonAddProfile_Click);
            // 
            // buttonRemoveProfile
            // 
            this.buttonRemoveProfile.Location = new System.Drawing.Point(82, 46);
            this.buttonRemoveProfile.Name = "buttonRemoveProfile";
            this.buttonRemoveProfile.Size = new System.Drawing.Size(95, 23);
            this.buttonRemoveProfile.TabIndex = 3;
            this.buttonRemoveProfile.Text = "Remove Profile";
            this.buttonRemoveProfile.UseVisualStyleBackColor = true;
            this.buttonRemoveProfile.Click += new System.EventHandler(this.buttonRemoveProfile_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(78, 98);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(99, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Profile Name";
            // 
            // buttonColor
            // 
            this.buttonColor.Enabled = false;
            this.buttonColor.Location = new System.Drawing.Point(5, 39);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(174, 55);
            this.buttonColor.TabIndex = 6;
            this.buttonColor.Text = "Change Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(5, 19);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(74, 23);
            this.buttonAddGroup.TabIndex = 8;
            this.buttonAddGroup.Text = "Add Group";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // buttonRemoveGroup
            // 
            this.buttonRemoveGroup.Location = new System.Drawing.Point(86, 19);
            this.buttonRemoveGroup.Name = "buttonRemoveGroup";
            this.buttonRemoveGroup.Size = new System.Drawing.Size(94, 23);
            this.buttonRemoveGroup.TabIndex = 9;
            this.buttonRemoveGroup.Text = "Remove Group";
            this.buttonRemoveGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveGroup.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // checkBoxTotal
            // 
            this.checkBoxTotal.AutoSize = true;
            this.checkBoxTotal.Location = new System.Drawing.Point(6, 48);
            this.checkBoxTotal.Name = "checkBoxTotal";
            this.checkBoxTotal.Size = new System.Drawing.Size(94, 17);
            this.checkBoxTotal.TabIndex = 10;
            this.checkBoxTotal.Text = "Total In Group";
            this.checkBoxTotal.UseVisualStyleBackColor = true;
            this.checkBoxTotal.CheckedChanged += new System.EventHandler(this.checkBoxTotal_CheckedChanged);
            // 
            // checkBoxDelta
            // 
            this.checkBoxDelta.AutoSize = true;
            this.checkBoxDelta.Location = new System.Drawing.Point(6, 71);
            this.checkBoxDelta.Name = "checkBoxDelta";
            this.checkBoxDelta.Size = new System.Drawing.Size(95, 17);
            this.checkBoxDelta.TabIndex = 11;
            this.checkBoxDelta.Text = "Delta In Group";
            this.checkBoxDelta.UseVisualStyleBackColor = true;
            this.checkBoxDelta.CheckedChanged += new System.EventHandler(this.checkBoxDelta_CheckedChanged);
            // 
            // labelPDPSlot
            // 
            this.labelPDPSlot.AutoSize = true;
            this.labelPDPSlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPDPSlot.Location = new System.Drawing.Point(6, 16);
            this.labelPDPSlot.Name = "labelPDPSlot";
            this.labelPDPSlot.Size = new System.Drawing.Size(86, 20);
            this.labelPDPSlot.TabIndex = 12;
            this.labelPDPSlot.Text = "PDP Slot:";
            // 
            // buttonCopyProfile
            // 
            this.buttonCopyProfile.Location = new System.Drawing.Point(7, 72);
            this.buttonCopyProfile.Name = "buttonCopyProfile";
            this.buttonCopyProfile.Size = new System.Drawing.Size(170, 23);
            this.buttonCopyProfile.TabIndex = 13;
            this.buttonCopyProfile.Text = "Copy Profile";
            this.buttonCopyProfile.UseVisualStyleBackColor = true;
            this.buttonCopyProfile.Click += new System.EventHandler(this.buttonCopyProfile_Click);
            // 
            // buttonOkay
            // 
            this.buttonOkay.Location = new System.Drawing.Point(243, 317);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 22);
            this.buttonOkay.TabIndex = 15;
            this.buttonOkay.Text = "Okay";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAddGroup);
            this.groupBox1.Controls.Add(this.buttonRemoveGroup);
            this.groupBox1.Controls.Add(this.checkBoxTotal);
            this.groupBox1.Controls.Add(this.checkBoxDelta);
            this.groupBox1.Location = new System.Drawing.Point(7, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelPDPSlot);
            this.groupBox2.Controls.Add(this.buttonColor);
            this.groupBox2.Location = new System.Drawing.Point(7, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 100);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PDP Slot";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(324, 317);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 22);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxProfiles);
            this.groupBox3.Controls.Add(this.buttonAddProfile);
            this.groupBox3.Controls.Add(this.buttonRemoveProfile);
            this.groupBox3.Controls.Add(this.textBoxName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttonCopyProfile);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 125);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Profile";
            // 
            // GroupEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 344);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.treeViewPDP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupEditorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Group Editor";
            this.Load += new System.EventHandler(this.GroupEditorDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.Button buttonRemoveGroup;
        private System.Windows.Forms.CheckBox checkBoxTotal;
        private System.Windows.Forms.CheckBox checkBoxDelta;
        private System.Windows.Forms.Label labelPDPSlot;
        private System.Windows.Forms.Button buttonCopyProfile;
        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}