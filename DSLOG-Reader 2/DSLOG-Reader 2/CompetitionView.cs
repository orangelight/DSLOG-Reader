using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader_2
{
    public partial class CompetitionView : UserControl, SeriesViewObserver
    {
        public CompetitionView()
        {
            InitializeComponent();
        }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            //throw new NotImplementedException();
        }

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            //throw new NotImplementedException();
        }
    }
}
