using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Globalization;
using System;
using FlightSimulator.Models;

namespace FlightSimulator.IO
{
    public class CSVParser: FileParser
    {
        private ArrayList properties;
        private ArrayList lines;
        private int numOfProperties;
        private Dictionary<string, ArrayList> map;
        public ArrayList Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }
        public ArrayList Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }
        public int NumOfProperties
        {
            get
            {
                return numOfProperties;
            }
            set
            {
                numOfProperties = value;
            }
        }

        public Dictionary<string, ArrayList> Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }
        // parse the data from a given CSV file's path 
        // and a list of headers
        public CSVParser(string newFilePath, ArrayList newProperties) : base(newFilePath)
        {
            this.properties = newProperties;
            this.numOfProperties = properties.Count;
            this.map = new Dictionary<string, ArrayList>();
            InitMap();
            this.lines = new ArrayList();
        }

        public static bool IsCSV(string path)
        {
            string extension = System.IO.Path.GetExtension(path);
            if (extension == null)
                return false;
            if (String.Compare(extension.ToLower(), ".csv") == 0)
                return true;
            return false;
        }
        // function to init this map
        private void InitMap()
        {
            for (int i = 0; i < this.numOfProperties; i++)
            {
                map.Add(this.properties[i].ToString(), new ArrayList());
            }
        }

        public override void Parse()
        {
            var reader = new StreamReader(File.OpenRead(FlightSimulatorModel.GetRelativePath("Files", "reg_flight.csv")));
            int i = 0;
            int j;

            while (!reader.EndOfStream)
            // maybe we should check validity- meaning if the amount of rows matches to the properties size
            {
                j = 0;
                string line = reader.ReadLine();
                string[] valuesString = line.Split(',');
                Lines.Add(line);
                i++;
                foreach (string value in valuesString)
                {
                    float currentValue = float.Parse(value, CultureInfo.InvariantCulture);
                    this.map[properties[j].ToString()].Add(currentValue);
                    j++;
                }
            }
            reader.Dispose();
        }

        // create a CSV file contains both headers and data
        public void CreateCSV(string fileName)
        {
            //var filepath = fileName;
            using (StreamWriter writer = new StreamWriter(new FileStream(fileName,
            FileMode.Create, FileAccess.Write)))
            {
                // write the first line of properties' name
                foreach (string h in this.properties)
                {
                    writer.Write(h + ", ");
                }

                foreach (string line in this.lines)
                {
                    writer.WriteLine();
                    string[] valuesString = line.Split(',');
                    foreach (string value in valuesString)
                    {
                        float currentValue = float.Parse(value, CultureInfo.InvariantCulture);
                        writer.Write(currentValue + ", ");
                    }
                }

                writer.Close();
            }
        }

    }
}
