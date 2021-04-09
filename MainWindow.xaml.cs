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
using FlightSimulator.Views;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isFirstPage;
        public MainWindow()
        {
            isFirstPage = true;
            InitializeComponent();

            Switcher.pageSwitcher = this;
            Switcher.Switch();
            //MainFrame.Content = entry;
            //MainFrame.Content = simulator;
        }
        public void Navigate()
        {
            if (isFirstPage)
            {
                isFirstPage = false;
                Page entry = new SetView();
                this.Content = entry;
            }
            else
            {
                Page simulator = new FlightSimulatorView();
                this.Content = simulator;
            }
        }
    }

    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch()
        {
            pageSwitcher.Navigate();
        }
    }
}
