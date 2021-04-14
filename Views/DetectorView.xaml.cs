using System.Windows.Controls;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for DetectorView.xaml
    /// </summary>
    public partial class DetectorView : UserControl
    {
        private DetectorViewModel vm;
        //private List<string> list;
        public DetectorView()
        {
            InitializeComponent();
        }
        public void SetVM(DetectorViewModel detectVM)
        {
            this.vm = detectVM;
        }
       /* public void initList()
        {
            list.Add("Choose detector");
            list.Add("Simple");
            list.Add("Circular");
            list.Add("Upload detector");
            vm.VM_DetectorsList = list;
        }*/
       
        private void AddToList(string path, string name)
        {
            this.vm.VM_DetectorsList.Add(name);
            this.vm.VM_DllMap.Add(name, path);
        }

    }
}
