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
                var groups = Profiles[comboBoxProfiles.SelectedIndex].Groups.ToTreeNodes().ToArray();
                foreach(var group in groups)
                {
                    foreach(TreeNode node in group.Nodes)
                    {
                        if (node.Name.StartsWith(DSAttConstants.TotalPrefix)) node.Name = DSAttConstants.TotalPrefix;
                        if (node.Name.StartsWith(DSAttConstants.DeltaPrefix)) node.Name = DSAttConstants.DeltaPrefix;
                    }
                }
                treeViewPDP.Nodes.AddRange(groups);
                foreach(var node in treeViewPDP.Nodes.Find(DSAttConstants.TotalPrefix, true))
                {
                    node.NodeFont = TDFont;
                }
                foreach (var node in treeViewPDP.Nodes.Find(DSAttConstants.DeltaPrefix, true))
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
            if (((TreeNode)e.Item).Name == DSAttConstants.TotalPrefix || ((TreeNode)e.Item).Name == DSAttConstants.DeltaPrefix || comboBoxProfiles.SelectedIndex == 0)
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

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode.Parent == null)
            {
                if (targetNode.Parent != null)
                {
                    return;
                }

                if (targetNode.Index == 0)
                {
                    draggedNode.Remove();
                    treeViewPDP.Nodes.Insert(0, draggedNode);
                }
                else
                {

                    if (draggedNode.Index > targetNode.Index)
                    {
                        draggedNode.Remove();
                        treeViewPDP.Nodes.Insert(targetNode.Index, draggedNode);
                    }
                    else
                    {
                        draggedNode.Remove();
                        treeViewPDP.Nodes.Insert(targetNode.Index + 1, draggedNode);
                    }


                }
            }
            else
            {
                if (targetNode.Parent != null)
                {
                    targetNode = targetNode.Parent;
                }

                // Retrieve the node that was dragged.
                

                // Confirm that the node at the drop location is not 
                // the dragged node or a descendant of the dragged node.
                if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
                {
                    // If it is a move operation, remove the node from its current 
                    // location and add it to the node at the drop location.
                    if (e.Effect == DragDropEffects.Move)
                    {
                        if (NumOfPDPNodes(draggedNode.Parent) <= 2)
                        {
                            draggedNode.Parent.Nodes.RemoveByKey(DSAttConstants.TotalPrefix);
                            draggedNode.Parent.Nodes.RemoveByKey(DSAttConstants.DeltaPrefix);
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
                    if (node.Bounds.Contains(e.Location) && node.Name != DSAttConstants.TotalPrefix && node.Name != DSAttConstants.DeltaPrefix)
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
                treeViewPDP.SelectedNode.Nodes.RemoveByKey(DSAttConstants.TotalPrefix);
                treeViewPDP.SelectedNode.Nodes.RemoveByKey(DSAttConstants.DeltaPrefix);
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

                if (treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.TotalPrefix)) checkBoxTotal.Checked = true;
                else checkBoxTotal.Checked = false;

                if (treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.DeltaPrefix)) checkBoxDelta.Checked = true;
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
            if (treeViewPDP.SelectedNode != null && treeViewPDP.SelectedNode.Name.StartsWith(DSAttConstants.PDPPrefix))
            {
                labelPDPSlot.Text = $"PDP Slot: {treeViewPDP.SelectedNode.Name.Replace(DSAttConstants.PDPPrefix, "")}";
            }
            else
            {
                labelPDPSlot.Text = $"PDP Slot:";
            }
        }

        private void checkBoxTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (!treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.TotalPrefix) && checkBoxTotal.Checked)
            {
                TreeNode node = new TreeNode($"{treeViewPDP.SelectedNode.Text} Total");
                node.BackColor = Color.LawnGreen;
                node.Name = DSAttConstants.TotalPrefix;
                node.NodeFont = TDFont;
                treeViewPDP.SelectedNode.Nodes.Add(node);
            }
            else if (treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.TotalPrefix) && !checkBoxTotal.Checked)
            {
                treeViewPDP.SelectedNode.Nodes.RemoveByKey(DSAttConstants.TotalPrefix);
            }
        }

        private void checkBoxDelta_CheckedChanged(object sender, EventArgs e)
        {
            if (!treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.DeltaPrefix) && checkBoxDelta.Checked)
            {
                TreeNode node = new TreeNode($"{treeViewPDP.SelectedNode.Text} Delta");
                node.BackColor = Color.Magenta;
                node.NodeFont = TDFont;
                node.Name = DSAttConstants.DeltaPrefix;
                treeViewPDP.SelectedNode.Nodes.Add(node);
            }
            else if (treeViewPDP.SelectedNode.Nodes.ContainsKey(DSAttConstants.DeltaPrefix) && !checkBoxDelta.Checked)
            {
                treeViewPDP.SelectedNode.Nodes.RemoveByKey(DSAttConstants.DeltaPrefix);
            }
        }

        private int NumOfPDPNodes(TreeNode parent)
        {
            int i = 0;
            foreach(TreeNode node in parent.Nodes)
            {
                if (!node.Name.StartsWith("group") && node.Name != DSAttConstants.TotalPrefix && node.Name != DSAttConstants.DeltaPrefix)
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
            int totalDeltaID = 0;
            SeriesGroupNodes groups = new SeriesGroupNodes();
            foreach(TreeNode treeGroup in nodes)
            {
                if (treeGroup.Nodes.Count == 0) continue;
                SeriesGroupNode groupNode = new SeriesGroupNode(treeGroup.Name, treeGroup.Text, treeGroup.BackColor);
                foreach(TreeNode treeChild in treeGroup.Nodes)
                {
                    if (treeChild.Name.Contains(DSAttConstants.TotalPrefix) || treeChild.Name.Contains(DSAttConstants.DeltaPrefix))
                    {
                        groupNode.Childern.Add(new SeriesChildNode($"{treeChild.Name}{totalDeltaID++}", treeChild.Text, treeChild.BackColor));
                    }
                    else
                    {
                        groupNode.Childern.Add(new SeriesChildNode(treeChild.Name, treeChild.Text, treeChild.BackColor));
                    }
                    
                }
                groups.Add(groupNode);
            }
            return groups;
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
            var fileStream = new StreamWriter(Util.ProfilesFile, false);
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

        private void treeViewPDP_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != null && e.Label != null && (e.Label.Contains(":") || e.Node.Name.StartsWith(DSAttConstants.DeltaPrefix) || e.Node.Name.StartsWith(DSAttConstants.TotalPrefix)))
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Node != null && e.Node.Nodes.Count > 0)
            {
                var totals = e.Node.Nodes.Find(DSAttConstants.TotalPrefix, true);
                foreach(var total in totals)
                {
                    total.Text = $"{e.Label} Total";
                }
                var deltas = e.Node.Nodes.Find(DSAttConstants.DeltaPrefix, true);
                foreach (var delta in deltas)
                {
                    delta.Text = $"{e.Label} Delta";
                }
            }

          
        }
    }
}
