using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using FlightSimulator.IO;

namespace FlightSimulator.Models
{
    class SetModel : ISetModel
    {
        // INotifyPropertyChanged  implementations
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        // IsetModel
        private ArrayList headersList;
        public ArrayList HeadersList
        {
            get
            {
                return headersList;
            }
            set
            {
                headersList = value;
                NotifyPropertyChanged("HeadersList");
            }
        }
        private string csvPath;
        public string CsvPath
        {
            get
            {
                return csvPath;
            }
            set
            {
                // should i check null?
                // first, validate file existence, then parse it
                if (FileParser.IsValidPath(value)) { 
                    csvPath = value;
                    NotifyPropertyChanged("CsvPath");
                    if (csvPath != null)
                    {
                        CSVParser csvParser = new CSVParser(csvPath, HeadersList);
                        csvParser.Parse();
                        DataMap = csvParser.Map;
                        DataLines = csvParser.Lines;
                    }  
                }
            }
        }
         private ArrayList dataLines;
        public ArrayList DataLines 
        {
            get 
            {
                return dataLines;
            }
            set
            {
                dataLines = value;
                NotifyPropertyChanged("DataLines");
            }
        }
        private Dictionary<string, ArrayList> dataMap;
        public Dictionary<string, ArrayList> DataMap
        {
            get 
            {
                return dataMap;
            }
            set
            {
                dataMap = value;
                NotifyPropertyChanged("DataMap");
            }
        }
        private double frequency;
        public double Frequency
        {
            get { return frequency; }
            set
            { 
                frequency = value;
                NotifyPropertyChanged("Frequency");
            }
        }
        // Constructor
        public SetModel()
        {
            csvPath = null;
            dataMap = null;
            dataLines = null;
            // parse xml for headers
            XMLParser xmlParser = new XMLParser();
            Frequency = xmlParser.getFrequency();
            xmlParser.Parse();
            headersList = xmlParser.Headers;
        }
    }
}
