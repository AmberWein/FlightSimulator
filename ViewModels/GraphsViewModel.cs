using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;
using System.ComponentModel;




namespace FlightSimulator.ViewModels
{
    public class GraphsViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model; // this is a try to hold only one model for all controllers!
        public event PropertyChangedEventHandler PropertyChanged;
        public GraphsViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                // NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }



        public void NotifyPropertyChanged(String propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

