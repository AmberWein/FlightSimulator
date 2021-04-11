using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using FlightSimulator.ViewModels;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.IO;
using FlightSimulator.Communication;

namespace FlightSimulator.Models
{
    class FlightSimulatorModel : IFlightSimulatorModel
    {
        private int maxLine;
        public int MaxLine
        {
            get {  return maxLine;}
            set { maxLine=value;}
        }
        // need maximum line for media player
        
        // indicates wheater simulator is on play mode.
        private bool isPlay;
        public bool IsPlay 
        {
            get { return isPlay; } 
            set 
            {
                isPlay = value; 
                // when set to true start a thread to get flight data from map
                if (isPlay)
                {
                    new Thread(StartFlying).Start();
                }
            } 
        }
        private ISetModel settings;
        // private Imodel[] controllers;
        private Client client;
        private int lineNumber;
        public int LineNumber { get { return lineNumber;} set { lineNumber = value; /* change loop*/} }
        private double playingSpeed;
        public double PlayingSpeed { get { return playingSpeed;} set {
                playingSpeed = value; /* need to check. if getting 1.5 from media player, should be 100/1.5?*/} }
        private Dictionary<string, ArrayList> dataMap;
        public Dictionary<string, ArrayList> DataMap { get{return dataMap;} 
            set
            {
                dataMap = value; /* should only occur once and make prog start. do we need to notify anyone?*/ 
            }
        }
        private ArrayList dataLines;
        public ArrayList DataLines { get {return dataLines;} set{dataLines = value; MaxLine = dataLines.Count;/* should only occur once. do we need to notify anyone?*/}}

        private double timer;
        public double Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        private double finishTime;
        public double FinishTime { get { return finishTime; } set { finishTime = value; } }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void SettingsChanged (Object sender, PropertyChangedEventArgs e) {
            if(string.Compare(e.PropertyName,"DataMap")==0)
            {
                DataMap = settings.DataMap;
            }
            else if(string.Compare(e.PropertyName, "DataLines")==0)
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
            // initialize members
            maxLine = 0;
            lineNumber = 0;
            playingSpeed = 1;
            dataMap = null;
            dataLines = null;
            // also create other models and put.??
            client = new Client();
            timer = 0;
            finishTime = 0;
        }
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
        
        public void initData()
        {
            Yaw = 0;
            Pitch = 0;
            Roll = 0;
            Orientation = 0;
            Altitude = 0;
            AirSpeed = 0;
        }
        public void StartFlying()
        {
            int sleepingTime =(int) (100 / PlayingSpeed);
            /*
          * 
            int sign= client.Connect();
            if (sign !=1)
                return;*/
           while (isPlay)
            {
                sleepingTime = (int)(100 / PlayingSpeed);
                // we might need to get the proper lineNumber. check if this gets changes from mediaplayerview
                Yaw =  float.Parse(DataMap["side-slip-deg"][lineNumber].ToString());
                Pitch =  float.Parse(DataMap["pitch-deg"][lineNumber].ToString());
                Roll =  float.Parse(DataMap["roll-deg"][lineNumber].ToString());
                Orientation =  float.Parse(DataMap["heading-deg"][lineNumber].ToString());
                Altitude =  float.Parse(DataMap["altitude-ft"][lineNumber].ToString());
                AirSpeed =  float.Parse(DataMap["airspeed-kt"][lineNumber].ToString());
               // client.Send(DataLines[lineNumber].ToString());
                lineNumber++;
                if (lineNumber >= maxLine)
                {
                    IsPlay = false;
                    initData();
                   // break;
                }
                System.Threading.Thread.Sleep(sleepingTime);
            }
            // should be a wl(hile and change all properties every time we move and also send line to client
        }
    }
}
