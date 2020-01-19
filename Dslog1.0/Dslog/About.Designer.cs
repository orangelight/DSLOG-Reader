namespace Dslog
{
    partial class About
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
            this.labelName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabelSourceCode = new System.Windows.Forms.LinkLabel();
            this.labelRealName = new System.Windows.Forms.Label();
            this.linkLabelRobostangs = new System.Windows.Forms.LinkLabel();
            this.labelFRC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(185, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(123, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "DSLOG Reader";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(186, 29);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(72, 13);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version: 0.2.0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DSLOG_Reader.Properties.Resources.RobostangsLogo;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 166);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabelSourceCode
            // 
            this.linkLabelSourceCode.AutoSize = true;
            this.linkLabelSourceCode.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelSourceCode.Location = new System.Drawing.Point(220, 55);
            this.linkLabelSourceCode.Name = "linkLabelSourceCode";
            this.linkLabelSourceCode.Size = new System.Drawing.Size(183, 13);
            this.linkLabelSourceCode.TabIndex = 5;
            this.linkLabelSourceCode.TabStop = true;
            this.linkLabelSourceCode.Text = "GitHub.com/orangelight/dslog-reader";
            this.linkLabelSourceCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSourceCode_LinkClicked);
            // 
            // labelRealName
            // 
            this.labelRealName.AutoSize = true;
            this.labelRealName.Location = new System.Drawing.Point(186, 68);
            this.labelRealName.Name = "labelRealName";
            this.labelRealName.Size = new System.Drawing.Size(83, 13);
            this.labelRealName.TabIndex = 6;
            this.labelRealName.Text = "By Alex Deneau";
            // 
            // linkLabelRobostangs
            // 
            this.linkLabelRobostangs.AutoSize = true;
            this.linkLabelRobostangs.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelRobostangs.Location = new System.Drawing.Point(241, 94);
            this.linkLabelRobostangs.Name = "linkLabelRobostangs";
            this.linkLabelRobostangs.Size = new System.Drawing.Size(114, 13);
            this.linkLabelRobostangs.TabIndex = 7;
            this.linkLabelRobostangs.TabStop = true;
            this.linkLabelRobostangs.Text = "www.Robostangs.com";
            this.linkLabelRobostangs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRobostangs_LinkClicked);
            // 
            // labelFRC
            // 
            this.labelFRC.AutoSize = true;
            this.labelFRC.Location = new System.Drawing.Point(186, 94);
            this.labelFRC.Name = "labelFRC";
            this.labelFRC.Size = new System.Drawing.Size(49, 13);
            this.labelFRC.TabIndex = 8;
            this.labelFRC.Text = "FRC 548";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Email: alex@robostangs.com";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Code:";
            // 
            // About
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 191);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelFRC);
            this.Controls.Add(this.linkLabelRobostangs);
            this.Controls.Add(this.labelRealName);
            this.Controls.Add(this.linkLabelSourceCode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(429, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(429, 230);
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelSourceCode;
        private System.Windows.Forms.Label labelRealName;
        private System.Windows.Forms.LinkLabel linkLabelRobostangs;
        private System.Windows.Forms.Label labelFRC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}