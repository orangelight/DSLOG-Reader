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
using System.Xml.Serialization;
using System.IO;

namespace DSLOG_Reader_2
{
    
    public partial class SeriesView : UserControl
    {
        

        private bool checkMode = false;
        private GroupProfiles Profiles;
        private SeriesGroupNodes NonEditGroups;

        public SeriesView()
        {
            InitializeComponent();
            Profiles = new GroupProfiles();
            NonEditGroups = new SeriesGroupNodes();

        }

        public void LoadSeries()
        {
            SeriesGroupNode robotMode = new SeriesGroupNode("robotMode", "Robot Mode", SystemColors.ControlLightLight);
            robotMode.Childern.Add(new SeriesChildNode("dsDisabled", "DS Disabled", Color.DarkGray));
            robotMode.Childern.Add(new SeriesChildNode("dsAuto", "DS Auto", Color.Lime));
            robotMode.Childern.Add(new SeriesChildNode("dsTele", "DS Tele", Color.Cyan));
            robotMode.Childern.Add(new SeriesChildNode("robotDisabled", "Robot Disabled", Color.DarkGray));
            robotMode.Childern.Add(new SeriesChildNode("robotAuto", "Robot Auto", Color.Lime));
            robotMode.Childern.Add(new SeriesChildNode("robotTele", "Robot Tele", Color.Cyan));

            robotMode.Childern.Add(new SeriesChildNode("brownout", "Brownout", Color.OrangeRed));
            robotMode.Childern.Add(new SeriesChildNode("watchdog", "Watchdog", Color.FromArgb(249, 0, 255)));
           
            
            SeriesGroupNode basic = new SeriesGroupNode("basic", "Basic", SystemColors.ControlLightLight);
            basic.Childern.Add(new SeriesChildNode("voltage", "Voltage", Color.Yellow));


            basic.Childern.Add(new SeriesChildNode("roboRIOCPU", "roboRIO CPU", Color.Red));


            basic.Childern.Add(new SeriesChildNode("can", "CAN", Color.Silver));
           


            SeriesGroupNode comms = new SeriesGroupNode("comms", "Comms", SystemColors.ControlLightLight);
            comms.Childern.Add(new SeriesChildNode("tripTime", "Trip Time", Color.Lime));


            comms.Childern.Add(new SeriesChildNode("lostPackets", "Lost Packets", Color.Chocolate));


            SeriesGroupNode pdp03 = new SeriesGroupNode("grouppdp03", "PDP (0-3) 40A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp47 = new SeriesGroupNode("grouppdp47", "PDP (4-7) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp811 = new SeriesGroupNode("grouppdp811", "PDP (8-11) 30A", SystemColors.ControlLightLight);
            SeriesGroupNode pdp1215 = new SeriesGroupNode("grouppdp1215", "PDP (12-15) 40A", SystemColors.ControlLightLight);
            List<SeriesChildNode> pdps = new List<SeriesChildNode>();
            for(int i = 0; i < 16; i++)
            {
                if (i < 4)
                {
                    pdp03.Childern.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i]));
                }
                else if(i < 8)
                {
                    pdp47.Childern.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i]));
                }
                else if (i < 12)
                {
                    pdp811.Childern.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i]));
                }
                else
                {
                    pdp1215.Childern.Add(new SeriesChildNode($"pdp{i}", $"PDP {i}", Util.PdpColors[i]));
                }
            }

            SeriesGroupNodes defG = new SeriesGroupNodes();
            defG.Add(pdp03);
            defG.Add(pdp47);
            defG.Add(pdp811);
            defG.Add(pdp1215);
            

            SeriesGroupNode other = new SeriesGroupNode("other", "Other", SystemColors.ControlLightLight);
            other.Childern.Add(new SeriesChildNode("messages", "Messages", Color.Gainsboro));


            other.Childern.Add(new SeriesChildNode("totalPdp", "Total PDP", Color.FromArgb(249, 0, 255)));


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
            if (File.Exists(".dslogsettings.xml"))
            {
                FileStream fileStream = null;
                try
                {
                    XmlSerializer profilesSerializer = new XmlSerializer(typeof(GroupProfiles));
                    fileStream = new FileStream(".dslogsettings.xml", FileMode.Open);
                    Profiles = (GroupProfiles)profilesSerializer.Deserialize(fileStream);
                } catch(Exception ex)
                {
                    MessageBox.Show($"Setting file is corrupted! {ex.Message}");
                    Profiles.Clear();
                    Profiles.Add(new GroupProfile("Default", defG));
                }
                finally
                {
                    if (fileStream != null) fileStream.Close();
                }
                
                
            }
            else
            {
                Profiles.Clear();
                Profiles.Add(new GroupProfile("Default", defG));
            }
            
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
            GroupEditorDialog groupEditor = new GroupEditorDialog((GroupProfiles)Profiles.Clone());
            groupEditor.ShowInTaskbar = false;
            groupEditor.ShowDialog();
            if (groupEditor.OK)
            {
                Profiles = groupEditor.Profiles;
                comboBoxProfiles.Items.Clear();
                comboBoxProfiles.Items.AddRange(Profiles.Select(p => p.Name).ToArray());
                comboBoxProfiles.SelectedIndex = 0;
            }
            
            
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach(var group in NonEditGroups)
            {
                treeView.Nodes.Add(group.ToTreeNode());
                
            }
            treeView.ExpandAll();
            foreach(var group in Profiles[comboBoxProfiles.SelectedIndex].Groups)
            {
                treeView.Nodes.Add(group.ToTreeNode());
            }
            treeView.EndUpdate();
        }
    }
}
