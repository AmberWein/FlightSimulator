﻿using FlightSimulator.Models;
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

        public MediaPlayerView()
        {
            FillList();
            InitializeComponent();
            vm = new MediaPlayerViewModel(new MediaPlayerModel());
            DataContext = vm;
            Binding dict = new Binding
            {
                Source = speeds
            };
            speedBox.SetBinding(ComboBox.ItemsSourceProperty, dict);
        }
        private void FillList()
        {
            this.speeds = new List<double>();

            for (double i = 0; i <= 2; i += 0.25)
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
        private void Backwards_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            //if (vm.VM_Timer >= 0.1)
            //vm.VM_Timer -= 0.1;
        }
        private void Play_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Timer -= 0.1;
        }
        private void Stop_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.VM_Timer != 0)
                this.vm.VM_Timer = 0;

            if (this.vm.VM_Speed != 0)
                this.vm.VM_Speed = 0;
        }
        private void Pause_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.VM_Speed != 0)
                this.vm.VM_Speed = 0;
        }
        private void Forward_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Timer += 0.1;
        }

        private void Speed_Changed_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem Speed_Box = ((sender as ListBox).SelectedItem as ListBoxItem);
            this.vm.VM_Speed = Convert.ToDouble(Speed_Box.Content.ToString());
        }

        private void Drag_Started_Click(object sender, RoutedEventArgs e)
        {
            //vm.VM_UserIsDraggingSlider = true;
        }
        private void Drag_Completed_Click(object sender, RoutedEventArgs e)
        {
            //vm.VM_UserIsDraggingSlider = false;
            //mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }
        private void Time_Changed_Click(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.vm.VM_Speed = Convert.ToDouble(e.NewValue.ToString());
        }
    }
}
