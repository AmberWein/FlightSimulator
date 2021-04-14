using System;
using System.ComponentModel;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class FlightSimulatorViewModel : INotifyPropertyChanged
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
        
        // Flight Simulator Controllers ViewModels
        public DashboardViewModel DashboardVM { get; internal set; }
        public MediaPlayerViewModel MediaPlayerVM { get; internal set; }

        public GraphsViewModel GraphsVM { get; internal set; }
        public GearControlViewModel GearControlVM { get; internal set; }

        public DetectorViewModel DetectorVM { get; internal set; }

        public FlightSimulatorViewModel(IFlightSimulatorModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
            // create the viewModels that will contact the model
            DashboardVM = new DashboardViewModel(this.model);
            MediaPlayerVM = new MediaPlayerViewModel(this.model);
            GearControlVM = new GearControlViewModel(this.model);
            GraphsVM = new GraphsViewModel(this.model);
            DetectorVM = new DetectorViewModel(this.model);

        }
        public bool VM_IsPlay { get { return model.IsPlay; } set { model.IsPlay = true; } }
        
       /* public Dictionary<string, ArrayList> VM_DataMap
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
        }*/
        
    }






}
