using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        Dictionary<string, ArrayList> DataMap { get; set;}
        ArrayList DataLines { get; set;}
        bool IsPlay { get; set;}
        float Yaw { get; set; }
        float Pitch { get;set;}
        float Roll { get; set;}
        float Orientation { get;set;}
        float AirSpeed { get;set;}
        float Altitude { get;set;}
    }
}
