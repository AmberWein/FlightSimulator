using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulator.Models
{
    class FlightSimulatorModel : IFlightSimulatorModel
    {
        private ISetModel settings;
        // private Imodel[] controllers;
        // private DataSender client;
        private int lineNumber;
        public int LineNumber { get { return lineNumber;} set { lineNumber = value; /* change loop*/} }
        private float playingSpeed;
        public float PlayingSpeed { get { return playingSpeed;} set { playingSpeed = value; /* change loop*/} }

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

        public FlightSimulatorModel(ISetModel set)
        {
            settings = set;
            // should sign as listener only to changes of map and lines.
            //set.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {if (sender == set && )};// this should listen only to change in map
            lineNumber = 0;
            playingSpeed = 1;
            dataMap = null;
            dataLines = null;
            // also create other models and put.
        }
        // function start. will only after DataMap and lines will be set.
    }
}
