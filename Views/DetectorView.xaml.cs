using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.IO;
using FlightSimulator.Models;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for DetectorView.xaml
    /// </summary>
    public partial class DetectorView : UserControl
    {
        private DetectorViewModel vm;
        public DetectorView()
        {
            InitializeComponent();
        }
        public void SetVM(DetectorViewModel detectVM)
        {
            this.vm = detectVM;
        }
        private void AddToList(string path, string name)
        {
            this.vm.VM_DetectorsList.Add(name);
            this.vm.VM_DllMap.Add(name, path);
        }

        private void DetectButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AnomaliesWindow win = new AnomaliesWindow();
            string name = FlightSimulatorModel.GetRelativePath("bin\\Debug", "AnomaliesReport.txt");
            /*Dictionary<string, ArrayList> d = FileParser.GetAnomalies(name);
            string text = "";
            foreach(string key in d.Keys)
            {
                text += (key+":\n");
                foreach (long num in d[key])
                    text += (num.ToString() + ",");
                text += "\n";
            }*/
            string text = FileParser.GetAnomalies(name);
            win.AnomaliesTextBlock.Text = text;
            win.Show();
        }
    }
}
