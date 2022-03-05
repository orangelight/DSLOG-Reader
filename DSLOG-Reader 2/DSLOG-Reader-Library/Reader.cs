﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSLOG_Reader_Library
{
    public abstract class Reader
    {
        protected BigEndianBinaryReader reader;
        public int Version { get; private set; }
        public DateTime StartTime { get; private set; }
        public string Path { get; private set; }
        
        public Reader(string path)
        {
            Path = path;
            Version = -1;
        }

        public abstract void Read();

        public void OnlyReadMetaData()
        {
            if (reader != null)
            {
                return;//Throw something
            }
            if (!File.Exists(Path))
            {
                return;//Throw something
            }
            reader = new BigEndianBinaryReader(File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            ReadMetadata();
            reader.Close();
        }

        protected virtual void ReadMetadata()
        {
            if (reader == null) return;//Throw something
            Version = reader.ReadInt32();
            StartTime = Util.FromLVTime(reader.ReadInt64(), reader.ReadUInt64());
        }

        protected bool ReadFile()
        {
            if (reader != null)
            {
                return false;//Throw something
            }
            if (!File.Exists(Path))
            {
                return false;//Throw something
            }
            reader = new BigEndianBinaryReader(File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            ReadMetadata();
            if (Version != 4) return false;
            return ReadEntries();
        }

        protected abstract bool ReadEntries();

    }

    public enum PDPType
    {
        Unknown,
        CTRE,
        REV,
        None,
    }
}
