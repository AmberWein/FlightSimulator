using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;
using System.ComponentModel;



namespace FlightSimulator.ViewModels
{

    public class GearControlViewModel : INotifyPropertyChanged
    {
            private IFlightSimulatorModel model; // this is a try to hold only one model for all controllers!
             public event PropertyChangedEventHandler PropertyChanged;
        public GearControlViewModel(IFlightSimulatorModel m){
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public float VM_Rudder
        {
            get
            {
                return model.Rudder;
            }
            set
            {

            }
        }
        public float VM_Throttle
        {
            get
            {
                return model.Throttle;
            }
            set
            {
              

            }
        }
        public float VM_Elevator
        {
            get
            {
                return model.Elevator;
            }
            set
            {


            }
        }
        public float VM_Aileron
        {
            get
            {
                return model.Aileron;
            }
            set
            {


            }
        }
        public float VM_Left
        {
            get
            {
                return (200 * (model.Elevator) + 125);
            }
            set
            {


            }
        }
        public float VM_Top
        {
            get
            {
                return 60*(model.Aileron) + 125;
            }
            set
            {


            }
        }
        public void NotifyPropertyChanged(String propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //model.PropertyChanged += DashboardPropertyChange;
    }
    
}
