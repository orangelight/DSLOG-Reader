using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader_2
{
    public partial class MainForm : Form
    {
        private bool CompMode = false;
        public MainForm()
        {
            InitializeComponent();
            fileListView.MainChart = mainGraphView;
            fileListView.EventView = eventsView1;
            fileListView.SetPath(@"C:\Users\Public\Documents\FRC\Log Files");
            fileListView.LoadFiles();
            seriesView.MainChart = mainGraphView;
            eventsView1.GraphView = mainGraphView;
            mainGraphView.MForm = this;
            mainGraphView.EventsView = eventsView1;

            seriesView.LoadSeries();
            
        }

        private void TimerCompMode_Tick(object sender, EventArgs e)
        {
            if (fileListView.HasFMSMatch())
            {
                buttonCompMode.Enabled = true;
                if (!CompMode)
                {
                    if (buttonCompMode.BackColor == SystemColors.Control)
                    {
                        buttonCompMode.BackColor = Color.Red;
                    }
                    else
                    {
                        buttonCompMode.BackColor = SystemColors.Control;
                    }
                }
                else
                {
                    if (buttonCompMode.BackColor == SystemColors.Control)
                    {
                        buttonCompMode.BackColor = Color.Lime;
                    }
                    else
                    {
                        buttonCompMode.BackColor = SystemColors.Control;
                    }
                }
                
            }
            else
            {
                buttonCompMode.Enabled = false;
            }
        }

        private void ButtonCompMode_Click(object sender, EventArgs e)
        {
            if (!CompMode)
            {
                CompMode = true;
                HideReg(false);
                buttonCompMode.Text = "Switch to Regular Mode";
            }
            else
            {
                CompMode = false;
                HideReg(true); 
                buttonCompMode.Text = "Switch to Competition Mode";
            }
        }

        private void HideReg(bool vis)
        {
            tabControl1.Visible = vis;
            tabControl2.Visible = vis;
            textBoxSearch.Visible = vis;
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            eventsView1.SetFilter(textBoxSearch.Text);
        }

        public void SetGraphRichText(string text, Color c)
        {
            richTextBoxGraph.Text = text;
            richTextBoxGraph.BackColor = c;
        }
    }
}
