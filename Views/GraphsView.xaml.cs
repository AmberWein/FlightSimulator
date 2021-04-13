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
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;
using OxyPlot.Reporting;

using System.Collections.Generic;
using System.Collections;
using FlightSimulator.ViewModels;




using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

//C:\Users\user\Desktop\reg_flight.csv
namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl

    {
        //private Models.FlightSimulatorModel viewModel;

       private GraphsViewModel vm;
        private string chosenAttribute;
        List<string> attributes;
        // public ArrayList data { get; private set; }
        public GraphsView()
        {
            InitializeComponent();
            FillList();


            Binding dict = new Binding
            {
                Source = attributes
            };
            atrributesBox.SetBinding(ComboBox.ItemsSourceProperty, dict);
            


            CompositionTarget.Rendering += CompositionTargetRendering;
        }

        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            vm.UpdateModel();
        }
        public void SetVM(GraphsViewModel graphsVM)
        {
            this.vm = graphsVM;
        }

        //fil list of attributes with info from viewModel
        private void FillList()
        {
            if (vm == null) { }
          
                this.attributes = new List<String>();
                foreach (var v in vm.VM_Attributes)
                {
                    this.attributes.Add(v.ToString());
                }


                this.attributes.Add("indicated-heading-deg");
                this.attributes.Add("latitude-deg");
                this.attributes.Add("engine_rpm");
            



        }


         private void Button_Click(object sender, RoutedEventArgs e)
         {
            vm.VM_ChosenAttribute = null;
            vm.LoadFromStart();
         }

        private void atrributesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.LoadFromStart();

        }
    }
}

