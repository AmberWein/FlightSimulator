using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model; // this is a try to hold only one model for all controllers!
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public float VM_Yaw
        {
            get { return model.Yaw; }
            set
            { //model.Yaw = value; }
            }
        }
        public float VM_Pitch
        {
            get
            {
                return model.Pitch;
            }
            set { }

        }
        public float VM_Roll
        {
            get
            {
                return model.Roll;
            }
                 set { }
        }
        public float VM_Orientation
        {
            get
            {
                return model.Orientation;
            }
                 set { }
        }
        public float VM_Altitude
        {
            get
            {
                return model.Altitude;
            }
                 set { }
        }
        public float VM_AirSpeed
        {
            get
            {
                return model.AirSpeed;
            }
                 set { }
        }
        public void DashboardPropertyChange(Object sender, PropertyChangedEventArgs e)
        {
            // example for one
            if (string.Compare(e.PropertyName, "Yaw") == 0)
            {
                VM_Yaw = model.Yaw;
                NotifyPropertyChanged("VM_Yaw");
                return;
            }
            if (string.Compare(e.PropertyName, "Roll") == 0)
            {
                VM_Roll = model.Roll;
                NotifyPropertyChanged("VM_Roll");
                return;
            }
            if (string.Compare(e.PropertyName, "Pitch") == 0)
            {
                VM_Pitch = model.Pitch;
                NotifyPropertyChanged("VM_Pitch");
                return;
            }
            if (string.Compare(e.PropertyName, "Orientation") == 0)
            {
                VM_Orientation = model.Orientation;
                NotifyPropertyChanged("VM_Orientation");
                return;
            }
            if (string.Compare(e.PropertyName, "Altitude") == 0)
            {
                VM_Altitude = model.Altitude;
                NotifyPropertyChanged("VM_Altitude");
                return;
            }
            if (string.Compare(e.PropertyName, "AirSpeed") == 0)
            {
                VM_AirSpeed = model.AirSpeed;
                NotifyPropertyChanged("VM_AirSpeed");
                return;
            }

        }
        public DashboardViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += DashboardPropertyChange;
        }
    }
}
