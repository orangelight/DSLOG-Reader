using DSLOG_Reader_Library;
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
            DSLOGReader reader = new DSLOGReader(@"C:\Users\Public\Documents\FRC\Log Files\2019_07_13 11_10_04 Sat.dslog");
            reader.Read();
        }
    }
}
