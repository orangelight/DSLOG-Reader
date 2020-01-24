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
        private Stopwatch stopWatch = new Stopwatch();
        List<double> lastEst = new List<double>();
        public CacheLoadingDialog(DSLOGFileEntryCache cache, List<DSLOGFileEntry> fileList, string path)
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
