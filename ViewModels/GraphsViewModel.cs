using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace FlightSimulator.ViewModels
{
    public class GraphsViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model; // this is a try to hold only one model for all controllers!
        LineSeries lineSerie = new LineSeries();

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsViewModel(IFlightSimulatorModel m)

        {
         
            this.model = m;

             model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
             {

                 OnPropertyChanged("VM_" + e.PropertyName);
             };
            
            PlotModel = new PlotModel();
            SetUpModel();
            LoadData();
            PlotModel.Series.Add(lineSerie);
        }

        private string chosenAttribute;

        public string VM_ChosenAttribute
        {
            get { return model.ChosenAttribute; }
            set
            {
                if (VM_ChosenAttribute != value)
                {
                    model.ChosenAttribute = value;
                    onPropertyChanged("VM_AttUserChoose");///??

                }

            }
        }
        public void onPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }







        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private DateTime lastUpdate = DateTime.Now;



        private readonly List<OxyColor> colors = new List<OxyColor>
                                            {
                                                OxyColors.Green,
                                                OxyColors.IndianRed,
                                                OxyColors.Coral,
                                                OxyColors.Chartreuse,
                                                OxyColors.Azure
                                            };

        private readonly List<MarkerType> markerTypes = new List<MarkerType>
                                                   {
                                                       MarkerType.Plus,
                                                       MarkerType.Star,
                                                       MarkerType.Diamond,
                                                       MarkerType.Triangle,
                                                       MarkerType.Cross
                                                   };


        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);

        }

        private void LoadData()
        {
            Data data = new Data(this.model);
            List<Measurement> measurements = data.GetData();

            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            foreach (var data_Proprety in dataPerDetector)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = colors[data_Proprety.Key],
                    MarkerType = markerTypes[data_Proprety.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Detector {0}", data_Proprety.Key),

                };

                data_Proprety.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
                PlotModel.Series.Add(lineSerie);
            }
            lastUpdate = DateTime.Now;
        }

        public void UpdateModel()

        {
      
            List<float> Y = new List<float>();

            Y.Add(float.Parse(model.DataMap[model.ChosenAttribute][10 * (int)model.Timer].ToString()));
            foreach (var d in Y)
            {
                if (lineSerie != null)
                {
                    lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(model.Timer), d));
                }
                else
                {
                    PlotModel.Series.Add(lineSerie);
                }
            }
            PlotModel = plotModel;
            lastUpdate = DateTime.Now;
            PlotModel.InvalidatePlot(true);
        }
    }
}

