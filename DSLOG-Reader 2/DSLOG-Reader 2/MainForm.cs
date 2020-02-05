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
        public MainForm()
        {
            InitializeComponent();
            fileListView.CompView = competitionView1;
            fileListView.MainChart = mainGraphView;
            fileListView.EventView = eventsView1;
            fileListView.SetPath(@"C:\Users\Public\Documents\FRC\Log Files");
            fileListView.LoadFiles();
            seriesView.AddObserver(mainGraphView);
            seriesView.AddObserver(exportView1);
            seriesView.AddObserver(competitionView1);
            eventsView1.GraphView = mainGraphView;
            mainGraphView.MForm = this;
            mainGraphView.EventsView = eventsView1;
            competitionView1.FileView = fileListView;
            mainGraphView.ProbeView = probeView1;
            seriesView.LoadSeries();
            exportView1.DSGraph = mainGraphView;
            exportView1.DSEvents = eventsView1;
            SetExportMode();
        }

        private void TimerCompMode_Tick(object sender, EventArgs e)
        {
            if (fileListView.HasFMSMatch())
            {
                if (!tabControl2.TabPages.Contains(tabPage7)) tabControl2.TabPages.Add(tabPage7);
            }
            else
            {
                if (tabControl2.TabPages.Contains(tabPage7)) tabControl2.TabPages.Remove(tabPage7);
            }
        }

        public void SetGraphRichText(string text, Color c)
        {
            richTextBoxGraph.Text = text;
            richTextBoxGraph.BackColor = c;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainGraphView.StopStreaming();
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        { 
            SetExportMode();
        }

        private void SetExportMode()
        {
            if (tabControl2.SelectedIndex == 0)
            {
                exportView1.SetMode(ExportView.ExportMode.Chart);
            }
            if (tabControl2.SelectedIndex == 1)
            {
                exportView1.SetMode(ExportView.ExportMode.Events);
            }
            if (tabControl2.SelectedIndex == 2)
            {
                exportView1.SetMode(ExportView.ExportMode.Compititon);
            }
        }
    }
}
