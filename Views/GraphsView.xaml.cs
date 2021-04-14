using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
     /*   GraphsViewModel vm;
       // public ArrayList data { get; private set; }
        public GraphsView()
        {
            InitializeComponent();
            var model = new PlotModel() { Title = "hier kehen!" };
            var s = new OxyPlot.Series.ScatterSeries()
            
                 {
                MarkerSize = 0.8f,
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Black
            };
        
            for (int i = 2; i < 300; i *= 5)
            {
               // float n = (float.Parse(model.DataMap["heading-deg"][i].ToString()));
                s.Points.Add(new OxyPlot.Series.ScatterPoint(i, i + 9));
            }
            model.Series.Add(s);
            

            model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Minimum = 0, Maximum = 200, Position = OxyPlot.Axes.AxisPosition.Left });
            model.Axes.Add(new OxyPlot.Axes.LinearAxis() { Minimum = 0, Maximum = 200, Position = OxyPlot.Axes.AxisPosition.Bottom });



            // this.data = new ArrayList();

            // Random rd = new Random();
            // randomPoints();
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

