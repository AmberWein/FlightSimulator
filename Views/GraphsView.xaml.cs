
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
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Charts;

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
                //prob need to do some threading
               vm.UpdateModel();
                 vm.UpdateModelCorr();
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

