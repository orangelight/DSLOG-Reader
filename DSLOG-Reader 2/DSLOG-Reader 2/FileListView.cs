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
        List<DSLOGFileEntry> DSLOGFiles;
        public FileListView()
        {
            InitializeComponent();
            DSLOGFiles = new List<DSLOGFileEntry>();
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
                    
                    var entry = new DSLOGFileEntry(Files[y].Name.Replace(".dslog", ""), Path);

                    listView.Items.Add(entry.ToListViewItem());
                    
                }
                //sroll down to bottom (need to use timer cuz it's weird
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
            var entry = new DSLOGFileEntry(e.Name.Replace(".dslog", ""), Path);
            listView.Invoke(new MethodInvoker(delegate { listView.Items.Add(entry.ToListViewItem()); }));
        }

        private void timerScrollToBottom_Tick(object sender, EventArgs e)
        {
            listView.EnsureVisible(listView.Items.Count - 1);
            timerScrollToBottom.Stop();
        }
    }

        
}
