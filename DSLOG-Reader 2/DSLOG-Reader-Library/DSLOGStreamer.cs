using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DSLOG_Reader_Library
{
    public class DSLOGStreamer : DSLOGReader
    {
        private ConcurrentQueue<DSLOGEntry> Queue;
        private Thread StreamingThread;
        private volatile bool StopSignal = false;
        public DSLOGStreamer(string path) : base(path) { }
        

        public void Stream()
        {
            Queue = new ConcurrentQueue<DSLOGEntry>();
            if (ReadFile())
            {
                StreamingThread = new Thread(StreamEntries);
                StreamingThread.Start();
            }
        }

        private void StreamEntries()
        {
            while (!StopSignal)
            {
                if (reader.BaseStream.Length - reader.BaseStream.Position >= 35)
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Queue.Enqueue(ReadEntry());
                    }      
                }
            }
            reader.Close();
        }

        public void StopStreaming()
        {
            StopSignal = true;
        }

        public int QueueCount()
        {
            return Queue.Count;
        }

        public DSLOGEntry PopEntry()
        {
            if (QueueCount() == 0) return null;
            DSLOGEntry entry;
            bool goodEntry = Queue.TryDequeue(out entry);
            if (!goodEntry) return null;
            return entry;
        }
    }
}
