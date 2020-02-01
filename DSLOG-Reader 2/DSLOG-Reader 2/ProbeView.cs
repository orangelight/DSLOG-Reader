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
                    if (!ser.Enabled || ser.Name == DSAttConstants.Messages) continue;
                    Label seriesLabel = new Label();
                    seriesLabel.Text = $"{ser.LegendText}: {EntryAttToUnit(ser.Name, entry, idTOpdp)}";
                    seriesLabel.Visible = true;
                    seriesLabel.AutoSize = true;
                    seriesLabel.Location = new Point(4, (24 * labelNum++) + 7);
                    Controls.Add(seriesLabel);
                }
            }
        }

        private string EntryAttToUnit(string name, DSLOGEntry en, Dictionary<string, int[]> idTOpdp)
        {
            if (name == DSAttConstants.TripTime)
            {
                return en.TripTime + "ms";
            }
            else if (name == DSAttConstants.LostPackets)
            {
                return en.LostPackets * 100 + "%";
            }
            else if (name == DSAttConstants.Voltage)
            {
                return en.Voltage + "V";
            }
            else if (name == DSAttConstants.RoboRIOCPU)
            {
                return en.RoboRioCPU * 100 + "%";
            }
            else if (name == DSAttConstants.CANUtil)
            {
                return  en.CANUtil * 100 + "%";
            }
            else if (name.StartsWith(DSAttConstants.PDPPrefix))
            {
                return en.GetPDPChannel(int.Parse(name.Substring(3))) + "A";
            }
            else if (name == DSAttConstants.DSDisabled)
            {
                return  en.DSDisabled.ToString();
            }
            else if (name == DSAttConstants.DSAuto)
            {
                return en.DSAuto.ToString();
            }
            else if (name == DSAttConstants.DSTele)
            {
                return  en.DSTele.ToString();
            }
            else if (name == DSAttConstants.RobotDisabled)
            {
                return  en.RobotDisabled.ToString();
            }
            else if (name == DSAttConstants.RobotAuto)
            {
                return en.RobotAuto.ToString();
            }
            else if (name == DSAttConstants.RobotTele)
            {
                return  en.RobotTele.ToString();
            }

            else if (name == DSAttConstants.Brownout)
            {
                return  en.Brownout.ToString();
            }
            else if (name == DSAttConstants.Watchdog)
            {
                return  en.Watchdog.ToString();
            }
            else if (name == DSAttConstants.TotalPDP)
            {
                return en.GetDPDTotal() + "A";
            }
            else if (name.StartsWith(DSAttConstants.TotalPrefix))
            {
                return en.GetGroupPDPTotal(idTOpdp[name]) + "A";
            }
            else if (name.StartsWith(DSAttConstants.DeltaPrefix))
            {
                return en.GetGroupPDPSd(idTOpdp[name]) + "A";
            }
            return "";
        }
    }
}
