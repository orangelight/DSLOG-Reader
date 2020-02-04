namespace DSLOG_Reader_2
{
    partial class ExportView
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
            this.comboBoxExportType = new System.Windows.Forms.ComboBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.labelTotalCol = new System.Windows.Forms.Label();
            this.labelMode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxExportType
            // 
            this.comboBoxExportType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExportType.FormattingEnabled = true;
            this.comboBoxExportType.Items.AddRange(new object[] {
            "CSV",
            "Clipboard",
            "Chart Image"});
            this.comboBoxExportType.Location = new System.Drawing.Point(1, 21);
            this.comboBoxExportType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxExportType.Name = "comboBoxExportType";
            this.comboBoxExportType.Size = new System.Drawing.Size(176, 21);
            this.comboBoxExportType.TabIndex = 0;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(1, 47);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(176, 28);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // labelTotalCol
            // 
            this.labelTotalCol.AutoSize = true;
            this.labelTotalCol.Location = new System.Drawing.Point(0, 79);
            this.labelTotalCol.Name = "labelTotalCol";
            this.labelTotalCol.Size = new System.Drawing.Size(80, 13);
            this.labelTotalCol.TabIndex = 2;
            this.labelTotalCol.Text = "Total  Columns:";
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMode.Location = new System.Drawing.Point(1, 4);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(82, 13);
            this.labelMode.TabIndex = 3;
            this.labelMode.Text = "Export Mode:";
            // 
            // ExportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.labelTotalCol);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.comboBoxExportType);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportView";
            this.Size = new System.Drawing.Size(184, 334);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxExportType;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Label labelTotalCol;
        private System.Windows.Forms.Label labelMode;
    }
}
