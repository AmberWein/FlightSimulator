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
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }
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
        public Dictionary<string, ArrayList> VM_DataMap //might not be beeded here at all. depends where connection to Simulator happens(model or VM)
            // if we keep then add CM_DataLines
        {
            get
            {
                return model.DataMap;
            }
        }

    }
}
