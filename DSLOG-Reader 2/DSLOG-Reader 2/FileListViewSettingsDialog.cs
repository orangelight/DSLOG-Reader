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
        }

        private void checkBoxFillIn_CheckedChanged(object sender, EventArgs e)
        {
            AllowEventFillIn = checkBoxFillIn.Checked;
        }
    }
}
