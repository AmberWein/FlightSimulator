using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using FlightSimulator.IO;

namespace FlightSimulator.Models
{
    class SetModel : ISetModel
    {
        // INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

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
                dataLines = value;/* should only occur once. do we need to notify anyone?*/
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
        public SetModel()
        {
            csvPath = null;
            XMLParser xmlParser = new XMLParser();
            xmlParser.Parse();
            headersList = xmlParser.Headers;
            dataMap = null;
        }
    }
}
