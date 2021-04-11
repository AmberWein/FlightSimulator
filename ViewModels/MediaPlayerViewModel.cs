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
                   // VM_Timer = model.Timer;
                    NotifyPropertyChanged("VM_Timer");
                    return;
                }
                if (string.Compare(e.PropertyName, "PlayingSpeed") == 0)
                {
                    //VM_PlayingSpeed = model.PlayingSpeed;
                    NotifyPropertyChanged("VM_PlayingSpeed");
                    return;
                }
                if (string.Compare(e.PropertyName, "FinishTime") == 0)
                {
                    //M_FinishTime = model.FinishTime;
                    NotifyPropertyChanged("VM_FinishTime");
                    return;
                }
                /*if (string.Compare(e.PropertyName, "LineNumber") == 0)
                {
                    VM_LineNumber = model.LineNumber;
                    //VM_Timer = 0.1 * (double)VM_LineNumber;
                    // NotifyPropertyChanged("VM_Timer");
                   
                    VM_Timer = TimeSpan.FromSeconds(0.1 * VM_LineNumber).ToString(@"hh\:mm\:ss");;
                    NotifyPropertyChanged("VM_LineNumber");
                    return;
                }
                if (string.Compare(e.PropertyName, "MaxLine") == 0)
                {
                    //VM_MaxLine = model.MaxLine;
                    VM_FinishTime = 0.1 * (double)VM_MaxLine;
                    //NotifyPropertyChanged("VM_MaxLine");
                   NotifyPropertyChanged("VM_FinishTime");
                    return;
                };*/
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

        // private double vm_Timer;
        // public double VM_Timer
        //{
        //   get { //return model.Timer;
        //     return vm_Timer;
        //return 0.1 * (double)model.LineNumber;
        //}
        //set
        //{
        //   if (value <= 0)
        //   {
        //      VM_Timer = 0;
        //     model.LineNumber = 0;
        // }
        // else
        //{// what will happen? vm_Timer need to be changed. but should line also change? or it changes anyway? 
        //   if (vm_Timer + 0.1 < value)
        //  {
        ///      VM_LineNumber = (int )(value * 10.0);
        //}
        //vm_Timer = value;

        // lineNumber??
        //   model.LineNumber = (int)(value * 10.0);
        //               }
        //if (value <= 0)
        //    model.Timer = 0;
        //else
        //    model.Timer = value;
        //         }
        //    }
        public double VM_Timer
        {
            get { return model.Timer; }
            set {
                model.Timer = value;
                /*notify?*/ }
        }
        public double VM_FinishTime
        {
            get { return model.FinishTime; }
           // set{ model.FinishTime = value; } 
        }
        public bool VM_IsPlay
        {
            get { return model.IsPlay; }
            set { model.IsPlay = value; }
        }
      /*  public int VM_LineNumber
        {
            get { return model.LineNumber; }
            set { model.LineNumber = value; }
        }

        public int VM_MaxLine
        {
            get { return model.MaxLine; }
            set { model.MaxLine = value; }
        }*/
    }
}


