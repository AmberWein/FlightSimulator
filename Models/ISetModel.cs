using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using FlightSimulator.IO;

namespace FlightSimulator.Models
{
    public interface ISetModel : INotifyPropertyChanged
    {
        // properties for flight
        ArrayList HeadersList { get; set; }
        string CsvPath { get; set; }
        Dictionary<string, ArrayList> DataMap { get; set; }
        ArrayList DataLines { get; set; }
        CSVParser CsvParser { get; }
        
        ArrayList CorrelatedFeatures { get; set; }
        double Frequency { get; set; }
        // returns the avarege of a given ArrayList
        float Avg(ArrayList x, int size);

        // returns the variance of X and Y
        float Var(ArrayList x, int size);
        // returns the covariance of X and Y
        float Cov(ArrayList x, ArrayList y, int size);
        // returns the Pearson correlation coefficient of X and Y
        float Pearson(ArrayList x, ArrayList y, int size);
    }
}
