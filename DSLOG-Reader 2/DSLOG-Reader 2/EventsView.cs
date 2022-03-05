﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DSLOG_Reader_Library;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using static DSLOG_Reader_2.MainForm;

namespace DSLOG_Reader_2
{
    public partial class EventsView : UserControl
    {
        public MainGraphView GraphView { get; set; }
        public MainForm MForm { get; set; }
        private Dictionary<Double, List<String>> EventsDict = new Dictionary<double, List<string>>();
        private int lastIndexSelectedEvents = -1;
        private string Filter = "";
        private bool FilterRepeated = false;
        private bool RemoveJoystick = false;
        private bool FilterImportant = false;
        private bool RefreshEvents = false;
        private bool FilterCodeOutput = false;
        List<DSEVENTSEntry> Entries;

        Regex ImportantRegex = new Regex("(ERROR)|(<flags> [1-2])|(<Code> -44009)|(<Code> 44008)|(<Code> 44004)|(Warning)", RegexOptions.Compiled);
        Regex CodeFilterRegex = new Regex(@"<TagVersion>[0-9]+ <time> -?(\d+:)?\d+\.\d+ <message>((?!<TagVersion>).)*", RegexOptions.Compiled);
        Regex JoystickRegex = new Regex(@"(Info Joystick [0-9]+: \(.*?\)[0-9]+ axes, [0-9]+ buttons, [0-9]+ POVs\.)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <count> 1 <flags> 0 <Code> 1 <details> Joystick (Button|axis|POV)? \d+ on port \d+ not available, check if controller is plugged in <location> .*? <stack>)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <message> Warning at .*?: Joystick (Button|axis|POV)? \d on port \d not available, check if controller is plugged in)", RegexOptions.Compiled);

        public EventsView()
        {
            InitializeComponent();
            listViewEvents.DoubleBuffered(true);
            toolTip.SetToolTip(buttonCodeOutput, "Remove Code Output");
            toolTip.SetToolTip(buttonDup, "Filter Duplicated Events");
            toolTip.SetToolTip(buttonImportant, "Filter Important Events");
            toolTip.SetToolTip(buttonJoystick, "Remove Joystick Output");
            toolTip.SetToolTip(textBoxSearch, "Search for Event");
        }


        public async void LoadLog(DSLOGFileEntry file)
        {
            
            var fileName = $"{file.FilePath}\\{file.Name}.dsevents";
            EventsDict.Clear();
            Entries = null;
            //listViewEvents.Items.Clear();
            if (!File.Exists(fileName))
            {
                return;
            }
            DSEVENTSReader reader = new DSEVENTSReader(fileName);
            try
            {
                reader.ReadAndParse();
            } catch(Exception ex)
            {
                MessageBox.Show("dsevent file corrupted!");
                return;
            }
         
            if (reader.Version != 4)
            {
                return;
            }
            Entries = reader.Entries;
           
        }

        private void AddEntryToDict(double key, string value)
        {
            if (EventsDict == null) return;
            if (!EventsDict.ContainsKey(key)) EventsDict[key] = new List<string>();
            EventsDict[key].Add(value);
        }

