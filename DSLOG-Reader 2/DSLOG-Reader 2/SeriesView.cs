using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSLOG_Reader_2
{
    
    public partial class SeriesView : UserControl
    {
        Color[] pdpColors = { Color.FromArgb(255, 113, 113), Color.FromArgb(255, 198, 89), Color.FromArgb(152, 255, 136), Color.FromArgb(136, 154, 255), Color.FromArgb(255, 52, 42), Color.FromArgb(255, 176, 42), Color.FromArgb(0, 255, 9), Color.FromArgb(0, 147, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(239, 139, 0), Color.FromArgb(46, 220, 0), Color.FromArgb(57, 42, 255), Color.FromArgb(180, 8, 0), Color.FromArgb(200, 132, 0), Color.FromArgb(42, 159, 0), Color.FromArgb(0, 47, 239) };

        private bool checkMode = false;
        private Dictionary<string, SeriesChildNode> SeriesNodes;

        public SeriesView()
        {
            InitializeComponent();
            SeriesNodes = new Dictionary<string, SeriesChildNode>();
        }

        public void LoadSeries()
        {
            SeriesGroupNode robotMode = new SeriesGroupNode("robotMode", "Robot Mode", SystemColors.ControlLightLight);

            List<SeriesChildNode> modes = new List<SeriesChildNode>();
            modes.Add(new SeriesChildNode("dsDisabled", "DS Disabled", Color.DarkGray, false, robotMode));
            modes.Add(new SeriesChildNode("dsAuto", "DS Auto", Color.Lime, false, robotMode));
            modes.Add(new SeriesChildNode("dsTele", "DS Tele", Color.Cyan, false, robotMode));
            modes.Add(new SeriesChildNode("robotDisabled", "Robot Disabled", Color.DarkGray, false, robotMode));
            modes.Add(new SeriesChildNode("robotAuto", "Robot Auto", Color.Lime, false, robotMode));
            modes.Add(new SeriesChildNode("robotTele", "Robot Tele", Color.Cyan, false, robotMode));

            modes.Add(new SeriesChildNode("brownout", "Brownout", Color.OrangeRed, false, robotMode));
            modes.Add(new SeriesChildNode("watchdog", "Watchdog", Color.FromArgb(249, 0, 255), false, robotMode));
            foreach(var mode in modes)
            {
                mode.ChartType = SeriesChartType.Line;
                mode.XValueType = ChartValueType.DateTime;
                mode.YAxisType = AxisType.Primary;
                mode.BoarderWidth = 5;
                
            }
            SeriesGroupNode basic = new SeriesGroupNode("basic", "Basic", SystemColors.ControlLightLight);
            var voltage = new SeriesChildNode("voltage", "Voltage", Color.Yellow, false, basic);
            voltage.XValueType = ChartValueType.DateTime;
            voltage.YAxisType = AxisType.Primary;
            voltage.ChartType = SeriesChartType.FastLine;

            var roboRIOCPU = new SeriesChildNode("roboRIOCPU", "roboRIO CPU", Color.Red, false, basic);
            roboRIOCPU.XValueType = ChartValueType.DateTime;
            roboRIOCPU.YAxisType = AxisType.Secondary;
            roboRIOCPU.ChartType = SeriesChartType.FastLine;

            var can = new SeriesChildNode("can", "CAN", Color.Silver, false, basic);
            can.XValueType = ChartValueType.DateTime;
            can.YAxisType = AxisType.Secondary;
            can.ChartType = SeriesChartType.FastLine;


            SeriesGroupNode comms = new SeriesGroupNode("comms", "Comms", SystemColors.ControlLightLight);
            var tripTime = new SeriesChildNode("tripTime", "Trip Time", Color.Lime, false, comms);
            tripTime.XValueType = ChartValueType.DateTime;
            tripTime.YAxisType = AxisType.Secondary;
            tripTime.ChartType = SeriesChartType.FastLine;

            var lostPackets = new SeriesChildNode("lostPackets", "Lost Packets", Color.Chocolate, false, comms);
            lostPackets.XValueType = ChartValueType.DateTime;
            lostPackets.YAxisType = AxisType.Secondary;
            lostPackets.ChartType = SeriesChartType.FastLine;

            SeriesGroupNode pdp03 = new SeriesGroupNode("pdp03", "PDP (0-3) 40A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp47 = new SeriesGroupNode("pdp47", "PDP (4-7) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp811 = new SeriesGroupNode("pdp811", "PDP (8-11) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp1215 = new SeriesGroupNode("pdp1215", "PDP (12-15) 40A", SystemColors.ControlLightLight);
            List<SeriesChildNode> pdps = new List<SeriesChildNode>();
            for(int i = 0; i < 16; i++)
            {
                if (i < 4)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", pdpColors[i], true, pdp03));
                }
                else if(i < 8)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", pdpColors[i], true, pdp47));
                }
                else if (i < 12)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", pdpColors[i], true, pdp811));
                }
                else
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", pdpColors[i], true, pdp1215));
                }
            }

            foreach(var node in pdps)
            {
                node.XValueType = ChartValueType.DateTime;
                node.YAxisType = AxisType.Secondary;
                node.ChartType = SeriesChartType.FastLine;
            }

            var messages = new SeriesChildNode("messages", "Messages", Color.Gainsboro, false, null);
            messages.XValueType = ChartValueType.DateTime;
            messages.YAxisType = AxisType.Primary;
            messages.ChartType = SeriesChartType.Point;
            messages.MarkerStyle = MarkerStyle.Circle;

            var totalPdp = new SeriesChildNode("totalPdp", "Total PDP", Color.FromArgb(249, 0, 255), false, null);
            totalPdp.XValueType = ChartValueType.DateTime;
            totalPdp.YAxisType = AxisType.Secondary;
            totalPdp.ChartType = SeriesChartType.FastLine;


            treeView.Nodes.Add(comms.ToTreeNode());
            treeView.Nodes.Add(basic.ToTreeNode());
            treeView.Nodes.Add(robotMode.ToTreeNode());
            treeView.Nodes.Add(pdp03.ToTreeNode());
            treeView.Nodes.Add(pdp47.ToTreeNode());
            treeView.Nodes.Add(pdp811.ToTreeNode());
            treeView.Nodes.Add(pdp1215.ToTreeNode());
            treeView.Nodes.Add(totalPdp.ToTreeNode());
            treeView.Nodes.Add(messages.ToTreeNode());

            treeView.ItemHeight = 20;
            treeView.ExpandAll();
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Checked = !e.Node.Checked;
            DoChecking(e.Node);
            e.Cancel = true;
        }

        private void DoChecking(TreeNode e)
        {
            if (!checkMode)
            {
                checkMode = true;
                if (e.Nodes.Count > 0 || e.Parent == null)//parent
                {
                    foreach (TreeNode node in e.Nodes)
                    {
                        node.Checked = e.Checked;
                    }
                }
                else//child
                {
                    if (!e.Checked)
                    {
                        e.Parent.Checked = false;
                    }
                    if (e.Checked)
                    {
                        foreach (TreeNode node in e.Parent.Nodes)
                        {
                            if (!node.Checked)
                            {
                                e.Parent.Checked = false;
                                checkMode = false;
                                return;
                            }
                        }
                        e.Parent.Checked = true;
                    }
                }

                checkMode = false;
            }
        }
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown) return;

            DoChecking(e.Node);
        }
        public class SeriesTreeView : TreeView
        {
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x0203)
                {
                    m.Result = IntPtr.Zero;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
        }
    }
}
