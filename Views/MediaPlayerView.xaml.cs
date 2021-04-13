using FlightSimulator.Models;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MediaPlayerView.xaml
    /// </summary>
    public partial class MediaPlayerView : UserControl
    {
        private MediaPlayerViewModel vm;
        List<double> speeds;
        double timeStep = 5;

        public MediaPlayerView()
        {
            FillList();
            InitializeComponent();
            // set binding between the ComboBox's chosen value to the speeds list 
            Binding dict = new Binding
            {
                Source = speeds
            };
            speedBox.SetBinding(ComboBox.ItemsSourceProperty, dict);
        }
        public void SetVM(MediaPlayerViewModel mediaVM)
        {
            this.vm = mediaVM;
        }

        // initialize the speeds list
        private void FillList()
        {
            this.speeds = new List<double>();

            for (double i = 0.25; i <= 2; i += 0.25)
            {
                this.speeds.Add(i);
            }
        }

         private void Open_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            // if(Open_CanExecute(e))
            //{
            //
            //}
        }
        //private void Open_CanExecute(e)
        //{
        //    e.CanExecute = true;
        //}
        // when backward button is pressed, the function reduced (if possible) the timer by a given timeStep value
        private void Backwards_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_Timer >= this.timeStep)
            {
                this.vm.VM_Timer -= timeStep;
            }
            else 
            { 
                this.vm.VM_Timer=0; 
            }
            // binding the current timer's change and show it in lblProgressStatus
            lblProgressStatus.Text = TimeSpan.FromSeconds(vm.VM_Timer).ToString(@"hh\:mm\:ss");
        }
        // when play button is pressed, the function (if needed) makes the app plays
        private void Play_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if (!this.vm.VM_IsPlay)
                this.vm.VM_IsPlay = true;
        }
        // when stop button is pressed, the function (if needed) makes the app stops
        private void Stop_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.VM_IsPlay)
                this.vm.VM_IsPlay = false;
            if (this.vm.VM_Timer != 0)
                this.vm.VM_Timer = 0;
            vm.VM_InitData();
        }
        // when pause button is pressed, the function (if needed) makes the app pauses
        private void Pause_Media_Player_Click(object sender, RoutedEventArgs e)
        {
          if (this.vm.VM_IsPlay)
                this.vm.VM_IsPlay = false;            
        }
        // when forward button is pressed, the function reduced (if possible) the timer property by a given timeStep value
        private void Forward_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if ((this.vm.VM_Timer + this.timeStep) <= this.vm.VM_FinishTime)
            this.vm.VM_Timer += this.timeStep;

            // binding the current timer's change and show it in lblProgressStatus
            lblProgressStatus.Text = TimeSpan.FromSeconds(this.vm.VM_Timer).ToString(@"hh\:mm\:ss");
        }
        // when comboBox button value's is changed, the function update the speed property accordingly
        private void Speed_Changed_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem speedBox = ((sender as ListBox).SelectedItem as ListBoxItem);
            this.vm.VM_PlayingSpeed = Convert.ToDouble(speedBox.Content.ToString());
        }
        // when slider button is dragged, the function update the timer property accordingly
        private void Time_Changed_Click(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            this.vm.VM_Timer = (Convert.ToDouble(sliProgress.Value.ToString()));
        }
    }
}
