using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulator.Models;
using FlightSimulator.ViewModels;
using FlightSimulator.Views;
namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ISetModel setModel = new SetModel();
            SetViewModel setVM = new SetViewModel(setModel);
            IFlightSimulatorModel simModel = new FlightSimulatorModel(setModel);
            FlightSimulatorViewModel flightSimVM = new FlightSimulatorViewModel(simModel);
            // does main window run simultanasle?
            MainWindow window = new MainWindow(setVM, flightSimVM);
 
            window.ShowDialog();
        }
    }
}
