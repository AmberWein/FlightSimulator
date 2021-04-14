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
using FlightSimulator.ViewModels;
using FlightSimulator.IO;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SetViewModel setVM;
        public SetViewModel SetVM
        {
            get
            {
                return setVM;
            }
            set { setVM = value; }
        }
        private FlightSimulatorViewModel flightSimVM;
        public FlightSimulatorViewModel FlightSimVM
        {
            get
            {
                return flightSimVM;
            }
            set { flightSimVM = value; }
        }
        private bool isFirstPage;
        //public MainWindow()
        /*public MainWindow(SetViewModel sVM, FlightSimulatorViewModel fVM)
        {
            isFirstPage = true;
            InitializeComponent();
            setVM = sVM;//added 
            flightSimVM = fVM;//added
            Switcher.pageSwitcher = this;
            Switcher.Switch();
            //MainFrame.Content = entry;
            //MainFrame.Content = simulator;
        }*/
        private List<Page> pages;
        int pgIndex;
        public MainWindow(SetViewModel sVM, FlightSimulatorViewModel fVM)
        {
            isFirstPage = true;

            setVM = sVM;//added 
            flightSimVM = fVM;//added
            pages = new List<Page>();
            pages.Add(new OpeningPage());
            pages.Add(new SetPage(setVM, this));
            pages.Add(new LetsStartPage());
            pages.Add(new FlightSimulatorView(flightSimVM));
            InitializeComponent();
            pgIndex = 0;
            MainFrame.Content = pages[pgIndex];
            //Switcher.pageSwitcher = this;
            //Switcher.Switch();
            //MainFrame.Content = entry;
            //MainFrame.Content = simulator;
            PageOperator.pageOperator = this;
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pgIndex == 0)
            {
                BackButton.Visibility = Visibility.Visible;
                NextButton.Content = "Next";
            }
            pgIndex++;
            /*if (pgIndex == 1)
            {
                NextButton.IsEnabled = false;
            }*/
            // turn on back button
            if (!BackButton.IsEnabled)
            {
                BackButton.IsEnabled = true;
            }
            // if this is last page, diable back button
            if (pgIndex == pages.Count - 1)
            {
                NextButton.IsEnabled = false;
                NextButton.Visibility = Visibility.Hidden;
            }

            MainFrame.Content = pages[pgIndex];
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (pgIndex == (pages.Count - 1))
            {
                NextButton.IsEnabled = true;
                NextButton.Visibility = Visibility.Visible;
            }
            pgIndex--;
            // if this is last page, diable back button
            if (pgIndex == 0)
            {
                BackButton.IsEnabled = false;
                BackButton.Visibility = Visibility.Hidden;
                NextButton.Content = "Start";
            }
            // turn on back button
            if (!NextButton.IsEnabled)
            {
                NextButton.IsEnabled = true;
            }
            MainFrame.Content = pages[pgIndex];
        }
        public void Navigate()
        {
            if (isFirstPage)
            {
                isFirstPage = false;
                Page entry = new SetView(setVM);
                this.Content = entry;
            }
            else
            {
                Page simulator = new FlightSimulatorView(flightSimVM);
                this.Content = simulator;
                //  flightSimVM.VM_IsPlay = true;
            }
        }
        public bool IsCSVPathValid(string path)
        {
            // do something
            if (CSVParser.IsCSV(path))
            {
                if (FileParser.IsValidPath(path))
                {
                    return true;
                }
            }
            return false;
        }
        public void OperateNextButton(bool val)
        {
            if (!val) // if path was not valid
            {
                NextButton.IsEnabled = false;   
            }
            else
            {
                NextButton.IsEnabled = true;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public static class PageOperator
    {
        public static MainWindow pageOperator;
        public static bool CheckPath(string path)
        {
            return pageOperator.IsCSVPathValid(path);
        }
        public static void OperateButton(bool val)
        {
            pageOperator.OperateNextButton(val);
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
