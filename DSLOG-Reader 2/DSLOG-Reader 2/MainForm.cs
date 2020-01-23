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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            fileListView.MainChart = mainGraphView;
            fileListView.SetPath(@"C:\Users\Public\Documents\FRC\Log Files");
            fileListView.LoadFiles();
            seriesView.MainChart = mainGraphView;
            seriesView.LoadSeries();
           
        }
    }
}
