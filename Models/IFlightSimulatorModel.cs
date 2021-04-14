using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulator.Models
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        // data structures
        Dictionary<string, ArrayList> DataMap { get; set;}
        ArrayList DataLines { get; set;}
        // mediaPlayer and running logic properties
        bool IsPlay { get; set;}
        double PlayingSpeed { get; set; }
        double Timer { get; set; }
        double FinishTime { get; set; }
        // dashboard properties
        float Yaw { get; set; }
        float Pitch { get;set;}
        float Roll { get; set;}
        float Orientation { get;set;}
        float AirSpeed { get;set;}
        float Altitude { get;set;}
        //properties for gear control
        float Throttle { set; get; }
        float Rudder { set; get; }
        float Aileron { set; get; }
        float Elevator { set; get; }
        // initialize all dashboard properties
        void InitDashboardData();
        // properties for dll detector
        Dictionary<string, string> DllMap { get; set; }
        List<string> DetectorsList { get; set; }
        string CurrentDetector { get; set; }
        bool IsDetectorOn { get; set; }
        //string InsertDLLPath { get; set; }
        //string InsertDLLName { get; set; }

        bool ValidateDLLPath(string path);



        bool GetDetector { get; set; }
    }
}
