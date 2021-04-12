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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.ViewModels;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for GearControl.xaml
    /// </summary>
    public partial class GearControlView : UserControl { 
          private GearControlViewModel vm;

        public GearControlView()
        {
            InitializeComponent();
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

