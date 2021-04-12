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
        public bool VM_IsPlay { get { return model.IsPlay; } set { model.IsPlay = true; } }
        public DashboardViewModel dashVM { get; internal set; }
        public GearControlViewModel gearVM { get; internal set; }
        public GraphsViewModel graphsVM { get; internal set; }

        public FlightSimulatorViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };

            dashVM = new DashboardViewModel(this.model);
            gearVM = new GearControlViewModel(this.model);
            graphsVM = new GraphsViewModel(this.model);
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


        
        /*this is a try to generate a vm for controller that will be directed to thie model*/
        
    }
}
