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

    }
}
