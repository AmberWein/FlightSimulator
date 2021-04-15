using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FlightSimulator.IO
{
    public abstract class FileParser
    {
        protected string filePath;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }
        public static bool IsDLL(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            if (extension == null)
                return false;
            if (String.Compare(extension.ToLower(), ".dll") == 0)
                return true;
            return false;
        }
        public FileParser(string filePath)
        {
            this.filePath = filePath;
        }

        // checks if a given file's path is valid
        public static bool IsValidPath(string filePath)
        {
            return File.Exists(filePath);
        }

        // parse data from this file's path
        virtual public void Parse()
        {
            // backup validity check
            if (IsValidPath(this.filePath) == false)
                return;
        }
        //public static Dictionary<string, ArrayList> GetAnomalies(string path)
        public static string GetAnomalies(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            if (extension == null)
                return ""; // meaning didn't happen
            if (String.Compare(extension.ToLower(), ".txt") != 0)
                return "";
            string text = "";
            string line;
            var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                text += line;
                text += "\n";
            }
            return text;
              reader.Dispose();

        }
    }
}
