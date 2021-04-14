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
    /// Interaction logic for SetPage.xaml
    /// </summary>
    public partial class SetPage : Page
    {
        private SetViewModel vm;
        //private Window hostWindow;
        private bool isValidPath;
        public SetPage(SetViewModel v, Window w)
        {
            vm = v;
          //  hostWindow = w;
            DataContext = vm;
            isValidPath = false;
            InitializeComponent();
        }
        private void pathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //PageOperator.OperateButton(true);
            invalidMessage.Visibility = Visibility.Hidden;
            bool isValidPath = PageOperator.CheckPath(pathBox.Text);
            if (isValidPath)
            {
                vm.VM_CsvPath = pathBox.Text;
                PageOperator.OperateButton(true);
            }
            else
            {
                invalidMessage.Visibility = Visibility.Visible;
                PageOperator.OperateButton(false);
            }
             // need to make next button enabled

            // when on - should make NextButton for test
        }


    }
}
