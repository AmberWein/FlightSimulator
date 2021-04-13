using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class DetectorViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        // INotifyPropertyChanged implementations
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string VM_CurrentDetector
        {
            get { return model.CurrentDetector; }
            set
            {
                if (string.Compare(value, "Upload detector") == 0)
                {
                    // neew to open box for path, check path. if its real - put in map and set this as current detector. 
                    // make some animation while detecting the file until the anomaliesreport is generated??
                    // if not good
                    model.CurrentDetector = "Choose detector";
                    NotifyPropertyChanged("VM_CurrentDetector");
                }
                else
                    model.CurrentDetector = value;

            }
        }
        public Dictionary<string, string> VM_DllMap
        {
            get { return model.DllMap; }
            set
            {
                model.DllMap = value;
            }
        }
        public List<string> VM_DetectorsList
        {
            get { return model.DetectorsList; }
            set
            {
               
            }
        }
        
        public bool VM_IsDetectorOn { get { return model.IsDetectorOn; } set { } }

        public void DetectorPropertyChange(Object sender, PropertyChangedEventArgs e)
        {
            // example for one
            if (string.Compare(e.PropertyName, "DllMap") == 0)
            {
                VM_DllMap = model.DllMap;
                NotifyPropertyChanged("VM_DllMap");
                return;
            }
            if (string.Compare(e.PropertyName, "CurrentDetector") == 0)
            {
                VM_CurrentDetector = model.CurrentDetector;
                NotifyPropertyChanged("VM_CurrentDetector");
                return;
            }
            if (string.Compare(e.PropertyName, "IsDetectorOn") == 0)
            {
                VM_IsDetectorOn = model.IsDetectorOn;
                NotifyPropertyChanged("VM_IsDetectorOn");
                return;
            }
            if (string.Compare(e.PropertyName, "DetectorsList") == 0)
            {
                VM_DetectorsList = model.DetectorsList;
                NotifyPropertyChanged("VM_DetectorsList");
                return;
            }
        }
        public DetectorViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += DetectorPropertyChange;
        }
    }
}
