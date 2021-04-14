using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
using FlightSimulator.IO;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for InsertDLLView.xaml
    /// </summary>
    public partial class InsertDLLView : UserControl
    {
        private DetectorViewModel vm;
        private bool nameBoxGotFocus;
        private bool pathBoxGotFocus;
        public InsertDLLView()
        {
            InitializeComponent();
            vm = null;
            nameBoxGotFocus = false;
            pathBoxGotFocus = false;
        }
         public void SetVM(DetectorViewModel detectVM)
        {
            this.vm = detectVM;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_IsDetectorOn = false;
            // check that dll path is valid
            if (FileParser.IsDLL(DLLpath.Text) && vm.VM_ValidateDLLPath(DLLpath.Text))
            {
                // check that name does not already exist
                if (vm.VM_DetectorsList.Contains(DLLname.Text))
                {
                    OkButton.IsEnabled = false;
                    nameBoxGotFocus = false;
                    invalidityMessage.Text = "Name already exists. Please Choose a different name.";
                    invalidityMessage.Visibility = Visibility.Visible;
                }
                else
                { // path and name are valid
                    vm.VM_DllMap.Add(DLLname.Text, DLLpath.Text);
                    vm.VM_DetectorsList.Insert(vm.VM_DetectorsList.Count - 1, DLLname.Text);
                    vm.VM_DetectorsList = vm.VM_DetectorsList;
                   // System.Threading.Thread.Sleep(150);
                    
                    vm.VM_CurrentDetector = vm.VM_DetectorsList[vm.VM_DetectorsList.Count - 2];
                    //List<string> l = vm.VM_DetectorsList;
                    //vm.VM_DetectorsList.Insert(vm.VM_DetectorsList.Count - 1, DLLname.Text);
                    // l.Insert(vm.VM_DetectorsList.Count - 1, DLLname.Text);

                    vm.VM_GetDetector = false;
                    //vm.VM_DetectorsList = l;
                    //vm.VM_CurrentDetector = DLLname.Text;
                    //vm.NotifyPropertyChanged("VM_DetectorsList");
                    Window.GetWindow(this).Close();

                }
            }
            else
            {
                OkButton.IsEnabled = false;
                invalidityMessage.Text = "Path is invalid";
                invalidityMessage.Visibility = Visibility.Visible;
                pathBoxGotFocus = false;
            }
        }
        private void DLLPathBox_GotFocus(object sender, RoutedEventArgs e)
        {
            invalidityMessage.Visibility = Visibility.Hidden;

            if (nameBoxGotFocus)
            {
                OkButton.IsEnabled = true;
            }
            pathBoxGotFocus = true;
        }
        private void DLLNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            invalidityMessage.Visibility = Visibility.Hidden;
            if (pathBoxGotFocus)
            {
                OkButton.IsEnabled = true;
            }
            nameBoxGotFocus = true;
        }
    }
}
