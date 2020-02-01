using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSLOG_Reader_2.CompView;

namespace DSLOG_Reader_2
{
    public partial class MainForm : Form
    {
        private bool CompMode = false;
        private CompForm compForm;
        public MainForm()
        {
            InitializeComponent();
            compForm = new CompForm();
            compForm.MForm = this;
            fileListView.ComForm = compForm;
            fileListView.MainChart = mainGraphView;
            fileListView.EventView = eventsView1;
            fileListView.SetPath(@"C:\Users\Public\Documents\FRC\Log Files");
            fileListView.LoadFiles();
            seriesView.MainChart = mainGraphView;
            seriesView.ComForm = compForm;
            eventsView1.GraphView = mainGraphView;
            mainGraphView.MForm = this;
            mainGraphView.EventsView = eventsView1;
            compForm.FileView = fileListView;
            mainGraphView.ProbeView = probeView1;
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
            compForm.Show();
            compForm.Focus();
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

        private void buttonFindMatch_Click(object sender, EventArgs e)
        {
            mainGraphView.ZoomIntoMatch();
        }

        private void buttonResetZoom_Click(object sender, EventArgs e)
        {
            mainGraphView.ResetZoom();
        }
    }
}
