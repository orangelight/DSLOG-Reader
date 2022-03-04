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
    public interface SeriesViewObserver
    {
        SeriesView SeriesViewObserving { get; set; }
        void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp);
        void SetEnabledSeries(TreeNodeCollection groups);
    }
    
    public partial class SeriesView : UserControl
    {
        

        private bool checkMode = false;
        private GroupProfiles Profiles;
        private SeriesGroupNodes NonEditGroups;
        private List<SeriesViewObserver> Observers;

        public SeriesView()
        {
            InitializeComponent();
            Observers = new List<SeriesViewObserver>();
            Profiles = new GroupProfiles();
            NonEditGroups = new SeriesGroupNodes();
            toolTip.SetToolTip(comboBoxProfiles, "Change PDP Profile");
            toolTip.SetToolTip(buttonEditGroups, "Edit PDP Profiles");

        }

        public void LoadSeries()
        {
            SeriesGroupNode robotMode = new SeriesGroupNode("robotMode", "Robot Mode", SystemColors.ControlLightLight);
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.DSDisabled, "DS Disabled", Color.DarkGray));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.DSAuto, "DS Auto", Color.Lime));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.DSTele, "DS Tele", Color.Cyan));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.RobotDisabled, "Robot Disabled", Color.DarkGray));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.RobotAuto, "Robot Auto", Color.Lime));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.RobotTele, "Robot Tele", Color.Cyan));

            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.Brownout, "Brownout", Color.OrangeRed));
            robotMode.Childern.Add(new SeriesChildNode(DSAttConstants.Watchdog, "Watchdog", Color.FromArgb(249, 0, 255)));
           
            
            SeriesGroupNode basic = new SeriesGroupNode("basic", "Basic", SystemColors.ControlLightLight);
            basic.Childern.Add(new SeriesChildNode(DSAttConstants.Voltage, "Voltage", Color.Yellow));
            basic.Childern.Add(new SeriesChildNode(DSAttConstants.RoboRIOCPU, "roboRIO CPU", Color.Red));
            basic.Childern.Add(new SeriesChildNode(DSAttConstants.CANUtil, "CAN", Color.Silver));
           


            SeriesGroupNode comms = new SeriesGroupNode("comms", "Comms", SystemColors.ControlLightLight);
            comms.Childern.Add(new SeriesChildNode(DSAttConstants.TripTime, "Trip Time", Color.Lime));
            comms.Childern.Add(new SeriesChildNode(DSAttConstants.LostPackets, "Lost Packets", Color.Chocolate));


            SeriesGroupNode groupNode = null;
            SeriesGroupNodes defG = new SeriesGroupNodes();
            for (int i = 0; i < 24; i++)
            {
                if (groupNode == null)
                {
                    groupNode = new SeriesGroupNode($"grouppdp{i}{i + 4}", $"PDP ({i}-{i + 4})", SystemColors.ControlLightLight);
                }


                groupNode.Childern.Add(new SeriesChildNode($"{DSAttConstants.PDPPrefix}{i}", $"PDP {i}", Util.PdpColors[i]));
                if (groupNode.Childern.Count == 4)
                {
                    defG.Add(groupNode);
                    groupNode = null;
                }
            }
            

            SeriesGroupNode other = new SeriesGroupNode("other", "Other", SystemColors.ControlLightLight);
            other.Childern.Add(new SeriesChildNode(DSAttConstants.Messages, "Messages", Color.Gainsboro));


            other.Childern.Add(new SeriesChildNode(DSAttConstants.TotalPDP, "Total PDP", Color.FromArgb(249, 0, 255)));


            NonEditGroups.Add(comms);
            NonEditGroups.Add(basic);
            NonEditGroups.Add(robotMode);
            NonEditGroups.Add(other);

            treeView.Nodes.Add(comms.ToTreeNode());
            treeView.Nodes.Add(basic.ToTreeNode());
            treeView.Nodes.Add(robotMode.ToTreeNode());
            foreach (var node in defG)
            {
                treeView.Nodes.Add(node.ToTreeNode());
            }
            treeView.Nodes.Add(other.ToTreeNode());

            treeView.ItemHeight = 20;
            treeView.ExpandAll();
            if (File.Exists(".dslogprofiles.xml"))
            {
                FileStream fileStream = null;
                try
                {
                    XmlSerializer profilesSerializer = new XmlSerializer(typeof(GroupProfiles));
                    fileStream = new FileStream(".dslogprofiles.xml", FileMode.Open);
                    Profiles = (GroupProfiles)profilesSerializer.Deserialize(fileStream);
                } catch(Exception ex)
                {
                    MessageBox.Show($"Profile file is corrupted! {ex.Message}");
                    Profiles.Clear();
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

            SetChartSeriesEnabled();
            SetChartSeries();
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
           
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
                                SetChartSeriesEnabled();
                                return;
                            }
                        }
                        e.Parent.Checked = true;
                    }
                }
                SetChartSeriesEnabled();
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
                SetChartSeries();
            }
            
            
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            
            foreach (var group in NonEditGroups)
            {
                var tree = group.ToTreeNode();
                tree.Checked = true;
                foreach(TreeNode node in tree.Nodes)
                {
                    node.Checked = true;
                }
                treeView.Nodes.Add(tree);
                
            }
            treeView.ExpandAll();
            foreach(var group in Profiles[comboBoxProfiles.SelectedIndex].Groups)
            {
                treeView.Nodes.Add(group.ToTreeNode());
            }
            treeView.EndUpdate();
            SetChartSeries();
            SetChartSeriesEnabled();
        }

        private void SetChartSeries()
        {
            foreach(var observer in Observers)
            {
                observer.SetSeries(NonEditGroups, Profiles[comboBoxProfiles.SelectedIndex].Groups);
            }
        }

        public void SetChartSeriesEnabled()
        {
            foreach (var observer in Observers)
            {
                observer.SetEnabledSeries(treeView.Nodes);
            }
        }

        public TreeNodeCollection GetSeries()
        {
            return treeView.Nodes;
        }

        public string GetProfileName()
        {
            return Profiles[comboBoxProfiles.SelectedIndex].Name;
        }

        public void AddObserver(SeriesViewObserver ob)
        {
            Observers.Add(ob);
            ob.SeriesViewObserving = this;
        }
    }
}
