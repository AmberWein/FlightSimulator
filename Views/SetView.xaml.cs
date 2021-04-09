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
using FlightSimulator.ViewModels;
using FlightSimulator.Models;
using FlightSimulator.IO;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for SetView.xaml
    /// </summary>
    public partial class SetView : Page
    {
        private SetViewModel vm;
        private bool isValidPath;
        private bool isFirstChange;
        // constructor
        public SetView()
        {
            InitializeComponent();
            vm = new SetViewModel(new SetModel());
            DataContext = vm;
            isValidPath = false;
            isFirstChange = true;
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileParser.IsValidPath(pathBox.Text))
            {
                isValidPath = true;
            }
            if (isValidPath)
            {
                Switcher.Switch();
                // vm.VM_Csv(vm.VM_Path,)
                //S
            }
            else
            {
                startButton.IsEnabled = false;
                invalidMessage.Visibility = Visibility.Visible;
            }
        }
        private void pathBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isFirstChange)
            {
                //pathBox.Text = "";
                isFirstChange = false;
            }
        }
        private void pathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            startButton.IsEnabled = true;
            invalidMessage.Visibility = Visibility.Hidden;
        }
    }
}
