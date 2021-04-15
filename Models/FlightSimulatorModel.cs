﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using FlightSimulator.Communication;




//C:\Users\user\Desktop\reg_flight.csv
namespace FlightSimulator.Models
{
    public class FlightSimulatorModel : IFlightSimulatorModel
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
        private ArrayList attributes;

        public ArrayList Attributes
        {
            get { return attributes; }
            set { attributes = value;
                NotifyPropertyChanged("Attributes");
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
                Attributes = settings.HeadersList;

                DataLines = settings.DataLines;
                CorrelatedFeatures = settings.CorrelatedFeatures;


            }
            else if (string.Compare(e.PropertyName, "HeadersList") == 0)
            {
                Attributes = settings.HeadersList;
            }
            else if (string.Compare(e.PropertyName, "CorrelatedFeatures") == 0)
            {
                CorrelatedFeatures = settings.CorrelatedFeatures;
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
            dllMap.Add("Simple", GetRelativePath("plugins","SimpleDetect.dll"));
            dllMap.Add("Circular", GetRelativePath("plugins", "CircularDetect.dll"));
            detectorsList = new List<string>() { "Choose detector", "Simple", "Circular", "Add detector" };
            currentDetector = DetectorsList[0];
            isDetectorOn = false;
            getDetector = false;
        }
        // return the parent folder of this folder, meaning FilghtSimulator's path
        public static string GetParentPath(string path)
        {
            try
            {
                System.IO.DirectoryInfo directoryInfo =
                    System.IO.Directory.GetParent(path);

                string parentFile = directoryInfo.FullName;
                return parentFile;
            }
            catch (ArgumentNullException)
            {
                System.Console.WriteLine("Path is a null reference.");
                return null;
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("Path is an empty string, " +
                    "contains only white spaces, or " +
                    "contains invalid characters.");
                return null;
            }
        }

        // return a relative path to a given file in a given folder
        public static string GetRelativePath(string folderName, string fileName)
        {
            string relativePath = GetParentPath(fileName);
            relativePath = GetParentPath(relativePath);
            relativePath = GetParentPath(relativePath);
            relativePath += "\\"+ folderName +"\\" + fileName;
            return relativePath;
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
        /****Graph Model*****/

        //the chosen atrribute from user to show graphs of
        string chosenAttribute;
        public string ChosenAttribute
        {
            get { return chosenAttribute; }
            set
            {
                chosenAttribute = value;
                NotifyPropertyChanged("ChosenAttribute");
            }
        }

        //correlatef features for presenting graphs
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
                if (Timer >= FinishTime) //change to > ?
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
                IsDetectorOn = false;
                
                if (string.Compare(value, DetectorsList[0]) == 0)
                {
                    currentDetector = value;
                    isDetectorOn = false;
                    // something else?
                    NotifyPropertyChanged("CurrentDetector");
                    return;
                }
                if (!GetDetector && string.Compare(value, DetectorsList[DetectorsList.Count - 1]) == 0)
                {
                    currentDetector = DetectorsList[0];
                    GetDetector = true;
                }
                else
                {
                    currentDetector = value;
                    new Thread(GetAnomalies).Start();
                    NotifyPropertyChanged("CurrentDetector");
                }
            }
        }
        private bool getDetector;
        public bool GetDetector
        {
            get { return getDetector; }
            set { getDetector = value; NotifyPropertyChanged("GetDetector"); }
        }
        public void GetAnomalies()
        {
            string dllPath;
            DllMap.TryGetValue(CurrentDetector, out dllPath);
            bool madeReport = Program.OperateDLL(dllPath);
            if (madeReport)
            {
                IsDetectorOn = true;
            }
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
                detectorsList = new List<string>(value);
                NotifyPropertyChanged("DetectorsList");
            }
        }
        private bool isDetectorOn;
        public bool IsDetectorOn { get { return isDetectorOn; } set { isDetectorOn = value; NotifyPropertyChanged("IsDetectorOn"); } }
        public bool ValidateDLLPath(string path)
        {
            return Program.IsValidDLL(path);
        }


        //calculates linear regression given a list of points
        public Line linear_reg(ArrayList points, int size)
        {
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();
           foreach (Point  p in points)
            {
                x.Add(p.x);
                y.Add(p.y);
            }
            float a = settings.Cov(x, y, size) / settings.Var(x, size);
            float b = settings.Avg(y, size) - a*(settings.Avg(x, size));
            Line l = new Line(a, b);
            return l;
        }


        public ArrayList fromFloatsToPoints(ArrayList a, ArrayList b)
        {
            ArrayList points = new ArrayList();
            for(int i = 0; i < a.Count- 1; i++)
            {
                points.Add(new Point(float.Parse(a[i].ToString()), float.Parse(b[i].ToString())));
            }

            return points;
        }




    }
}
