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

namespace DSLOG_Reader_2
{
    public partial class EnergyView : UserControl, SeriesViewObserver
    {
        private Dictionary<string, int[]> IdToPDPGroup;
        private Dictionary<string, Tuple<string, Color>> EnabledSeries = new Dictionary<string, Tuple<string, Color>>();
        private Dictionary<int, double> EnergyCache = new Dictionary<int, double>();
        private const double MillToHour20 = 0.00000556;
        public EnergyView()
        {
            InitializeComponent();
        }

        public void SetEnergy(IEnumerable<DSLOGEntry> entries)
        {
            EnergyCache.Clear();
            if (entries == null) return;
            for (int i = 0; i < 16; i++)
            {
                EnergyCache.Add(i, entries.Sum(en => (en.GetPDPChannel(i) * MillToHour20) * en.Voltage)*3.6);
            }
            EnergyCache.Add(-1, EnergyCache.Sum(ser => ser.Value));
            DisplayEnergy();
        }

        public void DisplayEnergy()
        {
            Controls.Clear();
            Font labelFont = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold);
            if (EnergyCache.Count != 0 && IdToPDPGroup != null)
            {
                int labelNum = 0;
                foreach (var ser in EnabledSeries)
                {
                    Label seriesLabel = new Label();
                    if (ser.Key.StartsWith(DSAttConstants.PDPPrefix))
                    {
                        seriesLabel.Text = $"{ser.Value.Item1}: { EnergyCache[int.Parse(ser.Key.Replace(DSAttConstants.PDPPrefix, ""))].ToString("0.00")}kJ";
                    }
                    else if(ser.Key == DSAttConstants.TotalPDP)
                    {
                        seriesLabel.Text = $"{ser.Value.Item1}: { EnergyCache[-1].ToString("0.00")}kJ";
                    }
                    else if (ser.Key.StartsWith(DSAttConstants.TotalPrefix))
                    {
                        seriesLabel.Text = $"{ser.Value.Item1}: { IdToPDPGroup[ser.Key].Sum(s => EnergyCache[s]).ToString("0.00")}kJ";  
                    }
                    else
                    {
                        continue;
                    }
                    
                    seriesLabel.Visible = true;
                    seriesLabel.AutoSize = true;
                    //seriesLabel.BackColor = ser.Value.Item2;
                    seriesLabel.Location = new Point(4, ((seriesLabel.Height) * labelNum++) + 7);
                    Controls.Add(seriesLabel);
                }
            }
        }

        public SeriesView SeriesViewObserving { get; set; }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            EnabledSeries.Clear();
            foreach (TreeNode group in groups)
            {
                foreach (TreeNode node in group.Nodes)
                {
                    if ((node.Name.StartsWith(DSAttConstants.PDPPrefix) || node.Name.StartsWith(DSAttConstants.TotalPrefix) || node.Name == DSAttConstants.TotalPDP) && node.Checked)
                    {
                        EnabledSeries.Add(node.Name, new Tuple<string, Color>(node.Text, node.BackColor));
                    }
                }
            }
            DisplayEnergy();
        }

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            IdToPDPGroup = new Dictionary<string, int[]>();
            foreach (var group in pdp)
            {
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith(DSAttConstants.PDPPrefix)).Select(n => int.Parse(n.Name.Replace(DSAttConstants.PDPPrefix, ""))).ToArray();
                foreach (var node in group.Childern)
                {
                    if (node.Name.StartsWith(DSAttConstants.TotalPrefix) || node.Name.Contains(DSAttConstants.DeltaPrefix))
                    {
                        IdToPDPGroup[node.Name] = pdpIds;
                    }
                }
            }
            DisplayEnergy();
        }
    }
}
