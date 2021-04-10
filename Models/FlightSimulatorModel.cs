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

namespace FlightSimulator.Models
{
    class FlightSimulatorModel : IFlightSimulatorModel
    {
        // need maximum line for media player
        // trying this
        private bool isPlay;
        public bool IsPlay 
        {
            get { return isPlay; } 
            set 
            {
                isPlay = value; 
                
                new Thread(StartFlying).Start();
                //Application.Current.Dispatcher.cur
                //Application.Current.Dispatcher.Invoke(new Action(()=>{  startFlying();}));

                //ThreadPool.QueueUserWorkItem(_ => { Dispatcher.BeginInvoke(new Action(()=>{startFlying();}));});
                
            } 
        }
        private ISetModel settings;
        // private Imodel[] controllers;
        // private DataSender client;
        private int lineNumber;
        public int LineNumber { get { return lineNumber;} set { lineNumber = value; /* change loop*/} }
        private int playingSpeed;
        public int PlayingSpeed { get { return playingSpeed;} set { playingSpeed = value; /* change loop*/} }

        private Dictionary<string, ArrayList> dataMap;
        public Dictionary<string, ArrayList> DataMap { get{return dataMap;} set{dataMap = value; /* should only occur once and make prog start. do we need to notify anyone?*/}}
        private ArrayList dataLines;
        public ArrayList DataLines { get {return dataLines;} set{dataLines = value;/* should only occur once. do we need to notify anyone?*/}}


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
         //   else if(string.Compare(e.PropertyName, "IsPlay")==0)
           // {
           //     IsPlay= settings.IsPlay;
            //}
        }
        public FlightSimulatorModel(ISetModel set)
        {
            settings = set;
            settings.PropertyChanged += SettingsChanged;
            // should sign as listener only to changes of map and lines.
            //set.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {if (sender == set && )};// this should listen only to change in map
            lineNumber = 0;
            playingSpeed = 1;
            dataMap = null;
            dataLines = null;
            // also create other models and put.

        }
        // function start. will only after DataMap and lines will be set.
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
        public void StartFlying()
        {
            playingSpeed = 100;
           while (isPlay)
            {
                Yaw =  float.Parse(DataMap["side-slip-deg"][lineNumber].ToString());
                Pitch =  float.Parse(DataMap["pitch-deg"][lineNumber].ToString());
                Roll =  float.Parse(DataMap["roll-deg"][lineNumber].ToString());
                Orientation =  float.Parse(DataMap["heading-deg"][lineNumber].ToString());
                Altitude =  float.Parse(DataMap["altitude-ft"][lineNumber].ToString());
                AirSpeed =  float.Parse(DataMap["airspeed-kt"][lineNumber].ToString());
                lineNumber++;
                System.Threading.Thread.Sleep(playingSpeed);
            }
            // should be a wl(hile and change all properties every time we move and also send line to client
        }

       

    }
}
