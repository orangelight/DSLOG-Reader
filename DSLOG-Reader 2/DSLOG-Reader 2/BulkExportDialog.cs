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
    public partial class BulkExportDialog : Form
    {
        public List<DSLOGFileEntry> Files { get; set; }
        public string FilePath { get; set; }
        private bool Init = false;
        public BulkExportDialog()
        {
            InitializeComponent();
            listView.DoubleBuffered(true);
        }

        private void BulkExportDialog_Shown(object sender, EventArgs e)
        {
            if (Files == null) return;
            foreach(var file in Files)
            {
                var item = file.ToListViewItem();
                item.Checked = true;
                listView.Items.Add(item);
            }
            listView.Columns[0].Width = -2;
            listView.Columns[3].Width = -2;
            listView.Columns[5].Width = -2;
            UpdateTotal();
            Init = true;
        }

        private void UpdateTotal()
        {
            labelTotal.Text = $"Total Logs: {listView.CheckedItems.Count}";
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!Init) return;
            UpdateTotal();
        }
    }
}
