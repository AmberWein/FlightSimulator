using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Globalization;

namespace FlightSimulator.IO
{
    class CSVParser: FileParser
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
        public CSVParser(string newFilePath, ArrayList newProperties) : base(newFilePath)
        {
            this.properties = newProperties;
            this.numOfProperties = properties.Count;
            this.map = new Dictionary<string, ArrayList>();
            this.lines = new ArrayList();
        }


        public override void Parse()
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            int i = 0;
            while (!reader.EndOfStream)
            //while (i < this.numOfProperties)
            // maybe we should check validity- meaning if the amount of rows matches to the properties size
            {
                string line = reader.ReadLine();
                Lines.Add(line);
                string[] valuesString = line.Split(',');
                string key = properties[i].ToString();
                i++;
                ArrayList listOfValues = new ArrayList();
                foreach (string value in valuesString)
                {
                    float courrentValue = float.Parse(value, CultureInfo.InvariantCulture);
                    listOfValues.Add(courrentValue);
                }
                this.map.Add(key, listOfValues);
            }
            reader.Dispose();
        }
    }
}
