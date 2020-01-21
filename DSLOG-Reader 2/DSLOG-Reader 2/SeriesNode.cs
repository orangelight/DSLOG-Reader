using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSLOG_Reader_2
{
    public abstract class SeriesNode
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

        public abstract TreeNode ToTreeNode();
    }


    public class SeriesChildNode : SeriesNode
    {
       
        public SeriesGroupNode Parent { get; set; }
        public bool Editable { get; private set; }

        public ChartValueType XValueType { get; set; }
        public AxisType YAxisType { get;  set; }
        public SeriesChartType ChartType { get; set; }
        public int BoarderWidth { get; set; }
        public MarkerStyle MarkerStyle { get; set; }


        public SeriesChildNode(string name, string text, Color backColor, bool edit, SeriesGroupNode parent) : base (name, text, backColor)
        {
            Name = name;
            Text = text;
            if (parent!= null) parent.AddChild(this);
            Editable = edit;
            BoarderWidth = -1;
        }


        public override TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(Text);
            node.Name = Name;
            node.BackColor = Color;

            return node;
        }

        public Series ToSeries()
        {
            Series series = new Series(Name);
            series.XValueType = XValueType;
            series.YAxisType = YAxisType;
            series.ChartType = ChartType;
            series.Color = Color;
            if(BoarderWidth != -1) series.BorderWidth = BoarderWidth;
            if (MarkerStyle != MarkerStyle.None) series.MarkerStyle = MarkerStyle;
            return series;
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

        public override TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(Text);
            node.Name = Name;
            node.BackColor = Color;
            foreach (var child in Childern)
            {
                node.Nodes.Add(child.ToTreeNode());
            }
            return node;
        }
    }
}
