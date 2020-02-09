using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;

namespace DSLOG_Reader_2
{
    public class SeriesNode : ICloneable
    {
        public string Name { get; set; }
        public string Text { get; set; }

        [XmlIgnore]
        public Color Color { get; set; }

        [XmlElement("Color")]
        public int BackColorAsArgb
        {
            get { return Color.ToArgb(); }
            set { Color = Color.FromArgb(value); }
        }

        public SeriesNode(string name, string text, Color color)
        {
            Name = name;
            Text = text;
            Color = color;
        }

        public SeriesNode()
        {

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
        public SeriesChildNode(string name, string text, Color backColor) : base (name, text, backColor)
        {
            Name = name;
            Text = text;
        }

        public SeriesChildNode()
        {

        }
    }

    public class SeriesGroupNode : SeriesNode
    {
        public List<SeriesChildNode> Childern {  get; set; }

        public SeriesGroupNode(string name, string text, Color backColor) : base (name, text, backColor)
        {
            Childern = new List<SeriesChildNode>();
        }

        public SeriesGroupNode()
        {

        }

        internal void AddChild(SeriesChildNode child)
        {
            //child.Parent = this;
            //Childern.Add(child);
        }

        protected SeriesGroupNode(SeriesGroupNode group) : base(group)
        {
            this.Childern = new List<SeriesChildNode>();
            foreach(var child in group.Childern)
            {
                this.Childern.Add(new SeriesChildNode(child.Name, child.Text, child.Color));
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

        public GroupProfile()
        {

        }

        public object Clone()
        {
            return new GroupProfile(Name, (SeriesGroupNodes)Groups.Clone());
        }
    }

    public class GroupProfiles : List<GroupProfile>, ICloneable
    {
        public object Clone()
        {
            GroupProfiles profiles = new GroupProfiles();
            foreach(var profile in this)
            {
                profiles.Add((GroupProfile)profile.Clone());
            }
            return profiles;
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
