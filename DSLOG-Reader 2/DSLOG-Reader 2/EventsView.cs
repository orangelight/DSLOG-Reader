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

namespace DSLOG_Reader_2
{
    public partial class EventsView : UserControl
    {
        //private List<DSEVENTSEntry> LogEvents;
        private Dictionary<Double, String> EventsDict = new Dictionary<double, string>();
        private int lastIndexSelectedEvents = -1;
        private string Filter = "";
        public EventsView()
        {
            InitializeComponent();
        }


        public void LoadLog(string file, string dir)
        {
            var fileName = $"{dir}\\{file}.dsevents";
            EventsDict.Clear();
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
            listViewEvents.Items.Clear();
            var test = EventsDict.Where(e => e.Value.Contains(Filter));
            foreach (var entry in EventsDict.Where(e=>e.Value.Contains(Filter)).AsParallel().ToList())
            {
                DataPoint po = new DataPoint(entry.Key, 15);
                po.MarkerSize = 6;
                ListViewItem item = new ListViewItem();
                DateTime entryTime = DateTime.FromOADate(entry.Key);
                item.Text = entryTime.ToString("h:mm:ss.fff tt");
                item.SubItems.Add(entry.Value);

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
                            //chartMain.Series["Messages"].Points.Add(pRL);
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
                                //chartMain.Series["Messages"].Points.Add(pRL);
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
                //chartMain.Series["Messages"].Points.Add(po);
                //EventsDict[en.Time.ToOADate()] = en.Data;
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
    }
}
