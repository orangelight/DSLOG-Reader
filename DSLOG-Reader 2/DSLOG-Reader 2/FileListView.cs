using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using DSLOG_Reader_Library;

namespace DSLOG_Reader_2
{
    public partial class FileListView : UserControl
    {
        private MainForm MForm;
        private string Path;
        private int lastIndexSelectedFiles = -1;
        public FileListView()
        {
            InitializeComponent();
        }

        public void SetMainForm(MainForm form) { MForm = form; }
        public void SetPath(string path) { Path = path; }

        public void LoadFiles()
        {
            if (Path == null)
            {
                return; //Throw something
            }
            if (Directory.Exists(Path))
            {
                DirectoryInfo dslogDir = new DirectoryInfo(Path);
                FileInfo[] Files = dslogDir.GetFiles("*.dslog");

                for (int y = 0; y < Files.Count(); y++)
                {
                    ListViewItem item = new ListViewItem();
                       
                    item.Text = Files[y].Name.Replace(".dslog", "");
                    DSEVENTSReader reader = new DSEVENTSReader(Path + "\\" + item.Text + ".dsevents");
                    reader.ReadForFMS();
                    if (reader.GetVersion() == 3)
                    {
                        DateTime sTime = DateTime.ParseExact(item.Text, "yyyy_MM_dd HH_mm_ss ddd", CultureInfo.InvariantCulture);//Need to read file if not right format
                        item.SubItems.Add(sTime.ToString("MMM dd, HH:mm:ss ddd"));
                        item.SubItems.Add("" + ((new FileInfo(Path + "\\" + item.Text + ".dslog").Length - 19) / 35) / 50);


                        StringBuilder sb = new StringBuilder();
                        foreach (DSEVENTSEntry en in reader.Entries)
                        {
                            sb.Append(en.Data);
                        }
                        String txtF = sb.ToString();
                        if (txtF.Contains("FMS Connected:   Qualification"))
                        {
                            item.SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Qualification - ") + 33, 5).Split(':')[0]);
                            item.BackColor = Color.Khaki;
                        }
                        else if (txtF.Contains("FMS Connected:   Elimination"))
                        {
                            item.SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Elimination - ") + 31, 5).Split(':')[0]);
                            item.BackColor = Color.LightCoral;
                        }
                        else if (txtF.Contains("FMS Connected:   Practice"))
                        {
                            item.SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   Practice - ") + 28, 5).Split(':')[0]);
                            item.BackColor = Color.LightGreen;
                        }
                        else if (txtF.Contains("FMS Connected:   None"))
                        {
                            item.SubItems.Add(txtF.Substring(txtF.IndexOf("FMS Connected:   None - ") + 24, 5).Split(':')[0]);
                            item.BackColor = Color.LightSkyBlue;
                        }
                        else
                        {
                            item.SubItems.Add("");
                        }


                        TimeSpan sub = DateTime.Now.Subtract(sTime);
                        item.SubItems.Add(sub.Days + "d " + sub.Hours + "h " + sub.Minutes + "m");
                        if (txtF.Contains("FMS Event Name: "))
                        {
                            string[] sArray = txtF.Split(new string[] { "Info" }, StringSplitOptions.None);
                            foreach (String ss in sArray)
                            {
                                if (ss.Contains("FMS Event Name: "))
                                {
                                    item.SubItems.Add(ss.Replace("FMS Event Name: ", ""));
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        item.BackColor = SystemColors.ControlDark;
                        item.SubItems.Add("VERSION");
                        item.SubItems.Add("NOT");
                        item.SubItems.Add("");
                        item.SubItems.Add("SUPPORTED");
                    }
                   
                    listView.Items.Add(item);
                    
                }
                //sroll down to bottom (need to use timer cuz it's weird
                lastIndexSelectedFiles = -1;
                timerScrollToBottom.Start();
                CreateFileWatcher();
            }
        }

        private void CreateFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.Filter = "*.dslog*";
            watcher.Created += new FileSystemEventHandler(OnFileChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            ListViewItem item = new ListViewItem();
            item.Text = e.Name.Replace(".dslog", "");
            listView.Invoke(new MethodInvoker(delegate { listView.Items.Add(item); }));
            //listView.Invoke(new MethodInvoker(delegate { addItemFileInfo(listViewDSLOGFolder.Items.Count - 1); }));
        }

        private void timerScrollToBottom_Tick(object sender, EventArgs e)
        {
            listView.EnsureVisible(listView.Items.Count - 1);
            timerScrollToBottom.Stop();
        }


        private class ListViewCache
        {

        }

    }

        
}