        public void AddEvents(bool onlyPlot = false)
        {
            richTextBox1.Text = "";
            if (Entries == null) return;
            if (MForm.GetCurrentMode() != MainMode.Events) //TODO Merge this with else for no repeated code
            {
                RefreshEvents = true;
            }
            else
            {
                RefreshEvents = false;
                listViewEvents.BeginUpdate();
                listViewEvents.ListViewItemSorter = null;
                lastIndexSelectedEvents = -1;
                listViewEvents.Items.Clear();
            }


            GraphView.ClearMessages();

            var dupDict = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            EventsDict = new Dictionary<double, List<string>>();
            foreach (var entry in Entries.Where(e => e.Data.Contains(Filter, StringComparison.OrdinalIgnoreCase)))
            {
               
                if (entry.SubEntries != null && entry.SubEntries.Count > 0) {
                    var filterSubEnties = entry.SubEntries.Where(e => e.Message.Contains(Filter, StringComparison.OrdinalIgnoreCase));
                    foreach(var subEntry in filterSubEnties)
                    {
                        ListViewItem item = new ListViewItem();
                        item.UseItemStyleForSubItems = false;
                        if (GraphView.CanUseMatchTime && GraphView.UseMatchTime)
                        {
                            item.Text = ((subEntry.Time - GraphView.MatchTime).TotalMilliseconds / 1000.0).ToString("0.###");
                        }
                        else
                        {
                            item.Text = subEntry.Time.ToString("h:mm:ss.fff tt");
                        }

                        string entryText = subEntry.Message;
                        if (FilterImportant && !IsMessageImportant(entryText)) continue;
                        if (RemoveJoystick)
                        {
                            var NoJoyText = RemoveJoyStickMessages(entryText);
                            if (!NoJoyText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                            if (string.IsNullOrWhiteSpace(NoJoyText)) continue;
                            entryText = Regex.Replace(NoJoyText, @"\s+", " ", RegexOptions.Compiled);
                        }
                        if (FilterCodeOutput)
                        {
                            entryText = CodeFilterRegex.Replace(entryText, "").Trim();
                            if (!entryText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                            if (string.IsNullOrWhiteSpace(entryText)) continue;

                        }
                        if (FilterRepeated && (dupDict.ContainsKey(entryText) && (dupDict[entryText] - subEntry.Time).Duration().TotalSeconds < 4.0))
                        {
                            continue;
                        }
                        else
                        {
                            dupDict[entryText] = subEntry.Time;
                        }

                        var mode = GraphView.GetEntryAt(subEntry.Time.ToOADate());

                        item.SubItems[0].BackColor = Color.DarkGray;
                        if (mode != null)
                        {
                            if (mode.DSAuto) item.SubItems[0].BackColor = Color.Lime;
                            else if (mode.DSTele) item.SubItems[0].BackColor = Color.Cyan;
                        }

                        if (MForm.GetCurrentMode() == MainMode.Events)
                        {
                            listViewEvents.Items.Add(item);
                        }



                        item.SubItems.Add(entryText);
                        if (entryText.Contains("ERROR") || entryText.Contains("<flags> 1"))
                        {
                            item.SubItems[1].BackColor = Color.Red;
          
                        }
                        else if (entryText.Contains("<Code> 44004 "))
                        {
                            item.SubItems[1].BackColor = Color.SandyBrown;
                            
                        }
                        else if (entryText.Contains("<Code> 44008 "))
                        {
                            AddRadioEvents(entry); // TODO
                            item.SubItems[1].BackColor = Color.Khaki;

                        }
                        else if (entryText.Contains("Warning") || entryText.Contains("<flags> 2") || entryText.Contains("<Code> -44009 "))
                        {
                            item.SubItems[1].BackColor = Color.Khaki;
                        }
                        item.SubItems.Add("" + subEntry.Time.ToOADate());
                    }
                } else
                {
                    DataPoint po = new DataPoint(entry.Time.ToOADate(), 15);
                    po.MarkerSize = 6;
                    ListViewItem item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    if (GraphView.CanUseMatchTime && GraphView.UseMatchTime)
                    {
                        item.Text = ((entry.Time - GraphView.MatchTime).TotalMilliseconds / 1000.0).ToString("0.###");
                    }
                    else
                    {
                        item.Text = entry.Time.ToString("h:mm:ss.fff tt");
                    }

                    string entryText = entry.Data;
                    if (FilterImportant && !IsMessageImportant(entryText)) continue;
                    if (RemoveJoystick)
                    {
                        var NoJoyText = RemoveJoyStickMessages(entryText);
                        if (!NoJoyText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                        if (string.IsNullOrWhiteSpace(NoJoyText)) continue;
                        entryText = Regex.Replace(NoJoyText, @"\s+", " ", RegexOptions.Compiled);
                    }
                    if (FilterCodeOutput)
                    {
                        entryText = CodeFilterRegex.Replace(entryText, "").Trim();
                        if (!entryText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                        if (string.IsNullOrWhiteSpace(entryText)) continue;

                    }
                    if (FilterRepeated && (dupDict.ContainsKey(entryText) && (dupDict[entryText] - entry.Time).Duration().TotalSeconds < 4.0))
                    {
                        continue;
                    }
                    else
                    {
                        dupDict[entryText] = entry.Time;
                    }




                    item.SubItems.Add(entryText);

                    if (entry.Data.Contains("ERROR") || entry.Data.Contains("<flags> 1"))
                    {
                        item.SubItems[1].BackColor = Color.Red;
                        po.Color = Color.Red;
                        po.YValues[0] = 14.7;
                    }
                    else if (entry.Data.Contains("<Code> 44004 "))
                    {
                        item.SubItems[1].BackColor = Color.SandyBrown;
                        po.Color = Color.SandyBrown;
                        po.MarkerStyle = MarkerStyle.Square;
                        po.YValues[0] = 14.7;
                    }
                    else if (entry.Data.Contains("<Code> 44008 "))
                    {
                        AddRadioEvents(entry);
                        item.SubItems[1].BackColor = Color.Khaki;
                        po.Color = Color.Khaki;
                        po.YValues[0] = 14.7;
                    }
                    else if (entry.Data.Contains("Warning") || entry.Data.Contains("<flags> 2") || entry.Data.Contains("<Code> -44009 "))
                    {
                        item.SubItems[1].BackColor = Color.Khaki;
                        po.Color = Color.Khaki;
                        po.YValues[0] = 14.7;
                    }
                    item.SubItems.Add("" + entry.Time.ToOADate());
                    var mode = GraphView.GetEntryAt(entry.Time.ToOADate());

                    item.SubItems[0].BackColor = Color.DarkGray;
                    if (mode != null)
                    {
                        if (mode.DSAuto) item.SubItems[0].BackColor = Color.Lime;
                        else if (mode.DSTele) item.SubItems[0].BackColor = Color.Cyan;
                    }

                    if (MForm.GetCurrentMode() == MainMode.Events)
                    {
                        listViewEvents.Items.Add(item);
                    }
                    GraphView.AddMessage(po);
                    AddEntryToDict(entry.Time.ToOADate(), entryText);
                }
                
                
            }

            if (MForm.GetCurrentMode() == MainMode.Events)
            {
                listViewEvents.Columns[0].Width = -2;
                listViewEvents.EndUpdate();
            }

        }

        private void AddRadioEvents(DSEVENTSEntry entry)
        {
            int pFrom = entry.Data.IndexOf("Warning <Code> 44008 <radioLostEvents>  ") + "Warning <Code> 44008 <radioLostEvents>  ".Length;
            int pTo = entry.Data.IndexOf("\r<Description>FRC:  Robot radio detection times.");
            if (pTo < 0 || pFrom < 0 || pTo <= pFrom) return;
            string processedData = entry.Data.Substring(pFrom, pTo - pFrom).Replace("<radioSeenEvents>", "~");
            string[] dataInArray = processedData.Split('~');
            if (!dataInArray[0].Trim().Equals(""))
            {
                
                    foreach (var num in dataInArray[0].Trim().Split(','))
                    {
                        double lostTime = 0;
                        if (double.TryParse(num, out lostTime))
                        {
                            DateTime newTime = entry.Time.AddSeconds(-lostTime);
                            DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.7);
                            pRL.Color = Color.Yellow;
                            pRL.MarkerSize = 6;
                            pRL.MarkerStyle = MarkerStyle.Square;
                            pRL.YValues[0] = 14.7;
                            GraphView.AddMessage(pRL);
                            AddEntryToDict(newTime.ToOADate(), "Radio Lost");
                        }
                    }
                
            }
            if (dataInArray.Length > 1)
            {
                if (!dataInArray[1].Trim().Equals(""))
                {
                        if (dataInArray[1].Trim().Split('<').Length == 0) return;
                        foreach (var num in dataInArray[1].Trim().Split('<')[0].Split(','))
                        {
                            double seenTime = 0;
                            if (double.TryParse(num, out seenTime))
                            {
                                DateTime newTime = entry.Time.AddSeconds(-seenTime);
                                DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.7);
                                pRL.Color = Color.Lime;
                                pRL.MarkerSize = 6;
                                pRL.MarkerStyle = MarkerStyle.Square;
                                pRL.YValues[0] = 14.7;
                                GraphView.AddMessage(pRL);
                                AddEntryToDict(newTime.ToOADate(), "Radio Seen");
                            }
                        }
                    
                    
                }
            }
        }

        public void SetFilter(string filter)
        {
            Filter = filter;
            AddEvents();
            richTextBox1.HighlightText(Filter, Color.Red);

        }

        private string RemoveJoyStickMessages(string message)
        {
            return JoystickRegex.Replace(message, "").Trim();
        }

        private void ListViewEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEvents.SelectedItems.Count != 0)
            {
                if (listViewEvents.SelectedItems[0].Index != lastIndexSelectedEvents)
                {

                    lastIndexSelectedEvents = listViewEvents.SelectedItems[0].Index;
                    richTextBox1.Text = listViewEvents.SelectedItems[0].SubItems[1].Text;
                    richTextBox1.HighlightText(Filter, Color.Red);
                }
            }
        }

