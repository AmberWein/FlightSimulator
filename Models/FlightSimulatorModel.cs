using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using FlightSimulator.Communication;

namespace FlightSimulator.Models
{
    class FlightSimulatorModel : IFlightSimulatorModel
    {
        //flight gear
        private float elevator;
        private float aileron;
        private float throttle;
        private float rudder;
        // INotifyPropertyChanged implementations
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private readonly ISetModel settings;
        private Client client;
        private Dictionary<string, ArrayList> dataMap;
        public Dictionary<string, ArrayList> DataMap
        {
            get { return dataMap; }
            set
            {
                // this should only occur once, as csv file will be parsed within the SetModel
                dataMap = value;
            }
        }
        private ArrayList dataLines;
        public ArrayList DataLines
        {
            get { return dataLines; }
            set
            {
                // this should only occur once, as csv file will be parsed within the SetModel
                dataLines = value;
                // 10 Hz means reading 10 lines in 1 second
                FinishTime = ((double)dataLines.Count)/Frequency;
            }
        }
        private double frequency;
        public double Frequency
        {
            get { return frequency; }
            set
            {
                if (value == 0)
                {
                    frequency = 10; // default value 10 Hz
                }
                else
                {
                    frequency = value;
                }
                // Notify?
            }
        }
        // indicates wheater simulator is on play mode.
        private bool isPlay;
        public bool IsPlay
        {
            get { return isPlay; }
            set
            {
                isPlay = value;
                // when set to true, start the flight in a new thread
                if (isPlay)
                {
                    new Thread(StartFlying).Start();
                }
            }
        }
        private double playingSpeed;
        public double PlayingSpeed
        {
            get { return playingSpeed; }
            set
            {
                // this is only controlled from View, no need to Notify
                playingSpeed = value;
            }
        }

        private double timer;
        public double Timer
        {
            get { return timer; }
            set {
                timer = value;
                NotifyPropertyChanged("Timer");
            }
        }

        private double finishTime;
        public double FinishTime {
            get { return finishTime; }
            set
            {
                finishTime = value;
                NotifyPropertyChanged("FinishTime");
            }
        }


        // make this model a listener to changes whitin settings. When the data is uploaded, get it.
        public void SettingsChanged(Object sender, PropertyChangedEventArgs e) {
            if (string.Compare(e.PropertyName, "DataMap") == 0)
            {
                DataMap = settings.DataMap;
            }
            else if (string.Compare(e.PropertyName, "DataLines") == 0)
            {
                DataLines = settings.DataLines;
            }
        }
        // Constructor
        public FlightSimulatorModel(ISetModel set)
        {
            // add to settings.PropertyChanged event to listen for changes
            settings = set;
            settings.PropertyChanged += SettingsChanged;
            // create a new client that will send data to local IP on port 5400
            client = new Client();
            // initialize members
            dataMap = null;
            dataLines = null;
            playingSpeed = 1;
            timer = 0;
            finishTime = 0;
            yaw = 0;
            pitch = 0;
            roll = 0;
            orientation = 0;
            altitude = 0;
            airSpeed = 0;
            frequency = 10; // default value
            dllMap = new Dictionary<string, string>();
           // dllMap.Add("Simple", "/plugins/SimpleDetect.dll");
            dllMap.Add("Simple", "C:\\Users\\NicoleS\\source\\repos\\FlightSimulator\\plugins\\SimpleDetect.dll");
            dllMap.Add("Circular", "C:\\Users\\NicoleS\\source\\repos\\FlightSimulator\\plugins\\CircularDetect.dll");
            detectorsList = new List<string>() { "Choose detector", "Simple", "Circular", "Add detector" };
            currentDetector = DetectorsList[0];
            isDetectorOn = false;
        }
        // Dashboard properties
        private float yaw;
        public float Yaw
        {
            get
            {
                return yaw;
            }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        private float pitch;
        public float Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        private float roll;
        public float Roll
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public float orientation;
        public float Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
                NotifyPropertyChanged("Orientation");
            }
        }
        public float altitude;
        public float Altitude
        {
            get
            {
                return altitude;
            }
            set
            {
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        public float airSpeed;
        public float AirSpeed
        {
            get
            {
                return airSpeed;
            }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public float Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public float Aileron
        {
            get { return aileron; }
            set
            {
                this.aileron = value;
                this.NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return elevator; }
            set
            {
                this.elevator = value;
                this.NotifyPropertyChanged("Elevator");
            }
        }
        public float Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        
        // initialize dashboard data
        public void InitDashboardData()
        {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
            Orientation = 0;
            Altitude = 0;
            AirSpeed = 0;
            Throttle = 0;
            Rudder = 0; 
            Aileron = 0; 
            Elevator = 0; 
        }
        // start updating data as the time passes
        public void StartFlying()
        {
            
            DetectorsList = detectorsList;
            CurrentDetector = DetectorsList[0];
            int sleepingTime, lineNumber;

            /*int sign= client.Connect();
            if (sign !=1)
                return;*/
            while (isPlay)
            {
                
                lineNumber = (int)(Timer * Frequency);
                sleepingTime = (int)(100 / PlayingSpeed);
                // get current values for dashboard properties
                Yaw =  float.Parse(DataMap["side-slip-deg"][lineNumber].ToString());
                Pitch =  float.Parse(DataMap["pitch-deg"][lineNumber].ToString());
                Roll =  float.Parse(DataMap["roll-deg"][lineNumber].ToString());
                Orientation =  float.Parse(DataMap["heading-deg"][lineNumber].ToString());
                Altitude =  float.Parse(DataMap["altitude-ft"][lineNumber].ToString());
                AirSpeed =  float.Parse(DataMap["airspeed-kt"][lineNumber].ToString());
                Rudder = float.Parse(DataMap["rudder"][lineNumber].ToString());
                Throttle = float.Parse(DataMap["throttle"][lineNumber].ToString());
                Aileron = float.Parse(DataMap["aileron"][lineNumber].ToString());
                Elevator= float.Parse(DataMap["elevator"][lineNumber].ToString());
                //client.Send(DataLines[lineNumber].ToString());
                Timer += 1.0/Frequency;
                // if we finished to read all lines
                if (Timer >= FinishTime)
                {
                    IsPlay = false;
                    InitDashboardData();
                   // break;
                }
                // make thread sleep, to control frequency
                System.Threading.Thread.Sleep(sleepingTime);
            }
        }
        // Anomalies Detector Properties
        private string currentDetector;
        public string CurrentDetector
        {
            get { return currentDetector; }
            set
            {
                currentDetector = value;
                if(string.Compare(value, "Choose detector") == 0)
                //if (string.Compare(value, DetectorsList[DetectorsList.Count-1]) == 0)
                {
                    IsDetectorOn = false;
                }
                else
                    new Thread(GetAnomalies).Start();
            }
        }
        public void GetAnomalies()
        {
            string dllPath;
            DllMap.TryGetValue(CurrentDetector, out dllPath);
            Program.OperateDLL(dllPath);
        }
        private Dictionary<string, string> dllMap;
        public Dictionary<string, string> DllMap
        {
            get { return dllMap; }
            set
            {
                dllMap = value;
            }
        }
        private List<string> detectorsList;
        public List<string> DetectorsList
        {
            get { return detectorsList; }
            set
            {
                detectorsList = value;
                NotifyPropertyChanged("DetectorsList");
            }
        }
        private bool isDetectorOn;
        public bool IsDetectorOn { get { return isDetectorOn; } set { isDetectorOn = value; NotifyPropertyChanged("IsDetectorOn"); } }

    }
}
