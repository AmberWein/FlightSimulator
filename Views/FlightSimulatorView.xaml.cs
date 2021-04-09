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

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for FlightSimulatorView.xaml
    /// </summary>
    public partial class FlightSimulatorView : Page
    {
        public FlightSimulatorView()
        {
            InitializeComponent();
        }
        private FlightSimulatorViewModel vm;
        public FlightSimulatorViewModel Vm
        {
            get { return vm;}
            set { vm = value;}
        }
        public FlightSimulatorView(FlightSimulatorViewModel f)
        {
            InitializeComponent();
            vm = f;
            DataContext = vm;
            this.dash.DataContext = f.dashVM;
            this.dash.SetVM(f.dashVM);
        }
    }
}
