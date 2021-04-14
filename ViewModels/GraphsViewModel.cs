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
        ScatterSeries scatterPoints = new ScatterSeries();
        ScatterSeries recentPoints = new ScatterSeries();

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
            PlotModelCorr = new PlotModel();
            SetUpModelCorr();
            PlotModelReg = new PlotModel();
            SetUpModelReg();

        }
     
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
        private ArrayList correlatedFeatures;
        string correlated_feature;


        public ArrayList VM_CorrelatedFeatures
        {
            get { return model.CorrelatedFeatures; }
            set
            {
                model.CorrelatedFeatures = value;
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
            PlotModelCorr.LegendTitle = "correlated feature";
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
             PlotModel.LegendTitle = model.ChosenAttribute;
             PlotModel.LegendOrientation = LegendOrientation.Horizontal;
             PlotModel.LegendPlacement = LegendPlacement.Outside;
             PlotModel.LegendPosition = LegendPosition.LeftTop;
             PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.Black);
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
            foreach (KeyValuePair<string, string> v in VM_CorrelatedFeatures)
            {
                if (v.Key == VM_ChosenAttribute)
                {
                    correlated_feature = v.Value;
                    break;

                }
            }
            //clean previous
            lineSerie.Points.Clear();
            PlotModel.Series.Clear();
            lineSerie_c.Points.Clear();
            PlotModelCorr.Series.Clear();


            //add
            PlotModel.Series.Add(lineSerie);
            PlotModelCorr.Series.Add(lineSerie_c);
            List<float> Y = new List<float>();
            List<float> Y_c = new List<float>();
           


                {
                    for (double i = 0; i < model.Timer; i += 0.1)
                    {
                        Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency * i)].ToString()));
                    if (correlated_feature != "") //not all attributes have corrlated features
                    {
                        Y_c.Add(float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * i)].ToString()));
                    }
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
                if (correlated_feature != "") //not all attributes have corrlated features
                {
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
                   
                }

                PlotModelCorr.InvalidatePlot(true);
            }
            
        }
          //updating the model throughout the flight ob a regular basis

          public void UpdateModel()

          {
            if (VM_ChosenAttribute != null)
            {
                List<float> Y = new List<float>();
                Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency * model.Timer)].ToString()));
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
            if (correlated_feature != "")
            {
                Y.Add(float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * model.Timer)].ToString()));
                foreach (var d in Y)
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

        public void UpdateModelReg()

        {
            PlotModelReg.InvalidatePlot(true);
            foreach (KeyValuePair<string, string> v in VM_CorrelatedFeatures)
            {
                if (v.Key == VM_ChosenAttribute)
                {
                    correlated_feature = v.Value;
                    break;

                }
            }
            if (VM_ChosenAttribute != null && correlated_feature != null)
            {


                //last 30 seconds points - in red
               
          
                PlotModelReg.Series.Add(recentPoints);

                if (model.Timer > 30)
                {
                   
                    //recent points
                    for (double i = (model.Timer - 30); i < model.Timer;  i+= 0.1)
                    {
                        recentPoints.Points.Add(new ScatterPoint(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency * model.Timer)].ToString()), float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * model.Timer)].ToString()), 3));
                    }
                }
                else
                {  //all points are recent points
                    for (double i = 0; i < model.Timer; i += 0.1)
                    {
                        recentPoints.Points.Add(new ScatterPoint(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency* model.Timer)].ToString()), float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * model.Timer)].ToString()), 3));
                    }


                }

                PlotModelReg.InvalidatePlot(true);
              
                
            }


        }


        public void LoadRegModel()
          {
            foreach (KeyValuePair<string, string> v in VM_CorrelatedFeatures)
            {
                if (v.Key == VM_ChosenAttribute)
                {
                    correlated_feature = v.Value;
                    break;

                }
            }
            if (correlated_feature != "" && correlated_feature != null)
            {
                scatterPoints.Points.Clear();
                lineSerieReg.Points.Clear();
               
                PlotModelReg.Series.Clear();

                ArrayList points = model.fromFloatsToPoints(model.DataMap[VM_ChosenAttribute], model.DataMap[correlated_feature]);

                Line reg_line = model.linear_reg(points, points.Count);

                float Max_x = float.Parse(model.DataMap[VM_ChosenAttribute].ToArray().Max().ToString());
                float Min_x = float.Parse(model.DataMap[VM_ChosenAttribute].ToArray().Min().ToString());
                float y_hat_max = reg_line.y_hat(Max_x);
                float y_hat_min = reg_line.y_hat(Min_x);


                lineSerieReg.Points.Add(new DataPoint(Max_x, y_hat_max));
                lineSerieReg.Points.Add(new DataPoint(Min_x, y_hat_min));

               /* var scatterPoints = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle,
                    MarkerFill = OxyColors.Gray
                };*/
                for (double i = 0; i < model.FinishTime-0.9; i+= 0.1)
                {
                    scatterPoints.Points.Add(new ScatterPoint(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency * model.Timer)].ToString()), float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * model.Timer)].ToString()), 3));
                }
                PlotModelReg.Series.Add(lineSerieReg);
                PlotModelReg.Series.Add(scatterPoints);
                PlotModelReg.InvalidatePlot(true);
            }
             //else write no corratlive attribute
            //scatter series copy from first load from start




        }




    }
    }


