
﻿using System;
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

//using OxyPlot.Axes;
using OxyPlot.Annotations;
using OxyPlot.Reporting;


using System.Collections.Generic;
using System.Collections;
using FlightSimulator.ViewModels;

﻿using System.Windows.Controls;



//        <oxy:PlotView x:Name="Plot2" Model="{Binding Path= PlotModelCorr}" Margin="0,9.667,10,-227.333" Grid.Row="3" Background="Blue" Height="90" Grid.RowSpan="4" ></oxy:PlotView>


using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Charts;

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
           
                CompositionTarget.Rendering += CompositionTargetRendering;
           
        }

        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            if (vm.VM_ChosenAttribute != null)
            {
                vm.UpdateModel();
           vm.UpdateModelCorr();
            }


            //      < oxy:PlotView x:Name = "Plot2" Model = "{Binding Path= PlotModelCorr}" Margin = "0,9.667,10,-227.333" Grid.Row = "3" Background = "Blue" Height = "90" Grid.RowSpan = "4" ></ oxy:PlotView >*/
        }
        public void SetVM(GraphsViewModel graphsVM)
        {

            this.vm = graphsVM;
            FillList();
            Binding dict = new Binding
            {
                Source = attributes
            };
            atrributesBox.SetBinding(ComboBox.ItemsSourceProperty, dict);          
        }
    
    
        //fil list of attributes with info from viewModel
        private void FillList()
        {
            if (vm != null)
            {

                this.attributes = new List<String>();
                foreach (var v in vm.VM_Attributes)
                {
                    this.attributes.Add(v.ToString());
                }
            }
        }


       //when a attribute is selected- run its graph

        private void atrributesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.VM_ChosenAttribute != null)
            {

                vm.LoadFromStart();
            }

           
        }
    }
}

