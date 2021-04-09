using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class FlightSimulatorViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        public FlightSimulatorViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Dictionary<string, ArrayList> VM_DataMap
        {
            get
            {
                return model.DataMap;
            }
            set
            {
                model.DataMap = value;
            }
        }
        public ArrayList VM_DataLines 
        {
            get
            {
                return model.DataLines;
            }
            set
            {
                model.DataLines = value;
            }
        }
    }
}
