using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (isValid())
            {
                AddDoctorViewModel add = new AddDoctorViewModel();
                add.Insert(Doctor.Text, ConsultType.SelectedItem.ToString());
            }
        }
        public bool isValid()
        {

            /* if (timeslot.SelectedItem.ToString() == string.Empty)
             {
                 MessageBox.Show("Timeslot is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                 return false;
             }*/
            int b = 0;
            string expression = "^(?!^\\.)(?!.*\\d)[a-zA-Z.]*$";
            Regex NamePattern = new Regex(expression);
            if (NamePattern.IsMatch(Doctor.Text))
            {
                b = 1;
            }
            if (Doctor.Text == string.Empty || b == 0)
            {
                MessageBox.Show("Please Enter a valid Doctor Name", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (ConsultType.SelectedItem == null)
            {
                MessageBox.Show("Department is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;

        }
    }
}
