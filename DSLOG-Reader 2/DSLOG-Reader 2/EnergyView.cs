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
                EnergyCache.Add(i, entries.Where(en=> en.Voltage < 30).Sum(en => (en.GetPDPChannel(i) * MillToHour20) * en.Voltage)*3.6);
            }
            EnergyCache.Add(-1, EnergyCache.Sum(ser => ser.Value));
            DisplayEnergy();
        }


        private void ResetTreeView()
        {
            treeView1.Visible = false;
            foreach (TreeNode group in treeView1.Nodes)
            {
                foreach (TreeNode node in group.Nodes)
                {
                    int i = node.Text.LastIndexOf(": ");
                    if (i > 0) node.Text = node.Text.Substring(0, i);
                }
            }
        }

        private void DisplayEnergy()
        {
            treeView1.BeginUpdate();
            ResetTreeView();
            if (EnergyCache.Count != 0 && IdToPDPGroup != null)
            {
                treeView1.Visible = true;
                foreach (TreeNode group in treeView1.Nodes)
                {
                    foreach (TreeNode node in group.Nodes)
                    {
                        if (node.Name.StartsWith(DSAttConstants.PDPPrefix))
                        {
                            node.Text = $"{node.Text}: { EnergyCache[int.Parse(node.Name.Replace(DSAttConstants.PDPPrefix, ""))].ToString("0.00")}kJ";
                        }
                        else if (node.Name == DSAttConstants.TotalPDP)
                        {
                            node.Text = $"{node.Text}:  { EnergyCache[-1].ToString("0.00")}kJ";
                        }
                        else if (node.Name.StartsWith(DSAttConstants.TotalPrefix))
                        {
                            node.Text = $"{node.Text}:  { IdToPDPGroup[node.Name].Sum(s => EnergyCache[s]).ToString("0.00")}kJ";
                        }
                        
                    }
                }
            }
            treeView1.EndUpdate();
        }

        public SeriesView SeriesViewObserving { get; set; }

        public void SetEnabledSeries(TreeNodeCollection groups)
        {
            //DisplayEnergy();
        }

        public void SetSeries(SeriesGroupNodes basic, SeriesGroupNodes pdp)
        {
            treeView1.Nodes.Clear();
            IdToPDPGroup = new Dictionary<string, int[]>();
            TreeNode otherGroup = basic.Find(g => g.Name == "other").ToTreeNode();
            otherGroup.Nodes.RemoveAt(0);
            treeView1.Nodes.Add(otherGroup);

            foreach (var group in pdp)
            {
                if (group.Childern.Count == 0) continue;
                int[] pdpIds = group.Childern.Where(n => n.Name.StartsWith(DSAttConstants.PDPPrefix)).Select(n => int.Parse(n.Name.Replace(DSAttConstants.PDPPrefix, ""))).ToArray();
                TreeNode newGroup = group.ToTreeNode();
                
                foreach (var node in group.Childern)
                {
                    if (node.Name.StartsWith(DSAttConstants.TotalPrefix))
                    {
                        IdToPDPGroup[node.Name] = pdpIds;
                    }
                    if (node.Name.StartsWith(DSAttConstants.DeltaPrefix))
                    {
                        newGroup.Nodes.RemoveByKey(node.Name);
                    }
                }

                foreach(TreeNode node in newGroup.Nodes)
                {
                    node.Text= node.Text + ": ";
                }
                treeView1.Nodes.Add(newGroup);
            }
            treeView1.ExpandAll();
            DisplayEnergy();
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
