using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for FlightSimulatorView.xaml
    /// </summary>
    public partial class FlightSimulatorView : Page
    {
        public void StartSim(object sender, RoutedEventArgs e) {
            //ThreadPool.QueueUserWorkItem(_ => { Dispatcher.BeginInvoke(new Action(()=>{vm.VM_IsPlay = true;}));});
            vm.VM_IsPlay = true;
        }
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
            this.media.DataContext = f.mediaVM;
            this.media.SetVM(f.mediaVM);
                        Loaded += StartSim;
            
        }

    }
}
