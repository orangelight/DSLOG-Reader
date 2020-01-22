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
        

        private bool checkMode = false;
        private List<GroupProfile> Profiles;
        private SeriesGroupNodes NonEditGroups;

        public SeriesView()
        {
            InitializeComponent();
            Profiles = new List<GroupProfile>();
            NonEditGroups = new SeriesGroupNodes();

        }

        public void LoadSeries()
        {
            SeriesGroupNode robotMode = new SeriesGroupNode("robotMode", "Robot Mode", SystemColors.ControlLightLight);

            List<SeriesChildNode> modes = new List<SeriesChildNode>();
            modes.Add(new SeriesChildNode("dsDisabled", "DS Disabled", Color.DarkGray, robotMode));
            modes.Add(new SeriesChildNode("dsAuto", "DS Auto", Color.Lime, robotMode));
            modes.Add(new SeriesChildNode("dsTele", "DS Tele", Color.Cyan, robotMode));
            modes.Add(new SeriesChildNode("robotDisabled", "Robot Disabled", Color.DarkGray, robotMode));
            modes.Add(new SeriesChildNode("robotAuto", "Robot Auto", Color.Lime, robotMode));
            modes.Add(new SeriesChildNode("robotTele", "Robot Tele", Color.Cyan, robotMode));

            modes.Add(new SeriesChildNode("brownout", "Brownout", Color.OrangeRed, robotMode));
            modes.Add(new SeriesChildNode("watchdog", "Watchdog", Color.FromArgb(249, 0, 255), robotMode));
           
            
            SeriesGroupNode basic = new SeriesGroupNode("basic", "Basic", SystemColors.ControlLightLight);
            var voltage = new SeriesChildNode("voltage", "Voltage", Color.Yellow,  basic);
           

            var roboRIOCPU = new SeriesChildNode("roboRIOCPU", "roboRIO CPU", Color.Red, basic);
            

            var can = new SeriesChildNode("can", "CAN", Color.Silver, basic);
           


            SeriesGroupNode comms = new SeriesGroupNode("comms", "Comms", SystemColors.ControlLightLight);
            var tripTime = new SeriesChildNode("tripTime", "Trip Time", Color.Lime, comms);


            var lostPackets = new SeriesChildNode("lostPackets", "Lost Packets", Color.Chocolate, comms);


            SeriesGroupNode pdp03 = new SeriesGroupNode("grouppdp03", "PDP (0-3) 40A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp47 = new SeriesGroupNode("grouppdp47", "PDP (4-7) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp811 = new SeriesGroupNode("grouppdp811", "PDP (8-11) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp1215 = new SeriesGroupNode("grouppdp1215", "PDP (12-15) 40A", SystemColors.ControlLightLight);
            List<SeriesChildNode> pdps = new List<SeriesChildNode>();
            for(int i = 0; i < 16; i++)
            {
                if (i < 4)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i], pdp03));
                }
                else if(i < 8)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i], pdp47));
                }
                else if (i < 12)
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i], pdp811));
                }
                else
                {
                    pdps.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i], pdp1215));
                }
            }

            SeriesGroupNodes defG = new SeriesGroupNodes();
            defG.Add(pdp03);
            defG.Add(pdp47);
            defG.Add(pdp811);
            defG.Add(pdp1215);
            Profiles.Add(new GroupProfile("Default", defG));

            SeriesGroupNode other = new SeriesGroupNode("other", "Other", SystemColors.ControlLightLight);
            var messages = new SeriesChildNode("messages", "Messages", Color.Gainsboro, other);


            var totalPdp = new SeriesChildNode("totalPdp", "Total PDP", Color.FromArgb(249, 0, 255), other);


            NonEditGroups.Add(comms);
            NonEditGroups.Add(basic);
            NonEditGroups.Add(robotMode);
            NonEditGroups.Add(other);

            treeView.Nodes.Add(comms.ToTreeNode());
            treeView.Nodes.Add(basic.ToTreeNode());
            treeView.Nodes.Add(robotMode.ToTreeNode());
            treeView.Nodes.Add(pdp03.ToTreeNode());
            treeView.Nodes.Add(pdp47.ToTreeNode());
            treeView.Nodes.Add(pdp811.ToTreeNode());
            treeView.Nodes.Add(pdp1215.ToTreeNode());
            treeView.Nodes.Add(other.ToTreeNode());

            treeView.ItemHeight = 20;
            treeView.ExpandAll();
            comboBoxProfiles.Items.Clear();
            comboBoxProfiles.Items.AddRange(Profiles.Select(e=> e.Name).ToArray());
            comboBoxProfiles.SelectedIndex = 0;
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

        private void buttonEditGroups_Click(object sender, EventArgs e)
        {
            GroupEditorDialog groupEditor = new GroupEditorDialog(Profiles);
            groupEditor.ShowInTaskbar = false;
            groupEditor.ShowDialog();
            comboBoxProfiles.Items.Clear();
            comboBoxProfiles.Items.AddRange(Profiles.Select(p => p.Name).ToArray());
            comboBoxProfiles.SelectedIndex = 0;
        }
    }
}
