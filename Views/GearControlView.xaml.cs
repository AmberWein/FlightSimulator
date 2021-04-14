using System;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for GearControl.xaml
    /// </summary>
    public partial class GearControlView : UserControl { 
          private GearControlViewModel vm;
        private Point firstPoint = new Point();

        public GearControlView()
        {
            InitializeComponent();
          //  DataContext = (Application.Current as App).ControlVM;
        }

        // Setting the control board view model
        public void SetVM(GearControlViewModel gearVM)
    {
        this.vm = gearVM;
    }
    
       
        public void centerKnob_Completed(object sender, EventArgs e)
        {

        }

       
    }
}

