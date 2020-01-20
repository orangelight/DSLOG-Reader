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
            var item = new ListViewItem(new string[] { "1", "2", "3", "4", "5", "6" });
            listView.Items.Add(item);
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

    }

        
    }
