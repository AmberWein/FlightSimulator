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
                FinishTime = 0.1 * (double)dataLines.Count;
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
        
        // initialize dashboard data
        public void InitDashboardData()
        {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
            Orientation = 0;
            Altitude = 0;
            AirSpeed = 0;
        }
        // start updating data as the time passes
        public void StartFlying()
        {
            int sleepingTime, lineNumber;
            
           
            /*int sign= client.Connect();
            if (sign !=1)
                return;*/
           while (isPlay)
            {
                lineNumber = (int)(Timer * 10.0);
                sleepingTime = (int)(100 / PlayingSpeed);
                // get current values for dashboard properties
                Yaw =  float.Parse(DataMap["side-slip-deg"][lineNumber].ToString());
                Pitch =  float.Parse(DataMap["pitch-deg"][lineNumber].ToString());
                Roll =  float.Parse(DataMap["roll-deg"][lineNumber].ToString());
                Orientation =  float.Parse(DataMap["heading-deg"][lineNumber].ToString());
                Altitude =  float.Parse(DataMap["altitude-ft"][lineNumber].ToString());
                AirSpeed =  float.Parse(DataMap["airspeed-kt"][lineNumber].ToString());
                //client.Send(DataLines[lineNumber].ToString());
                Timer += 0.1;
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
    }
}
