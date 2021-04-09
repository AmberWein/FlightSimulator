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
            initMap();
            this.lines = new ArrayList();
        }

        private void initMap()
        {
            for (int i = 0; i < this.numOfProperties; i++)
            {
                map.Add(this.properties[i].ToString(), new ArrayList());
            }
        }

        public override void Parse()
        {
            var reader = new StreamReader(File.OpenRead(filePath));
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
    }
}
