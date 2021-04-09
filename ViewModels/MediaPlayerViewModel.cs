using System;
using System.ComponentModel;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    class MediaPlayerViewModel: INotifyPropertyChanged
    {
        private IMediaPlayerModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public MediaPlayerViewModel(IMediaPlayerModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool VM_IsPlay
        {
            get { return model.IsPlay; }
            set
            {
                model.IsPlay = value;
            }
        }

        public double VM_Speed
        {
            get { return model.Speed; }
            set
            {
                model.Speed = value;
            }
        }

        public double VM_Timer
        {
            get { return model.Timer; }
            set
            {
                if (value <= 0)
                    model.Timer = 0;
                else
                    model.Timer = value;
            }
        }

        public bool VM_UserIsDraggingSlider
        {
            get { return model.UserIsDraggingSlider; }
            set
            {
                model.UserIsDraggingSlider = value;
            }
        }
    }
}
