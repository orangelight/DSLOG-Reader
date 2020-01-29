using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSLOG_Reader_Library;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSLOG_Reader_2
{
    public partial class ProbeView : UserControl
    {
        public ProbeView()
        {
            InitializeComponent();
        }

        public void SetProbe(DSLOGEntry entry, SeriesCollection series, Dictionary<string, int[]> idTOpdp)
        {
            Controls.Clear();
            if (entry != null  && series!= null)
            {
                int labelNum = 0;
                Label timeLabel = new Label();
                timeLabel.Text = $"Time: {entry.Time.ToString("HH:mm:ss.fff")}";
                timeLabel.Visible = true;
                timeLabel.AutoSize = true;
                timeLabel.Location = new Point(4, (24 * labelNum++) + 7);
                Controls.Add(timeLabel);
                foreach(Series ser in series)
                {
                    if (!ser.Enabled || ser.Name == "messages") continue;
                    Label seriesLabel = new Label();
                    seriesLabel.Text = $"{ser.LegendText}: ";
                    seriesLabel.Visible = true;
                    seriesLabel.AutoSize = true;
                    seriesLabel.Location = new Point(4, (24 * labelNum++) + 7);
                    Controls.Add(seriesLabel);
                }
            }
        }
    }
}
