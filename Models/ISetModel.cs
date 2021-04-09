using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FlightSimulator.Models
{
    interface ISetModel : INotifyPropertyChanged
    {
        ArrayList HeadersList { get; set; }
        string CsvPath { get; set; }
        Dictionary<string, ArrayList> DataMap { get; set; }

    }
}
