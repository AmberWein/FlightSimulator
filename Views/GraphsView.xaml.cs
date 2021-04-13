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
using FlightSimulator.Models;

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl

    {
        //private Models.FlightSimulatorModel viewModel;

        GraphsViewModel vm;
        private string chosenAttribute;
        // public ArrayList data { get; private set; }
        public GraphsView()
        {
        

            CompositionTarget.Rendering += CompositionTargetRendering;
            stopwatch.Start();
            InitializeComponent();
         
        }
        private long frameCounter;
        private System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        private long lastUpdateMilliSeconds;
        private void CompositionTargetRendering(object sender, EventArgs e)
        {
           
                vm.UpdateModel();
            
            

        }
        public void SetVM(GraphsViewModel graphsVM)
        {
            this.vm = graphsVM;
        }



        /* var model = new PlotModel { Title = "ScatterSeries" };

         var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
         var r = new Random(314);
         for (int i = 0; i < 100; i++)
         {
             var x = r.NextDouble();
             var y = r.NextDouble();
             var size = r.Next(5, 15);
             var colorValue = r.Next(100, 1000);
             scatterSeries.Points.Add(new ScatterPoint(x, y, size, colorValue));
         }

         model.Series.Add(scatterSeries);
         model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });*/


       /* public void randomPoints()
        {
                    Random rd = new Random();


            String myText = "";

            int anz = rd.Next(30, 60);

            for (int i = 0; i < anz; i++)
                myText += i + "," + rd.Next(0, 99) + ";";

            myText = myText.Substring(0, myText.Length - 1);
            String[] splitText = myText.Split(';');

            for (int i = 0; i < splitText.Length; i++)
            {
                String[] tmp = splitText[i].Split(',');
                Points.Add(new DataPoint(Double.Parse(tmp[0].Trim()), Double.Parse(tmp[1].Trim())));
            }

            while (Points.Count > anz)
                Points.RemoveAt(0);

            myChart.InvalidatePlot(true);
        }*/
    }
}

