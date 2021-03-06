using System;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
using Syncfusion.UI.Xaml.Gauges;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private DashboardViewModel vm;
        public DashboardView()
        {
            InitializeComponent();
        }
        // Setting the control board view model
        public void SetVM(DashboardViewModel dashVM)
        {
            this.vm = dashVM;
        }

        private void OrientaitionGauge_LabelCreated(Object sender, LabelCreatedEventArgs args)
        {
            switch ((string)args.LabelText)
            {
                case "0":
                    args.LabelText="N";
                    break;
                case "45":
                    args.LabelText="NE";
                    break;
                case "90":
                    args.LabelText="E";
                    break;
                case "135":
                    args.LabelText="SE";
                    break;
                case "180":
                    args.LabelText="S";
                    break;
                case "225":
                    args.LabelText = "SW";
                    break;
                case "270":
                    args.LabelText="W";
                    break;
                case "315":
                    args.LabelText="NW";
                    break;


            }
        }
    }
}
