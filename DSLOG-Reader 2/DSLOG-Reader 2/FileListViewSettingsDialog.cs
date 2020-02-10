using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader_2
{
    public partial class FileListViewSettingsDialog : Form
    {
        public bool AllowEventFillIn { get; set; }
        public string FolderPath { get; set; }
        public FileListViewSettingsDialog()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FileListViewSettingsDialog_Shown(object sender, EventArgs e)
        {
            checkBoxFillIn.Checked = AllowEventFillIn;
            textBoxPath.Text = FolderPath;
        }

        private void checkBoxFillIn_CheckedChanged(object sender, EventArgs e)
        {
            AllowEventFillIn = checkBoxFillIn.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            browserDialog.SelectedPath = textBoxPath.Text;
            var result = browserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxPath.Text = browserDialog.SelectedPath;
                FolderPath = browserDialog.SelectedPath;
            }
        }
    }
}