        private void ListViewEvents_Resize(object sender, EventArgs e)
        {
            if (listViewEvents.Width > 1095)
            {
                listViewEvents.Columns[1].Width = listViewEvents.Width - 97;
            }
            else
            {
                listViewEvents.Columns[1].Width = 1000;
            }
        }

        public bool TryGetEvent(double key, out string data)
        {
            data = "";
            List<string> raw;
            bool found = EventsDict.TryGetValue(key, out raw);
            if (!found) return false;
            data = string.Join("\n", raw);
            if (RemoveJoystick) data = RemoveJoyStickMessages(data);
            return true;
        }

        private void buttonJoystick_Click(object sender, EventArgs e)
        {
            RemoveJoystick = !RemoveJoystick;
            if (RemoveJoystick)
            {
                buttonJoystick.BackColor = Color.Lime;
            }
            else
            {
                buttonJoystick.BackColor = Color.Red;
            }
            AddEvents();
        }

        private void buttonDup_Click(object sender, EventArgs e)
        {
            FilterRepeated = !FilterRepeated;
            if (FilterRepeated)
            {
                buttonDup.BackColor = Color.Lime;
            }
            else
            {
                buttonDup.BackColor = Color.Red;
            }
            AddEvents();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SetFilter(textBoxSearch.Text);
        }

