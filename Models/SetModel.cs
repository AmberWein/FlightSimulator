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
                //
                NotifyPropertyChanged("DataMap");
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
            xmlParser.Parse();
            headersList = xmlParser.Headers;
        }

        // anomaly detection util
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
        float var(ArrayList x, int size)
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
            return Cov(x, y, size) / (float)(Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size)));
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
    }
}
