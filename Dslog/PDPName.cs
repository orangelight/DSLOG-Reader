using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader
{
    public partial class PDPName : UserControl
    {
        public PDPName(String name, Color c, int index)
        {
            InitializeComponent();
            this.textBox1.Text = name;
            this.BackColor = c;
            this.label1.Text = "PDP " + index;
            this.textBox1.TabIndex= index;
        }

        public String getName()
        {
            return textBox1.Text;
        }
    }
}