        public List<Tuple<string, string>> GetEntries()
        {
            List<Tuple<string, string>> events = new List<Tuple<string, string>>();
            foreach (ListViewItem item in listViewEvents.Items)
            {
                events.Add(new Tuple<string, string>(item.SubItems[0].Text, item.SubItems[1].Text));
            }
            return events;
        }

        private void listViewEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GraphView.SetCursorPosition(Double.Parse(listViewEvents.SelectedItems[0].SubItems[2].Text));
            MForm.SetRightTabIndex(0);
        }

        private void backgroundWorkerLoadEvents_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        
        private bool IsMessageImportant(string data)
        {
            return ImportantRegex.Match(data).Captures.Count > 0;
        }

        private void buttonImportant_Click(object sender, EventArgs e)
        {
            FilterImportant = !FilterImportant;
            if (FilterImportant)
            {
                buttonImportant.BackColor = Color.Lime;
            }
            else
            {
                buttonImportant.BackColor = Color.Red;
            }
            AddEvents();
        }

        public void SetMode(MainMode mode)
        {
            if (mode == MainMode.Events && RefreshEvents)
            {
                AddEvents();
            }
        }

        private void buttonCodeOutput_Click(object sender, EventArgs e)
        {
            FilterCodeOutput = !FilterCodeOutput;
            if (FilterCodeOutput)
            {
                buttonCodeOutput.BackColor = Color.Lime;
            }
            else
            {
                buttonCodeOutput.BackColor = Color.Red;
            }
            AddEvents();
        }
    }
}
