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
    /// Interaction logic for BookAppointment.xaml
    /// </summary>
    public partial class BookAppointment : UserControl
    {
        BookAppointmentViewModel viewModel = new BookAppointmentViewModel();
        public BookAppointment()
        {
            InitializeComponent();
        }

        private void Consul_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s="";
            if (ConsultType.SelectedItem != null)
            {
                s = ConsultType.SelectedItem.ToString();

            }
            List<string> list = new List<string>();
            list = viewModel.consulchange(s);
            Doctor.ItemsSource = list;
            Datepicker.SelectedDate = null;
        }

        private void Doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fromtime.ItemsSource = null;
            Endtime.ItemsSource = null; 
     
            string doc = "";
            DateTime selectedDate=DateTime.Now;
            if (Doctor.SelectedItem != null && Datepicker.SelectedDate != null)
            {
                doc = Doctor.SelectedItem.ToString();
                selectedDate = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy"));
            }
            viewModel.docSelChanged(doc, selectedDate);
            Fromtime.ItemsSource = viewModel.slotlist;
            Endtime.ItemsSource = viewModel.slotlist2;
            
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string selectedDep="";
            string doc = "";
            DateTime selectedDate = DateTime.Now;
            if (ConsultType.SelectedItem != null && Doctor.SelectedItem != null && Datepicker.SelectedDate != null)
            {
                selectedDep = ConsultType.SelectedItem.ToString();
                doc = Doctor.SelectedItem.ToString();
                selectedDate = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy"));
            }
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;
            if (Fromtime.SelectedItem != null)
            {
                string s = Fromtime.SelectedItem.ToString();
                DateTime dt;
                DateTime.TryParse(s, out dt);
                string Time = dt.ToString("HH:mm:ss");
                d1 = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy ") + Time);
            }
            if (Endtime.SelectedItem != null)
            {
                string s = Endtime.SelectedItem.ToString();
                DateTime dt1;
                DateTime.TryParse(s, out dt1);
                string Time = dt1.ToString("HH:mm:ss");
                d2 = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy ") + Time);
            }

            if (isValid())
            {
                viewModel.Add(selectedDep, selectedDate, doc, d1, d2);

            }
        }

        public bool isValid()
        {

            if (ConsultType.SelectedItem == null)
            {
                MessageBox.Show("Consult Type is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Doctor.SelectedItem == null)
            {
                MessageBox.Show("Please select a Doctor", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Datepicker.SelectedDate == null)
            {
                MessageBox.Show("A Date should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Fromtime.SelectedValue == null)
            {
                MessageBox.Show("A valid from time should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Endtime.SelectedValue == null)
            {
                MessageBox.Show("A valid End time should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if ((Convert.ToInt32(Fromtime.SelectedItem.ToString().Substring(0, 5).Remove(2, 1))) >= (Convert.ToInt32(Endtime.SelectedItem.ToString().Substring(0, 5).Remove(2, 1))))
            {
                Fromtime.SelectedItem = null;
                Endtime.SelectedItem = null;
                MessageBox.Show("Enter valid From time and To time", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;


        }


    }
}
