using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using System.Collections.ObjectModel;
using System.Collections;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Threading;


namespace FlightSimulator.ViewModels
{
    public class GraphsViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model; // this is a try to hold only one model for all controllers!
        LineSeries lineSerie = new LineSeries();
        LineSeries lineSerie_c = new LineSeries();
        LineSeries lineSerieReg = new LineSeries();
        ScatterSeries recentPoints = new ScatterSeries();

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsViewModel(IFlightSimulatorModel m)

        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                OnPropertyChanged("VM_" + e.PropertyName);
            };

            ///create and set the graphs
            PlotModel = new PlotModel();
            SetUpModel();
            PlotModelCorr = new PlotModel();
            SetUpModelCorr();
            PlotModelReg = new PlotModel();
            SetUpModelReg();
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Timer" && model.IsPlay == true)//we want to update graph for each sec passing. dont worry, we wont miss points - we have a loop to find all points sonce last update
                {
                    if (model.Timer == (int)model.Timer) //this is a start of second
                    {
                        UpdateModel();
                        UpdateModelCorr();
                    }
                }
            };
             model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
             {
                 if (e.PropertyName == "IsPlay")
                {
                    if (!(model.IsPlay)) //handle an event of user pressing stop: we want to clean graphs
                    {
                         VM_ChosenAttribute = null;

                    }
                }

            };
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
                onPropertyChanged("VM_CorrelatedFeatures");

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
        public void onPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
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
            if(correlated_feature == null || correlated_feature == "")
            {
                PlotModelCorr.Title = "Correlated Feature: Not Found";
            }
           
           
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "correlated feature" };
            PlotModelCorr.Axes.Add(valueAxis);

        }
        //C:\Users\user\Desktop\reg_flight.csv
        private void SetUpModel()
        {
            if (VM_ChosenAttribute == null)
            {
                PlotModel.Title = "Choose Attribute to Display Graph";
            }
          
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = VM_ChosenAttribute };
             PlotModel.Axes.Add(valueAxis);

        }
        //Set up the model for regression line
        private void SetUpModelReg()
        {

            PlotModelReg.Title = "Linear Regression";

             var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Y" };
             PlotModelReg.Axes.Add(valueAxis);
        }

        //loading the series from start of flight untill now when an attribute is chosen
        public void LoadFromStart()

        {
            //should do this calculation outside
            foreach (KeyValuePair<string, string> v in VM_CorrelatedFeatures)
            {
                if (v.Key == VM_ChosenAttribute)
                {
                    correlated_feature = v.Value;
                    break;

                }
            }
            //clean previous feature graphs.user has chosen a fearture and we have to restart the graphs from the start
            lineSerie.Points.Clear();
            PlotModel.Series.Clear();
            lineSerie_c.Points.Clear();
            PlotModelCorr.Series.Clear();


            PlotModel.Title = VM_ChosenAttribute;


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
                    PlotModelCorr.Title = correlated_feature;

                    //add points to the corralated feature graph
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
          
                if (VM_ChosenAttribute != null )
                {
                    List<float> Y = new List<float>();
                //create the val of new point
                for (double i =  (model.Timer - 1.0); i <= model.Timer; i += 0.1) //(model.Timer - 1.0) is start of past sec since we last updated - find point since
                {
                    Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][(int)(model.Frequency * i)].ToString()));
                }
                    foreach (var d in Y)
                    {
                        if (lineSerie != null)
                        {
                            //add point to serie
                            lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(model.Timer), d));
                        }
                        else
                        {
                            PlotModel.Series.Add(lineSerie);
                        }
                    }
                    //apply the changes in series
                    PlotModel.InvalidatePlot(true);
                }
                


            
                
              
          }

          //updating the model of corralated attribute throughout the flight on a regular basis
          public void UpdateModelCorr()

          {
            if (VM_ChosenAttribute != null)
            {
                List<float> Y = new List<float>();
            if (correlated_feature != "")
            {
                    for (double i = (model.Timer - 1.0); i <= model.Timer; i += 0.1) //(model.Timer - 1.0) is start of past sec since we last updated - find point since
                    {
                        //add current point to the series
                        Y.Add(float.Parse(model.DataMap[correlated_feature][(int)(model.Frequency * i)].ToString()));
                    }
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
                //apply the changes in series
                PlotModelCorr.InvalidatePlot(true);
            }
               }  
          }


        //updating the model throughout the flight ob a regular basis

        public void UpdateModelReg()

        {
            //should do this calculation outside
            foreach (KeyValuePair<string, string> v in VM_CorrelatedFeatures)
            {
                if (v.Key == VM_ChosenAttribute)
                {
                    correlated_feature = v.Value;
                    break;

                }
            }
            if (PlotModelReg.Series.Contains(recentPoints))
            {
                recentPoints.Points.Clear();
                PlotModelReg.Series.Remove(recentPoints);
                
            }
            if (VM_ChosenAttribute != null && correlated_feature != null)
            {

                if (model.Timer > 90.0)
                {
                   
                    //recent points
                    for (double i = (model.Timer - 90.0); i < model.Timer;  i+= 0.1)
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
                PlotModelReg.Series.Add(recentPoints);
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
                PlotModelReg.Series.Add(lineSerieReg);
                PlotModelReg.InvalidatePlot(true);
            }
        




        }


      




    }
    }


