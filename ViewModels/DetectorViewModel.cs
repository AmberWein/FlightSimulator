using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using FlightSimulator.Views;

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

        public bool VM_GetDetector
        {
            get;
            set;
        }
        public string VM_CurrentDetector
        {
            get { return model.CurrentDetector; }
            set
            {
                model.CurrentDetector = value;
                // check if its add detector
              /*  if (string.Compare(value, VM_DetectorsList[VM_DetectorsList.Count - 1]) == 0)
                {
                    // neew to open box for path, check path. if its real - put in map and set this as current detector. 
                    // make some animation while detecting the file until the anomaliesreport is generated??
                    // if not good
                    model.CurrentDetector = model.DetectorsList[0];
                    DLLInsertWindow win = new DLLInsertWindow(this);
                    win.Show();
                    // 
                    //NotifyPropertyChanged("VM_CurrentDetector");
                }
                else
                    model.CurrentDetector = value; // this works for some reason*/
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
                model.DetectorsList = value;
            }
        }
        
        public bool VM_IsDetectorOn { get { return model.IsDetectorOn; } set { } }

        public void DetectorPropertyChange(Object sender, PropertyChangedEventArgs e)
        {
            // example for one
            if (string.Compare(e.PropertyName, "DllMap") == 0)
            {
                //VM_DllMap = model.DllMap;
                NotifyPropertyChanged("VM_DllMap");
                return;
            }
            if (string.Compare(e.PropertyName, "CurrentDetector") == 0)
            {
                //VM_CurrentDetector = model.CurrentDetector;
                NotifyPropertyChanged("VM_CurrentDetector");
                return;
            }
            if (string.Compare(e.PropertyName, "IsDetectorOn") == 0)
            {
                //VM_IsDetectorOn = model.IsDetectorOn;
                NotifyPropertyChanged("VM_IsDetectorOn");
                return;
            }
            if (string.Compare(e.PropertyName, "DetectorsList") == 0)
            {
                //VM_DetectorsList = model.DetectorsList;
                NotifyPropertyChanged("VM_DetectorsList");
                return;
            }
            if (string.Compare(e.PropertyName, "GetDetector") == 0)
            {
                DLLInsertWindow win = new DLLInsertWindow(this);
                win.Show();
                //VM_DetectorsList = model.DetectorsList;
                //NotifyPropertyChanged("VM_DetectorsList");
                return;
            }
        }
        public DetectorViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += DetectorPropertyChange;
        }

        public bool VM_ValidateDLLPath(string path)
        {
            return model.ValidateDLLPath(path);
        }
       // public string VM_InsertDLLPath { get { return model.InsertDLLPath; } set { model.InsertDLLPath = value; } }
        //public string VM_InsertDLLName { get { return model.InsertDLLName; } set { model.InsertDLLName = value; } }
    }
}
