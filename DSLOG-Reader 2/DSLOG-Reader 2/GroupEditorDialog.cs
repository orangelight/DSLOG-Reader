using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DSLOG_Reader_2
{
    public partial class GroupEditorDialog : Form
    {
        public GroupProfiles Profiles { get; set; }
        private int lastIndex = -1;
        public bool OK { get; set; }
        private Font TDFont = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Underline);
        public GroupEditorDialog(GroupProfiles profiles)
        {
            InitializeComponent();
            Profiles = profiles;
            OK = false;
            
        }

        private void GroupEditorDialog_Load(object sender, EventArgs e)
        {
            AddProfilesToCombo(0);
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastIndex != comboBoxProfiles.SelectedIndex)
            {
                if (lastIndex > 0)
                {
                    SaveProfile(lastIndex);
                }

                textBoxName.Text = Profiles[comboBoxProfiles.SelectedIndex].Name;
                if (comboBoxProfiles.SelectedIndex == 0)
                {
                    textBoxName.Enabled = false;
                    buttonRemoveProfile.Enabled = false;
                    treeViewPDP.LabelEdit = false;
                    buttonAddGroup.Enabled = false;
                    buttonRemoveGroup.Enabled = false;
                }
                else
                {
                    textBoxName.Enabled = true;
                    buttonRemoveProfile.Enabled = true;
                    treeViewPDP.LabelEdit = true;
                    buttonAddGroup.Enabled = true;
                    CheckGroupTotalAndDelta();
                }
                treeViewPDP.BeginUpdate();
                treeViewPDP.Nodes.Clear();
                treeViewPDP.Nodes.AddRange(Profiles[comboBoxProfiles.SelectedIndex].Groups.ToTreeNodes().ToArray());
                foreach(var node in treeViewPDP.Nodes.Find("total", true))
                {
                    node.NodeFont = TDFont;
                }
                foreach (var node in treeViewPDP.Nodes.Find("delta", true))
                {
                    node.NodeFont = TDFont;
                }
                treeViewPDP.ExpandAll();
                treeViewPDP.EndUpdate();
                lastIndex = comboBoxProfiles.SelectedIndex;
                buttonColor.Enabled = false;
                
            }
            
        }

        private void buttonAddProfile_Click(object sender, EventArgs e)
        {
            GroupProfile newProfile = (GroupProfile)Profiles[0].Clone();
            newProfile.Name = $"New Profile {Profiles.Count}";
            Profiles.Add(newProfile);
            AddProfilesToCombo(Profiles.Count - 1);

        }

        private void AddProfilesToCombo(int selected)
        {
            
            comboBoxProfiles.Items.Clear();
            comboBoxProfiles.Items.AddRange(Profiles.Select(p => p.Name).ToArray());
            comboBoxProfiles.SelectedIndex = selected;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            Profiles[comboBoxProfiles.SelectedIndex].Name = textBoxName.Text;
            comboBoxProfiles.Items[comboBoxProfiles.SelectedIndex] = textBoxName.Text;
        }

        private void buttonRemoveProfile_Click(object sender, EventArgs e)
        {
            Profiles.RemoveAt(comboBoxProfiles.SelectedIndex);
            lastIndex = -1;
            AddProfilesToCombo(0);
        }

        private void treeViewPDP_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex != 0)
            {
                
                if (e.Node.Parent != null)
                {
                    buttonColor.Enabled = true;
                    buttonColor.BackColor = e.Node.BackColor;
                    buttonRemoveGroup.Enabled = false;
                }
                else
                {
                    buttonColor.Enabled = false;
                    if (treeViewPDP.Nodes.Count > 1 && e.Node.Name.StartsWith("group"))
                    {
                        buttonRemoveGroup.Enabled = true;
                    }
                    else
                    {
                        buttonRemoveGroup.Enabled = false;
                    }
                }
                
            }
        }

        private void treeViewPDP_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (((TreeNode)e.Item).Parent == null || ((TreeNode)e.Item).Name == "total" || ((TreeNode)e.Item).Name == "delta" || comboBoxProfiles.SelectedIndex == 0)
            {
                return;
            }
            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeViewPDP_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeViewPDP_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeViewPDP.PointToClient(new Point(e.X, e.Y));
            if (treeViewPDP.GetNodeAt(targetPoint) != null && treeViewPDP.GetNodeAt(targetPoint).Parent != null)
            {
                treeViewPDP.SelectedNode = treeViewPDP.GetNodeAt(targetPoint).Parent;
            }

            if (treeViewPDP.GetNodeAt(targetPoint) != null && treeViewPDP.GetNodeAt(targetPoint).Parent == null)
            {
                treeViewPDP.SelectedNode = treeViewPDP.GetNodeAt(targetPoint);
            }
        }

        private void treeViewPDP_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = treeViewPDP.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeViewPDP.GetNodeAt(targetPoint);
            if (targetNode == null)
            {
                return;
            }
            if (targetNode.Parent != null)
            {
                targetNode = targetNode.Parent;
            }

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    if (NumOfPDPNodes(draggedNode.Parent) <=2)
                    {
                        draggedNode.Parent.Nodes.RemoveByKey("total");
                        draggedNode.Parent.Nodes.RemoveByKey("delta");
                    }
                    
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();
                CheckGroupTotalAndDelta();
            }
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = treeViewPDP.SelectedNode.BackColor;
            colorDialog1.FullOpen = true;
            colorDialog1.CustomColors = Util.PdpColors.Select(c => ColorTranslator.ToOle(c)).ToArray();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog1.Color;
                treeViewPDP.SelectedNode.BackColor = colorDialog1.Color;
            }
        }

        private void treeViewPDP_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //if (e.Node.Parent == null || e.Node.Name == "total" || comboBoxProfiles.SelectedIndex == 0)
            //{
            //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            //}
            //else
            //{
            //    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeAll;
            //}
        }

        private void treeViewPDP_MouseMove(object sender, MouseEventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex == 0)
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                return;
            }
            foreach (TreeNode group in treeViewPDP.Nodes)
            {
                foreach(TreeNode node in group.Nodes)
                {
                    if (node.Bounds.Contains(e.Location) && node.Name != "total" && node.Name != "delta")
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeAll;
                        return;
                    }
                }
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex == 0)
            {
                return;
            }
            TreeNode node = new TreeNode("New Group");
            node.Name = "group";
            node.BackColor = SystemColors.ControlLightLight;
            treeViewPDP.Nodes.Add(node);
            treeViewPDP.SelectedNode = node;
            node.BeginEdit();
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            if (treeViewPDP.SelectedNode.Nodes.Count > 0)
            {
                treeViewPDP.SelectedNode.Nodes.RemoveByKey("total");
                treeViewPDP.SelectedNode.Nodes.RemoveByKey("delta");
                foreach (TreeNode node in treeViewPDP.SelectedNode.Nodes)
                {
                    if (treeViewPDP.SelectedNode.Index == 0)
                    {
                        treeViewPDP.Nodes[1].Nodes.Add((TreeNode)node.Clone());
                    }
                    else
                    {
                        treeViewPDP.Nodes[treeViewPDP.SelectedNode.Index-1].Nodes.Add((TreeNode)node.Clone());
                    }

                }

                
                
            }
            treeViewPDP.SelectedNode.Remove();
            if (treeViewPDP.Nodes.Count <= 1)
            {
                buttonRemoveGroup.Enabled = false;
            }
            treeViewPDP.ExpandAll();
        }

        private void CheckGroupTotalAndDelta()
        {
            if (treeViewPDP.SelectedNode != null && treeViewPDP.SelectedNode.Name.StartsWith("group") && comboBoxProfiles.SelectedIndex != 0 && NumOfPDPNodes(treeViewPDP.SelectedNode) > 1)
            {
                checkBoxDelta.Enabled = true;
                checkBoxTotal.Enabled = true;

                if (treeViewPDP.SelectedNode.Nodes.ContainsKey("total")) checkBoxTotal.Checked = true;
                else checkBoxTotal.Checked = false;

                if (treeViewPDP.SelectedNode.Nodes.ContainsKey("delta")) checkBoxDelta.Checked = true;
                else checkBoxDelta.Checked = false;
            }
            else
            {
                checkBoxDelta.Enabled = false;
                checkBoxTotal.Enabled = false;
            }
        }

        private void treeViewPDP_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CheckGroupTotalAndDelta();
            if (treeViewPDP.SelectedNode != null && treeViewPDP.SelectedNode.Name.StartsWith("pdp"))
            {
                labelPDPSlot.Text = $"PDP Slot: {treeViewPDP.SelectedNode.Name.Replace("pdp", "")}";
            }
            else
            {
                labelPDPSlot.Text = $"PDP Slot:";
            }
        }

        private void checkBoxTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (!treeViewPDP.SelectedNode.Nodes.ContainsKey("total") && checkBoxTotal.Checked)
            {
                TreeNode node = new TreeNode("Group Total");
                node.BackColor = Color.LawnGreen;
                node.Name = "total";
                node.NodeFont = TDFont;
                treeViewPDP.SelectedNode.Nodes.Add(node);
            }
            else if (treeViewPDP.SelectedNode.Nodes.ContainsKey("total") && !checkBoxTotal.Checked)
            {
                treeViewPDP.SelectedNode.Nodes.RemoveByKey("total");
            }
        }

        private void checkBoxDelta_CheckedChanged(object sender, EventArgs e)
        {
            if (!treeViewPDP.SelectedNode.Nodes.ContainsKey("delta") && checkBoxDelta.Checked)
            {
                TreeNode node = new TreeNode("Group Delta");
                node.BackColor = Color.Magenta;
                node.NodeFont = TDFont;
                node.Name = "delta";
                treeViewPDP.SelectedNode.Nodes.Add(node);
            }
            else if (treeViewPDP.SelectedNode.Nodes.ContainsKey("delta") && !checkBoxDelta.Checked)
            {
                treeViewPDP.SelectedNode.Nodes.RemoveByKey("delta");
            }
        }

        private int NumOfPDPNodes(TreeNode parent)
        {
            int i = 0;
            foreach(TreeNode node in parent.Nodes)
            {
                if (!node.Name.StartsWith("group") && node.Name != "total" && node.Name != "delta")
                {
                    i++;
                }
            }
            return i;
        }


        private void buttonCopyProfile_Click(object sender, EventArgs e)
        {
            SaveProfile(comboBoxProfiles.SelectedIndex);
            GroupProfile newProfile = (GroupProfile)Profiles[comboBoxProfiles.SelectedIndex].Clone();
            newProfile.Name = $"{comboBoxProfiles.SelectedItem.ToString()} Copy";
            Profiles.Add(newProfile);
            AddProfilesToCombo(Profiles.Count - 1);
        }

        private SeriesGroupNodes TreeNodesToSeriesGroupNodes(TreeNodeCollection nodes)
        {
            SeriesGroupNodes groups = new SeriesGroupNodes();
            foreach(TreeNode treeGroup in nodes)
            {
                if (treeGroup.Nodes.Count == 0) continue;
                SeriesGroupNode groupNode = new SeriesGroupNode(treeGroup.Name, treeGroup.Text, treeGroup.BackColor);
                foreach(TreeNode treeChild in treeGroup.Nodes)
                {
                    groupNode.Childern.Add(new SeriesChildNode(treeChild.Name, treeChild.Text, treeChild.BackColor));
                }
                groups.Add(groupNode);
            }
            return groups;
        }

        private void buttonProfileSave_Click(object sender, EventArgs e)
        {
            
        }

        private void SaveProfile(int index = -1)
        {
            if (index == -1) Profiles[comboBoxProfiles.SelectedIndex].Groups = TreeNodesToSeriesGroupNodes(treeViewPDP.Nodes);
            else Profiles[index].Groups = TreeNodesToSeriesGroupNodes(treeViewPDP.Nodes); 
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            SaveProfile();
            OK = true;
            XmlSerializer profilesSerializer = new XmlSerializer(typeof(GroupProfiles));
            var fileStream = new StreamWriter(".dslogsettings.xml", false);
            profilesSerializer.Serialize(fileStream, Profiles);
            fileStream.Close();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Close();
        }

        private void treeViewPDP_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
