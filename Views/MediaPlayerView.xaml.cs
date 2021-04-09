using FlightSimulator.Models;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Media_Player_View.xaml
    /// </summary>
    public partial class Media_Player_View : UserControl
    {
        private MediaPlayerViewModel vm;
        public Media_Player_View()
        {
            InitializeComponent();
            vm = new MediaPlayerViewModel(new MediaPlayerModel());
            DataContext = vm;
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
            this.vm.VM_IsPlay = true;
        }
        private void Stop_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Timer = 0;
            this.vm.VM_IsPlay = false;
        }
        private void Pause_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_IsPlay = false;
        }
        private void Forward_Media_Player_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Timer += 1;
        }

        private void Speed_Changed_Click(object sender, RoutedEventArgs e)
        {
            //vm.VM_Speed = Convert.ToDouble(e.Source.GetType().GetProperty("Speed").GetValue(e.Source, null));
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
        private void Time_Changed_Click(object sender, RoutedEventArgs e)
        {
            //lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }
    }
}
