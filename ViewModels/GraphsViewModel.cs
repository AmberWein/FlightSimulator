using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot;
using System.Collections;




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

                 NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
    /*    public ScatterSeries VM_Data
            
        {
            get
            {
                var s = new ScatterSeries();
                for (int i = 0; i < 30; i*= 5)
                {
                    float n = (float.Parse(model.DataMap["heading-deg"][i].ToString()));
                    s.Points.Add(new OxyPlot.Series.ScatterPoint(i, i + 9));
                }

                return s;
            }
            set{ }
        

        }*/

    



        public void NotifyPropertyChanged(String propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

