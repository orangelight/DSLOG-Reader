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
        private Dictionary<Double, String> EventsDict = new Dictionary<double, string>();
        private int lastIndexSelectedEvents = -1;
        private string Filter = "";
        public bool FilterRepeated = false;
        public bool RemoveJoystick = false;
        public EventsView()
        {
            InitializeComponent();
        }


        public void LoadLog(DSLOGFileEntry file, string dir)
        {
            var fileName = $"{dir}\\{file.Name}.dsevents";
            EventsDict.Clear();
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
            foreach(var en in reader.Entries)
            {
                EventsDict[en.Time.ToOADate()] = en.Data;
            }
            richTextBox1.Text = "";
            AddEvents();
        }

        private void AddEvents()
        {
            listViewEvents.BeginUpdate();
            listViewEvents.ListViewItemSorter = null;
            lastIndexSelectedEvents = -1;
            GraphView.ClearMessages();
            listViewEvents.Items.Clear();
            var dupDict = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in EventsDict.Where(e=>e.Value.Contains(Filter, StringComparison.OrdinalIgnoreCase)).AsParallel().ToList())
            {
                DataPoint po = new DataPoint(entry.Key, 15);
                po.MarkerSize = 6;
                
                DateTime entryTime = DateTime.FromOADate(entry.Key);

               

               
                ListViewItem item = new ListViewItem();
                item.Text = entryTime.ToString("h:mm:ss.fff tt");
                string entryText = entry.Value;

                if (RemoveJoystick)
                {
                    var NoJoyText = RemoveJoyStickMessages(entryText);
                    if (!NoJoyText.Contains(Filter, StringComparison.OrdinalIgnoreCase)) continue;
                    if (string.IsNullOrWhiteSpace(NoJoyText)) continue;
                    entryText = NoJoyText;
                }

                if (FilterRepeated && (dupDict.ContainsKey(entryText) && (dupDict[entryText] - entryTime).Duration().TotalSeconds < 4.0))
                {
                    continue;
                }
                dupDict[entryText] = entryTime;
                item.SubItems.Add(entryText);

                if (entry.Value.Contains("ERROR") || entry.Value.Contains("<flags> 1"))
                {
                    item.BackColor = Color.Red;
                    po.Color = Color.Red;
                    po.YValues[0] = 14.7;
                }
                else if (entry.Value.Contains("<Code> 44004 "))
                {
                    item.BackColor = Color.SandyBrown;
                    po.Color = Color.SandyBrown;
                    po.MarkerStyle = MarkerStyle.Square;
                    po.YValues[0] = 14.7;
                }
                else if (entry.Value.Contains("<Code> 44008 "))
                {
                    int pFrom = entry.Value.IndexOf("Warning <Code> 44008 <radioLostEvents>  ") + "Warning <Code> 44008 <radioLostEvents>  ".Length;
                    int pTo = entry.Value.IndexOf("\r<Description>FRC:  Robot radio detection times.");
                    string processedData = entry.Value.Substring(pFrom, pTo - pFrom).Replace("<radioSeenEvents>", "~");
                    string[] dataInArray = processedData.Split('~');
                    if (!dataInArray[0].Trim().Equals(""))
                    {
                        double[] arrayLost = Array.ConvertAll(dataInArray[0].Trim().Split(','), Double.Parse);
                        foreach (double d in arrayLost)
                        {
                            DateTime newTime = entryTime.AddSeconds(-d);
                            DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.7);
                            pRL.Color = Color.Yellow;
                            pRL.MarkerSize = 6;
                            pRL.MarkerStyle = MarkerStyle.Square;
                            pRL.YValues[0] = 14.7;
                            GraphView.AddMessage(pRL);
                            EventsDict[newTime.ToOADate()] = "Radio Lost";
                        }
                    }
                    if (dataInArray.Length > 1)
                    {
                        if (!dataInArray[1].Trim().Equals(""))
                        {
                            double[] arrayLost = Array.ConvertAll(dataInArray[1].Trim().Split('<')[0].Split(','), Double.Parse);
                            foreach (double d in arrayLost)
                            {
                                DateTime newTime = entryTime.AddSeconds(-d);
                                DataPoint pRL = new DataPoint(newTime.ToOADate(), 14.7);
                                pRL.Color = Color.Lime;
                                pRL.MarkerSize = 6;
                                pRL.MarkerStyle = MarkerStyle.Square;
                                pRL.YValues[0] = 14.7;
                                GraphView.AddMessage(pRL);
                                EventsDict[newTime.ToOADate()] = "Radio Seen";
                            }
                        }
                    }

                    item.BackColor = Color.Khaki;
                    po.Color = Color.Khaki;
                    po.YValues[0] = 14.7;
                }
                else if (entry.Value.Contains("Warning") || entry.Value.Contains("<flags> 2"))
                {
                    item.BackColor = Color.Khaki;
                    po.Color = Color.Khaki;
                    po.YValues[0] = 14.7;
                }
                item.SubItems.Add("" + entry.Key);
                listViewEvents.Items.Add(item);
                GraphView.AddMessage(po);
                EventsDict[entry.Key] = entry.Value;
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
            return Regex.Replace(message, @"(Info Joystick [0-9]+: \(.*?\)[0-9]+ axes, [0-9]+ buttons, [0-9]+ POVs\.)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <count> 1 <flags> 0 <Code> 1 <details> Joystick (Button|axis|POV)? \d+ on port \d+ not available, check if controller is plugged in <location> edu\.wpi\.first\.wpilibj\.DriverStation\.reportJoystickUnpluggedWarning\(DriverStation\.java:1110\) <stack>)|(<TagVersion>1 <time> (\d+:)?-?\d+\.\d+ <message> Warning at edu\.wpi\.first\.wpilibj\.DriverStation\.reportJoystickUnpluggedWarning\(DriverStation\.java:1110\): Joystick (Button|axis|POV)? \d on port \d not available, check if controller is plugged in)", "").Trim();
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
            return EventsDict.TryGetValue(key, out data);
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
    }
}
