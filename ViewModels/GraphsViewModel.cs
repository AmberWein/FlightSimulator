﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Models;
using System;
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
            PlotModel.Series.Add(lineSerie);
        }

        private string chosenAttribute;

        public string VM_ChosenAttribute
        {
            get { return model.ChosenAttribute; }
            set
            {
                    model.ChosenAttribute = value;
                    onPropertyChanged("VM_ChosenAttribute");///??
            }
        }

        private ArrayList attributes;

        public ArrayList VM_Attributes
        {
            get { return model.Attributes; }
            set { model.Attributes = value;
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

        //Set up the model
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

       //loading the series from start of flight untill now when an attribute is chosen
        public void LoadFromStart()

        {
            lineSerie.Points.Clear();
            PlotModel.Series.Clear();
            PlotModel = new PlotModel();
            SetUpModel();
            PlotModel.Series.Add(lineSerie);
            List<float> Y = new List<float>();
            if (VM_ChosenAttribute == null) { }
            else

            {
                for (double i = 0; i < model.Timer; i+= 0.1)
            {
                Y.Add(float.Parse(model.DataMap[VM_ChosenAttribute][10 * (int)i].ToString()));
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
            }
        }
        //updating the model throughout the flight ob a regular bases

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
    }
}

