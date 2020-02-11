using System;
using System.Collections.Concurrent;
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
        private Dictionary<string, DSLOGFileEntry> DSLOGFiles;
        private string Path;
        private FileInfo[] Files;
        private volatile int UselessFiles = 0;
        private Stopwatch stopWatch = new Stopwatch();
        private List<double> lastEst = new List<double>();
        private volatile int EntryNum = 0;
        public CacheLoadingDialog(DSLOGFileEntryCache cache, Dictionary<string, DSLOGFileEntry> fileList, string path)
        {
            InitializeComponent();
            Cache = cache;
            DSLOGFiles = fileList;
            Path = path;
            DirectoryInfo dslogDir = new DirectoryInfo(Path);
            Files = dslogDir.GetFiles("*.dslog");
            backgroundWorker1.WorkerReportsProgress = true;

            this.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            backgroundWorker1.RunWorkerAsync();
            stopWatch.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ConcurrentBag<DSLOGFileEntry> fileEntryList = new ConcurrentBag<DSLOGFileEntry>();
            int lastNum = -1;
            Parallel.ForEach(Files, (file) =>
            {
                DSLOGFileEntry entry;

                string name = file.Name.Replace(".dslog", "");
                if (Cache.TryGetEntry(name, out entry))
                {
                    fileEntryList.Add(entry);

                }
                else
                {
                    entry = Cache.AddEntry(name, Path);

                    fileEntryList.Add(entry);
                    if (entry.Useless) UselessFiles++;
                }
                int num = (int)(100.0 * ((double)EntryNum++ / (double)Files.Count()));
                if (num > lastNum)
                {
                    backgroundWorker1.ReportProgress(num);
                }
                
            });

            foreach(var entry in fileEntryList.OrderBy(en => en.StartTime))
            {
                DSLOGFiles.Add(entry.Name, entry);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int deltaP = e.ProgressPercentage - progressBar1.Value;
           
            if (deltaP > 0)
            {
                int left = 100 - e.ProgressPercentage;
                stopWatch.Stop();
                TimeSpan timeTaken = stopWatch.Elapsed;
                stopWatch.Reset();
                stopWatch.Start();
                double estMili = (timeTaken.TotalMilliseconds / (double)deltaP) * (double)left;
                progressBar1.Value = e.ProgressPercentage;
                
                label2.Text = $"{(int)(((double)e.ProgressPercentage / 100.0) * Files.Count())}/{Files.Count()} Logs";
                if(lastEst.Count != 0)
                {
                    double alpha = .05;
                    lastEst.Add(estMili);
                    label4.Text = $"Estimated time left: {(int)(lastEst.Aggregate((ema, est) => alpha * est + (1 - alpha) * ema) / 1000)} Seconds";
                }
                else
                {
                    lastEst.Add(estMili);
                }
                label3.Text = $"{UselessFiles} Useless logs found!";
            }
           
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

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text =  "Too Bad...";
        }
    }
}
