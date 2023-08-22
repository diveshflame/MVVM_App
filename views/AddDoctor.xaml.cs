using MVVM_App.ViewModels;
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

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : UserControl
    {
        public AddDoctor()
        {
            InitializeComponent();
         
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            AddDoctorViewModel add = new AddDoctorViewModel();
            add.Insert(Doctor.Text,ConsultType.SelectedItem.ToString());
        }
    }
}
