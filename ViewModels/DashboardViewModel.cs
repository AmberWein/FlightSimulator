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
        public void DashboardPropertyChange(Object sender, PropertyChangedEventArgs e)
        {
            // example for one
            if (string.Compare(e.PropertyName, "Yaw") == 0)
            {
                VM_Yaw = model.Yaw;
                NotifyPropertyChanged("VM_Yaw");
            }

        }
        public DashboardViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += DashboardPropertyChange;
        }
    }
}
