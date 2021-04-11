using System;
using System.ComponentModel;
using FlightSimulator.Models;

namespace FlightSimulator.ViewModels
{
    public class MediaPlayerViewModel: INotifyPropertyChanged
    {
        //private IMediaPlayerModel model;
        private IFlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        //public MediaPlayerViewModel(IMediaPlayerModel model)
        public MediaPlayerViewModel(IFlightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (string.Compare(e.PropertyName, "Timer") == 0)
                {
                    VM_Timer = model.Timer;
                    NotifyPropertyChanged("VM_Timer");
                    return;
                }
                if (string.Compare(e.PropertyName, "PlayingSpeed") == 0)
                {
                    VM_PlayingSpeed = model.PlayingSpeed;
                    NotifyPropertyChanged("VM_PlayingSpeed");
                    return;
                }
                if (string.Compare(e.PropertyName, "FinishTime") == 0)
                {
                    VM_FinishTime = model.FinishTime;
                    NotifyPropertyChanged("VM_FinishTime");
                    return;
                };
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public double VM_PlayingSpeed
        {
            get { return model.PlayingSpeed; }
            set
            {
                model.PlayingSpeed = value;
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

        public double VM_FinishTime
        {
            get { return model.FinishTime; }
            set{ } 
        }
        public bool VM_IsPlay
        {
            get { return model.IsPlay; }
            set { model.IsPlay = value; }
        }

    }
}


