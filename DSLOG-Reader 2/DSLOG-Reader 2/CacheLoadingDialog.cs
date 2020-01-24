using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLOG_Reader_2
{
    public partial class CacheLoadingDialog : Form
    {
        private DSLOGFileEntryCache Cache;
        private List<DSLOGFileEntry> DSLOGFiles;
        private string Path;
        private FileInfo[] Files;
        private int UselessFiles = 0;
        public CacheLoadingDialog(DSLOGFileEntryCache cache, List<DSLOGFileEntry> fileList, string path)
        {
            InitializeComponent();
            Cache = cache;
            DSLOGFiles = fileList;
            Path = path;
            DirectoryInfo dslogDir = new DirectoryInfo(Path);
            Files = dslogDir.GetFiles("*.dslog");
            backgroundWorker1.WorkerReportsProgress = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            
            for (int y = 0; y < Files.Count(); y++)
            {
                DSLOGFileEntry entry;
                string name = Files[y].Name.Replace(".dslog", "");
                if (Cache.TryGetEntry(name, out entry))
                {
                    DSLOGFiles.Add(entry);
                    
                }
                else
                {
                    entry = Cache.AddEntry(name, Path);
                    
                    DSLOGFiles.Add(entry);
                    if (entry.Useless) UselessFiles++;
                }
               
                int num =(int) (100.0* ((double)y / (double)Files.Count()));
                backgroundWorker1.ReportProgress(num);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = $"{(int)(((double)e.ProgressPercentage / 100.0) * Files.Count())}/{Files.Count()} Logs";
            label3.Text = $"{UselessFiles} Useless logs found!";
        }

        private void CacheLoadingDialog_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
