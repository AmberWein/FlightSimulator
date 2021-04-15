
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using FlightSimulator.ViewModels;
using System.Diagnostics;
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
        bool isFirst;
        // public ArrayList data { get; private set; }
        private long frameCounter;
        private System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        private long lastUpdateMilliSeconds;

        public GraphsView()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTargetRendering;
            stopwatch.Start();

            isFirst = true;

        }

        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            if (isFirst && vm.VM_Attributes != null)
            {
                isFirst = false;
                FillList();
                Binding dict = new Binding
                {
                    Source = attributes
                };
                atrributesBox.SetBinding(ComboBox.ItemsSourceProperty, dict);
                stopwatch.Start();
               
                // maybe add other two lines
            }
            if (vm.VM_ChosenAttribute != null)
            {
                // vm.UpdateModelCorr();

                //update every 5 sec
                if (stopwatch.ElapsedMilliseconds > lastUpdateMilliSeconds + 5000)
                {
                    vm.UpdateModelReg();
                    
                    lastUpdateMilliSeconds = stopwatch.ElapsedMilliseconds;

                }
            }
            
         
        }

        public void SetVM(GraphsViewModel graphsVM)
        {

            this.vm = graphsVM;
            
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
                vm.LoadRegModel();
            }


        }
    }
}

