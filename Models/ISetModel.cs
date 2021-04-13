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
    }
}
