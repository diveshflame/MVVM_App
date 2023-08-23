using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for UpdateDoctor.xaml
    /// </summary>
    public partial class UpdateDoctor : UserControl
    {
        public UpdateDoctor()
        {
            InitializeComponent();
        }

        private void UpdateDoctorButton(object sender, RoutedEventArgs e)
        {
            UpdateDoctorViewModel add = new UpdateDoctorViewModel();
            //add.updatedoc(Doctor.Text, ConsultType.SelectedItem.ToString());
        }
    }
}
