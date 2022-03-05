namespace DSLOG_Reader_2
{
    partial class EventsView
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listViewEvents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonCodeOutput = new System.Windows.Forms.Button();
            this.buttonImportant = new System.Windows.Forms.Button();
            this.buttonDup = new System.Windows.Forms.Button();
            this.buttonJoystick = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 421);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(870, 249);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // listViewEvents
            // 
            this.listViewEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewEvents.FullRowSelect = true;
            this.listViewEvents.GridLines = true;
            this.listViewEvents.HideSelection = false;
            this.listViewEvents.Location = new System.Drawing.Point(0, 22);
            this.listViewEvents.Margin = new System.Windows.Forms.Padding(2);
            this.listViewEvents.MultiSelect = false;
            this.listViewEvents.Name = "listViewEvents";
            this.listViewEvents.Size = new System.Drawing.Size(870, 394);
            this.listViewEvents.TabIndex = 1;
            this.listViewEvents.UseCompatibleStateImageBehavior = false;
            this.listViewEvents.View = System.Windows.Forms.View.Details;
            this.listViewEvents.SelectedIndexChanged += new System.EventHandler(this.ListViewEvents_SelectedIndexChanged);
            this.listViewEvents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEvents_MouseDoubleClick);
            this.listViewEvents.Resize += new System.EventHandler(this.ListViewEvents_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DS Time";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Event Message Text";
            this.columnHeader2.Width = 1000;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(564, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(211, 20);
            this.textBoxSearch.TabIndex = 4;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // buttonCodeOutput
            // 
            this.buttonCodeOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCodeOutput.BackColor = System.Drawing.Color.Red;
            this.buttonCodeOutput.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.Output_16x;
            this.buttonCodeOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCodeOutput.FlatAppearance.BorderSize = 0;
            this.buttonCodeOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCodeOutput.Location = new System.Drawing.Point(800, -1);
            this.buttonCodeOutput.Name = "buttonCodeOutput";
            this.buttonCodeOutput.Size = new System.Drawing.Size(23, 23);
            this.buttonCodeOutput.TabIndex = 6;
            this.buttonCodeOutput.UseVisualStyleBackColor = false;
            this.buttonCodeOutput.Click += new System.EventHandler(this.buttonCodeOutput_Click);
            // 
            // buttonImportant
            // 
            this.buttonImportant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportant.BackColor = System.Drawing.Color.Red;
            this.buttonImportant.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.Important_16x;
            this.buttonImportant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonImportant.FlatAppearance.BorderSize = 0;
            this.buttonImportant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImportant.Location = new System.Drawing.Point(777, -1);
            this.buttonImportant.Name = "buttonImportant";
            this.buttonImportant.Size = new System.Drawing.Size(23, 23);
            this.buttonImportant.TabIndex = 5;
            this.buttonImportant.UseVisualStyleBackColor = false;
            this.buttonImportant.Click += new System.EventHandler(this.buttonImportant_Click);
            // 
            // buttonDup
            // 
            this.buttonDup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDup.BackColor = System.Drawing.Color.Red;
            this.buttonDup.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.RepeatButton_16red;
            this.buttonDup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDup.FlatAppearance.BorderSize = 0;
            this.buttonDup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonDup.Location = new System.Drawing.Point(824, -1);
            this.buttonDup.Name = "buttonDup";
            this.buttonDup.Size = new System.Drawing.Size(23, 23);
            this.buttonDup.TabIndex = 3;
            this.buttonDup.TabStop = false;
            this.buttonDup.UseVisualStyleBackColor = false;
            this.buttonDup.Click += new System.EventHandler(this.buttonDup_Click);
            // 
            // buttonJoystick
            // 
            this.buttonJoystick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJoystick.BackColor = System.Drawing.Color.Red;
            this.buttonJoystick.BackgroundImage = global::DSLOG_Reader_2.Properties.Resources.x_con;
            this.buttonJoystick.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonJoystick.FlatAppearance.BorderSize = 0;
            this.buttonJoystick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJoystick.Location = new System.Drawing.Point(848, -1);
            this.buttonJoystick.Name = "buttonJoystick";
            this.buttonJoystick.Size = new System.Drawing.Size(23, 23);
            this.buttonJoystick.TabIndex = 2;
            this.buttonJoystick.TabStop = false;
            this.buttonJoystick.UseVisualStyleBackColor = false;
            this.buttonJoystick.Click += new System.EventHandler(this.buttonJoystick_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(483, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Search Events:";
            // 
            // EventsView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCodeOutput);
            this.Controls.Add(this.buttonImportant);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonDup);
            this.Controls.Add(this.buttonJoystick);
            this.Controls.Add(this.listViewEvents);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EventsView";
            this.Size = new System.Drawing.Size(870, 670);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListView listViewEvents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonJoystick;
        private System.Windows.Forms.Button buttonDup;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonImportant;
        private System.Windows.Forms.Button buttonCodeOutput;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
    }
}
