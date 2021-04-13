using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using FlightSimulator.IO;
using System;

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
                        //CSVParser csvParser = new CSVParser(csvPath, HeadersList);
                        CsvParser.FilePath = value;
                        CsvParser.Parse();
                        CsvParser.CreateCSV("anomaly_flight_with_headers.csv");
                        DataMap = CsvParser.Map;
                        DataLines = CsvParser.Lines;
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
                //
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
        private CSVParser csvParser;
        public CSVParser CsvParser
        {
            get
            {
                return csvParser;
            }
        }

        private ArrayList correlatedFeatures { get; set; }
        public ArrayList CorrelatedFeatures
        {
            get
            {
                return correlatedFeatures;
            }
            set
            {
                correlatedFeatures = value;
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
            csvParser = new CSVParser("C:/Users/NicoleS/Downloads/reg_flight.csv", headersList);
            csvParser.Parse();
            csvParser.CreateCSV("reg_flight_with_headers.csv");
            // also need to make correlation map
        }

        // correlation computin

        // returns the avarege of a given ArrayList
        float Avg(ArrayList x, int size)
        {
            float sum = 0;
            foreach(float xValue in x)
            {
                sum += xValue;
            }
            return sum / size;
        }

        // returns the variance of X and Y
        float Var(ArrayList x, int size)
        {
            float av = Avg(x, size);
            float sum = 0;
            foreach(float xValue in x)
            {
                sum += xValue * xValue;
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        float Cov(ArrayList x, ArrayList y, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                float xValue = (float)x[i];
                float yValue = (float)y[i];
                sum += xValue * yValue;
            }
            sum /= size;

            return sum - Avg(x, size) * Avg(y, size);
        }
        // returns the Pearson correlation coefficient of X and Y
        float Pearson(ArrayList x, ArrayList y, int size)
        {
            float a = (float)Math.Sqrt(Var(x, size));
            float b = (float)Math.Sqrt(Var(y, size));
            // make sure not to divide by zero
            if (a == 0 || b == 0)
                return 0;
            return Cov(x, y, size) / (a * b);
        }
        string GetMostCorrelatedFeature(string featureName)
        {
            float mostCorrelatedValue = 0;
            string mostCorrelatedName = "";
            if (dataMap.ContainsKey(featureName))
            {
                ArrayList givenFeatue = dataMap[featureName];
                // go over all the features and find the most correlative one
                foreach (string hl in this.HeadersList)
                {
                    // ignore checking the given feature
                    if (hl == featureName)
                    {
                        continue;
                    }
                    else
                    {
                        ArrayList currentFeatue = dataMap[hl];
                        float currentCorrelation = Cov(givenFeatue, currentFeatue, headersList.Count);
                        // chech if we get higher correlation for the current feature,
                        // if so, then upate the values
                        if (mostCorrelatedValue < currentCorrelation)
                        {
                            mostCorrelatedValue = currentCorrelation;
                            mostCorrelatedName = hl;
                        }
                    }

                }
            }
            return mostCorrelatedName;
        }
        // set the correlated features
        void SetMostCorrelated()
        {
            // go over each property and find the most correlated property to it
            foreach(string h in this.headersList)
            {
                string correlated = GetMostCorrelatedFeature(h);
                this.CorrelatedFeatures.Add(new KeyValuePair<string, string>(h, correlated));
            }
        }
    }
}
