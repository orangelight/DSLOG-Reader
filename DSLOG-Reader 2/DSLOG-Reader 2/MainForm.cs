﻿using System;
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

        public enum MainMode
        {
            Graph,
            Events,
            Compititon
        }

        public MainForm()
        {
            InitializeComponent();
            fileListView.CompView = competitionView1;
            fileListView.MainChart = mainGraphView;
            fileListView.EventView = eventsView1;
            fileListView.SetPath(@"C:\Users\Public\Documents\FRC\Log Files\DSLogs");
            fileListView.LoadFiles();
            seriesView.AddObserver(mainGraphView);
            seriesView.AddObserver(exportView1);
            seriesView.AddObserver(competitionView1);
            seriesView.AddObserver(probeView1);
            seriesView.AddObserver(fileListView);
            seriesView.AddObserver(energyView1);
            eventsView1.GraphView = mainGraphView;
            eventsView1.MForm = this;
            mainGraphView.MForm = this;
            mainGraphView.EnergyView = energyView1;
            mainGraphView.EventsView = eventsView1;
            competitionView1.FileView = fileListView;
            mainGraphView.ProbeView = probeView1;
            seriesView.LoadSeries();
            exportView1.DSGraph = mainGraphView;
            exportView1.DSEvents = eventsView1;
            exportView1.Comp = competitionView1;
            exportView1.Files = fileListView;
            probeView1.MainGraph = mainGraphView;
            SetMode();
        }

        private void TimerCompMode_Tick(object sender, EventArgs e)
        {
            if (fileListView.HasFMSMatch())
            {
                if (!tabControlRight.TabPages.Contains(tabPage7)) tabControlRight.TabPages.Add(tabPage7);
            }
            else
            {
                if (tabControlRight.TabPages.Contains(tabPage7)) tabControlRight.TabPages.Remove(tabPage7);
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
            SetMode();
            competitionView1.SetCurrentMainTab(tabControlRight.SelectedTab.Text);
        }

        private void SetMode()
        {
            if (tabControlRight.SelectedIndex == 0)
            {
                exportView1.SetMode(MainMode.Graph);
                eventsView1.SetMode(MainMode.Graph);

            }
            if (tabControlRight.SelectedIndex == 1)
            {
                exportView1.SetMode(MainMode.Events);
                eventsView1.SetMode(MainMode.Events);
            }
            if (tabControlRight.SelectedIndex == 2)
            {
                exportView1.SetMode(MainMode.Compititon);
                eventsView1.SetMode(MainMode.Compititon);
            }
        }

        public MainMode GetCurrentMode()
        {
            if (tabControlRight.SelectedIndex == 0)
            {
                return MainMode.Graph;

            }
            if (tabControlRight.SelectedIndex == 1)
            {
                return MainMode.Events;
            }
            if (tabControlRight.SelectedIndex == 2)
            {
                return MainMode.Compititon;
            }
            return MainMode.Graph;
        }

        public void SetRightTabIndex(int index)
        {
            tabControlRight.SelectedIndex = index;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/orangelight/DSLOG-Reader/blob/master/Help.md");
        }

        public void SetLiveLog(bool live)
        {
            if (live)
            {

            }
        }
    }
}
