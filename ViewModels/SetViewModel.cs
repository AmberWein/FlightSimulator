using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class SetViewModel : INotifyPropertyChanged
    {
        private ISetModel model;
        public SetViewModel(ISetModel m)
        {
            this.model = m;
            // make this a listener for PropertyChanged of model
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }
        // INotifyPropertyChanged implementations
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }   
        }
        
        public string VM_CsvPath
        {
            get
            {
                return model.CsvPath;
            }
            set
            {
                model.CsvPath = value;
            }
        }
        /*public Dictionary<string, ArrayList> VM_DataMap
        {
            get
            {
                return model.DataMap;
            }
        }*/

    }
}
