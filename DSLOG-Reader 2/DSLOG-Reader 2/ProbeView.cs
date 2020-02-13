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
    public partial class ProbeView : UserControl, SeriesViewObserver
    {
        private DSLOGEntry Entry;
        private Dictionary<string, int[]> IdToPDPGroup;
        public MainGraphView MainGraph { get; set; }
        public ProbeView()
        {
            InitializeComponent();
        }

        public SeriesView SeriesViewObserving { get; set; }

      

        public void SetProbe(DSLOGEntry entry)
        {
            Entry = entry;
            DisplayEntry();
        }

        

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            var timeNode = new TreeNode();
            timeNode.Text = "Time:";
            timeNode.Name = "time";
            treeView1.Nodes.Add(timeNode);

            foreach (TreeNode group in groups)
            {
                bool keepGroup = false;
                foreach (TreeNode node in group.Nodes)
                {
                    if (node.Checked && node.Name != DSAttConstants.Messages)
                    {
                        keepGroup = true;
                        break;
                    }            
                }
                if (!keepGroup) continue;
                TreeNode newGroup = group.CopyTreeNode();
                foreach (TreeNode node in group.Nodes)
                {
                    if (node.Checked && node.Name != DSAttConstants.Messages)
                    {
                        TreeNode newNode = node.CopyTreeNode();
                        newGroup.Nodes.Add(newNode);
                    }
                }
                treeView1.Nodes.Add(newGroup);
                treeView1.ExpandAll();

            }
            treeView1.EndUpdate();
            DisplayEntry();
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
            //DisplayEntry();
        }
        private void ResetTreeView()
        {
            treeView1.Visible = false;
            foreach (TreeNode group in treeView1.Nodes)
            {
                if (group.Name == "time")
                {
                    group.Text = $"Time:";
                    continue;
                }
                foreach (TreeNode node in group.Nodes)
                {
                    int i = node.Text.LastIndexOf(": ");
                    if (i > 0) node.Text = node.Text.Substring(0, i);
                }
            }
        }
        private void DisplayEntry()
        {
            treeView1.BeginUpdate();
            ResetTreeView();
            if (Entry != null && IdToPDPGroup != null)
            {
               
                foreach (TreeNode group in treeView1.Nodes)
                {
                    if (group.Name == "time")
                    {
                        if (MainGraph.UseMatchTime && MainGraph.CanUseMatchTime)
                        {
                            group.Text = $"Time: {((Entry.Time-MainGraph.MatchTime).TotalMilliseconds / 1000.0).ToString("0.###")}";
                        }
                        else
                        {
                            group.Text = $"Time: {Entry.Time.ToString("HH:mm:ss.fff")}";
                        }
                        
                        continue;
                    }
                    foreach(TreeNode node in group.Nodes)
                    {
                        node.Text = $"{node.Text}: { Entry.EntryAttToUnit(node.Name, IdToPDPGroup)}";
                    }
                }
                treeView1.Visible = true;
            }
            treeView1.EndUpdate();
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
