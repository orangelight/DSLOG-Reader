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
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;

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
        List<DSEVENTSEntry> Entries;
        public EventsView()
        {
            InitializeComponent();
            listViewEvents.DoubleBuffered(true);
        }


        public void LoadLog(DSLOGFileEntry file)
        {
            var fileName = $"{file.FilePath}\\{file.Name}.dsevents";
            EventsDict.Clear();
            Entries = null;
            listViewEvents.Items.Clear();
            if (!File.Exists(fileName))
            {
                return;
            }
            DSEVENTSReader reader = new DSEVENTSReader(fileName);
            reader.Read();
            if (reader.Version != 3)
            {
                return;
            }
            Entries = reader.Entries;
            richTextBox1.Text = "";
            AddEvents();
        }

        private void AddEntryToDict(double key, string value)
        {
            if (!EventsDict.ContainsKey(key)) EventsDict[key] = new List<string>();
            EventsDict[key].Add(value);
        }

        public void AddEvents()
        {
            if (Entries == null) return;
            listViewEvents.BeginUpdate();
            listViewEvents.ListViewItemSorter = null;
            lastIndexSelectedEvents = -1;
            GraphView.ClearMessages();
            listViewEvents.Items.Clear();
            var dupDict = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            EventsDict = new Dictionary<double, List<string>>();
            foreach (var entry in Entries.Where(e => e.Data.Contains(Filter, StringComparison.OrdinalIgnoreCase)))
            {
                DataPoint po = new DataPoint(entry.Time.ToOADate(), 15);
                po.MarkerSize = 6;
                ListViewItem item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = entry.Time.ToString("h:mm:ss.fff tt");
                string entryText = entry.Data;

                if (RemoveJoystick)
                {
                    var NoJoyText = RemoveJoyStickMessages(entryText);
                    if (!NoJoyText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                    if (string.IsNullOrWhiteSpace(NoJoyText)) continue;
                    entryText = NoJoyText.Replace("   ", " ").Replace("   ", " ");
                }

                if (FilterRepeated && (dupDict.ContainsKey(entryText) && (dupDict[entryText] - entry.Time).Duration().TotalSeconds < 4.0))
                {
                    continue;
                }

                if (FilterImportant && !IsMessageImportant(entryText)) continue;
                dupDict[entryText] = entry.Time;
                
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
                    int pFrom = entry.Data.IndexOf("Warning <Code> 44008 <radioLostEvents>  ") + "Warning <Code> 44008 <radioLostEvents>  ".Length;
                    int pTo = entry.Data.IndexOf("\r<Description>FRC:  Robot radio detection times.");
                    string processedData = entry.Data.Substring(pFrom, pTo - pFrom).Replace("<radioSeenEvents>", "~");
                    string[] dataInArray = processedData.Split('~');
                    if (!dataInArray[0].Trim().Equals(""))
                    {
                        double[] arrayLost = Array.ConvertAll(dataInArray[0].Trim().Split(','), Double.Parse);
                        foreach (double d in arrayLost)
                        {
                            DateTime newTime = entry.Time.AddSeconds(-d);
                            DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.7);
                            pRL.Color = Color.Yellow;
                            pRL.MarkerSize = 6;
                            pRL.MarkerStyle = MarkerStyle.Square;
                            pRL.YValues[0] = 14.7;
                            GraphView.AddMessage(pRL);
                            AddEntryToDict(newTime.ToOADate(), "Radio Lost");
                        }
                    }
                    if (dataInArray.Length > 1)
                    {
                        if (!dataInArray[1].Trim().Equals(""))
                        {
                            double[] arrayLost = Array.ConvertAll(dataInArray[1].Trim().Split('<')[0].Split(','), Double.Parse);
                            foreach (double d in arrayLost)
                            {
                                DateTime newTime = entry.Time.AddSeconds(-d);
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

                listViewEvents.Items.Add(item);
                GraphView.AddMessage(po);
                AddEntryToDict(entry.Time.ToOADate(), entryText);
            }
            listViewEvents.Columns[0].Width = -2;
            listViewEvents.EndUpdate();
        }

        public void SetFilter(string filter)
        {
            Filter = filter;
            AddEvents();
            richTextBox1.HighlightText(Filter, Color.Red);

        }

        private string RemoveJoyStickMessages(string message)
        {
            return Regex.Replace(message, @"(Info Joystick [0-9]+: \(.*?\)[0-9]+ axes, [0-9]+ buttons, [0-9]+ POVs\.)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <count> 1 <flags> 0 <Code> 1 <details> Joystick (Button|axis|POV)? \d+ on port \d+ not available, check if controller is plugged in <location> .*? <stack>)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <message> Warning at .*?: Joystick (Button|axis|POV)? \d on port \d not available, check if controller is plugged in)", "").Trim();
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
            RemoveJoystick =  !RemoveJoystick;
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
            foreach(ListViewItem item in listViewEvents.Items)
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
            return Regex.Match(data, "(ERROR)|(<flags> [1-2])|(<Code> -44009)|(<Code> 44008)|(<Code> 44004)|(Warning)").Captures.Count > 0;
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
    }
}
