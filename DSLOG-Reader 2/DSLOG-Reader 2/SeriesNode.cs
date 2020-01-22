using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSLOG_Reader_2
{
    public class SeriesNode : ICloneable
    {
        public string Name { get; protected set; }
        public string Text { get; set; }
        public Color Color { get; protected set; }

        public SeriesNode(string name, string text, Color color)
        {
            Name = name;
            Text = text;
            Color = color;
        }

        protected SeriesNode(SeriesNode node)
        {
            this.Name = node.Name;
            this.Text = node.Text;
            this.Color = node.Color;
        }

        public virtual TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(Text);
            node.Name = Name;
            node.BackColor = Color;
            return node;
        }

        public virtual object Clone()
        {
            return new SeriesNode(this);
        }
    }


    public class SeriesChildNode : SeriesNode
    {
       
        public SeriesGroupNode Parent { get; set; }

        public SeriesChildNode(string name, string text, Color backColor, SeriesGroupNode parent) : base (name, text, backColor)
        {
            Name = name;
            Text = text;
            if (parent!= null) parent.AddChild(this);
        }
    }

    public class SeriesGroupNode : SeriesNode
    {
        private List<SeriesChildNode> Childern {  get; set; }

        public SeriesGroupNode(string name, string text, Color backColor) : base (name, text, backColor)
        {
            Childern = new List<SeriesChildNode>();
        }



        public void AddChild(SeriesChildNode child)
        {
            child.Parent = this;
            Childern.Add(child);
        }

        protected SeriesGroupNode(SeriesGroupNode group) : base(group)
        {
            this.Childern = new List<SeriesChildNode>();
            foreach(var child in group.Childern)
            {
                new SeriesChildNode(child.Name, child.Text, child.Color, this);
            }
        }

        public override TreeNode ToTreeNode()
        {
            var node = base.ToTreeNode();
            foreach (var child in Childern)
            {
                node.Nodes.Add(child.ToTreeNode());
            }
            return node;
        }

        public override object Clone()
        {
            return new SeriesGroupNode(this);
        }
    }

    public class GroupProfile : ICloneable
    {
        public string Name { get; set; }
        public SeriesGroupNodes Groups { get; set; }
        public GroupProfile(string name, SeriesGroupNodes groups)
        {
            Name = name;
            Groups = groups;
        }

        public object Clone()
        {
            return new GroupProfile(Name, (SeriesGroupNodes)Groups.Clone());
        }
    }

    public class SeriesGroupNodes : List<SeriesGroupNode>, ICloneable
    {
        public object Clone()
        {
            SeriesGroupNodes nodes = new SeriesGroupNodes();
            foreach (var group in this)
            {
                nodes.Add((SeriesGroupNode)group.Clone());
            }
            return nodes;
        }

        public List<TreeNode> ToTreeNodes()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach(var group in this)
            {
                nodes.Add(group.ToTreeNode());
            }
            return nodes;
        }
    }
}
