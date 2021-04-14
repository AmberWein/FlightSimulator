using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using System.Collections.ObjectModel;
using System.Collections;
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
        LineSeries lineSerie_c = new LineSeries();
        LineSeries lineSerieReg = new LineSeries();
        ScatterSeries ScatterSerie = new ScatterSeries();
        ScatterSeries lastSecsScatterSerie = new ScatterSeries();

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsViewModel(IFlightSimulatorModel m)

        {
            this.model = m;
            //Init();

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                OnPropertyChanged("VM_" + e.PropertyName);
            };

            PlotModel = new PlotModel();
            SetUpModel();
            //  PlotModel.Series.Add(lineSerie);

        }
       /* private void Init()
        {
            Levels =  new DataPointCollection();

            for (int i = 0; i < 500; i++) Levels.AddPoints(0.2 * i, i, 3);
        }

        private DataPointCollection _levels;
        public DataPointCollection Levels
        {
            get { return _levels; ; }
            set
            {
                _levels = value;
                //OnPropertyChanged();
            }
        }*/

        private string chosenAttribute;

        public string VM_ChosenAttribute
        {
            get { return model.ChosenAttribute; }
            set
            {
                model.ChosenAttribute = value;
                onPropertyChanged("VM_ChosenAttribute");
            }
        }

        private ArrayList attributes;

        public ArrayList VM_Attributes
        {
            get { return model.Attributes; }
            set
            {
                model.Attributes = value;
                onPropertyChanged("VM_Attributes");

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
        private PlotModel plotModelCorr;
        public PlotModel PlotModelCorr
        {
            get { return plotModelCorr; }
            set { plotModelCorr = value; OnPropertyChanged("PlotModelCorr"); }
        }
        private PlotModel plotModelReg;

        public PlotModel PlotModelReg
        {
            get { return plotModelReg; }
            set { plotModelReg = value; OnPropertyChanged("PlotModelReg"); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



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

        //Set up the model for corralated features
        private void SetUpModelCorr()
        {
            PlotModelCorr.LegendTitle = "Legend";
            PlotModelCorr.LegendOrientation = LegendOrientation.Horizontal;
            PlotModelCorr.LegendPlacement = LegendPlacement.Outside;
            PlotModelCorr.LegendPosition = LegendPosition.LeftTop;
            PlotModelCorr.LegendBackground = OxyColor.FromAColor(200, OxyColors.Aqua);
            PlotModelCorr.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModelCorr.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModelCorr.Axes.Add(valueAxis);

        }
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
        //Set up the model for regression line
        private void SetUpModelReg()
        {
             PlotModelCorr.LegendTitle = "Legend";
             PlotModelCorr.LegendOrientation = LegendOrientation.Horizontal;
             PlotModelCorr.LegendPlacement = LegendPlacement.Outside;
             PlotModelCorr.LegendPosition = LegendPosition.LeftTop;
             PlotModelCorr.LegendBackground = OxyColor.FromAColor(200, OxyColors.Aqua);
             PlotModelCorr.LegendBorder = OxyColors.Black;

             var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
             PlotModelCorr.Axes.Add(dateAxis);
             var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
             PlotModelCorr.Axes.Add(valueAxis);

        }

        //loading the series from start of flight untill now when an attribute is chosen
        public void LoadFromStart()

        {
            //clean previous
              lineSerie.Points.Clear();
              PlotModel.Series.Clear(); 
              PlotModelCorr.Series.Clear();
              PlotModelReg.Series.Clear();
              PlotModel = new PlotModel();
              PlotModelCorr = new PlotModel();

              SetUpModelCorr();
              SetUpModel();

              //add
              PlotModel.Series.Add(lineSerie);
              PlotModelCorr.Series.Add(lineSerie_c);
              List<float> Y = new List<float>();
              List<float> Y_c = new List<float>();
              if (VM_ChosenAttribute == null) { }
              else

              {
                  for (double i = 0; i < model.Timer; i+= 0.1)
              {
                  Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][10 * (int)i].ToString()));
               //    Y_c.Add(float.Parse(model.DataMap[VM_ChosenAttribute.corrlated][10 * (int)i].ToString()));
                  }


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
                  PlotModel.InvalidatePlot(true);
                   foreach (var d in Y_c)
                   {
                       if (lineSerie_c != null)
                       {
                           lineSerie_c.Points.Add(new DataPoint(DateTimeAxis.ToDouble(model.Timer), d));
                       }
                       else
                       {
                           PlotModelCorr.Series.Add(lineSerie_c);
                       }
                   }
                  PlotModelCorr.InvalidatePlot(true);
              }
          }
          //updating the model throughout the flight ob a regular basis

          public void UpdateModel()

          {
              List<float> Y = new List<float>();
              if (VM_ChosenAttribute == null) { }
              else
              {
                  Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][(int) (10.0 * model.Timer)].ToString()));
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
                  PlotModel.InvalidatePlot(true);
              }
          }

          //updating the model of corralated attribute throughout the flight on a regular basis
          public void UpdateModelCorr()

          {
              List<float> Y = new List<float>();
              // Y.Add(float.Parse(model.DataMap[//][(int)(10.0 * model.Timer)].ToString()));
                  foreach (var d in Y)
                  {
                      if (lineSerie_c != null)
                      {
                          lineSerie_c.Points.Add(new DataPoint(DateTimeAxis.ToDouble(model.Timer), d));
                      }
                      else
                      {
                          PlotModelCorr.Series.Add(lineSerie);
                      }
                  }
                  PlotModelCorr.InvalidatePlot(true);
          }


          public void LoadRegModel()
          {
            lineSerieReg.Points.Clear();
            PlotModelReg.Series.Clear();
            PlotModelReg = new PlotModel();

            /*var lineSerieReg = new LineSeries()
              {
                  Color = OxyColors.Red,
                  StrokeThickness = 2
              };*/



            /*Line reg_line = model.linear_reg(new Point(DataMap[attrubute], DataMap[Corr]));
             float X_Max = DataMap[attribute].max;
            float X_Min = DataMap[attribute].max;
            float Y_a = reg_line.f(X_Max);
            float Y_b = reg_line.f(X_Min);
            lineSerieReg.Add(new DataPoint(X_Max,Y_a)
            lineSerieReg.Add(new DataPoint(X_Min,Y_b)


            PlotModelReg.Series.Add(lineSerieReg);
             */
            //scatter series copy from first load from start
    



        }




    }
    }


