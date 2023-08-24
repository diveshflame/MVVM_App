using Microsoft.Identity.Client;
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

        private void Consul_SelectionChanged(object sender, SelectionChangedEventArgs e) //changing Doctor name with dept
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

        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e) //Setting the From time and To time
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
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            if (Fromtime.SelectedItem != null)
            {
                string s = Fromtime.SelectedItem.ToString();
                DateTime dateTemp;
                DateTime.TryParse(s, out dateTemp);
                string Time = dateTemp.ToString("HH:mm:ss");
                date1 = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy ") + Time); //Date with selected From time
            }
            if (Endtime.SelectedItem != null)
            {
                string s = Endtime.SelectedItem.ToString();
                DateTime dateTemp2;
                DateTime.TryParse(s, out dateTemp2);
                string Time = dateTemp2.ToString("HH:mm:ss");
                date2 = DateTime.Parse(Datepicker.SelectedDate.Value.ToString("dd/MM/yyyy ") + Time); //Date with selected To Time
            }

            if (isValid())
            {
                viewModel.Add(selectedDep, selectedDate, doc, date1, date2);  //To insert into the Database

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

        private void Doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s=Doctor.SelectedItem.ToString(); 
            
            DateTime? Blackout_Date=viewModel.Blackout(s);
            if (Blackout_Date.HasValue)
            {
                Datepicker.BlackoutDates.Add(new CalendarDateRange(Blackout_Date.Value));
            }
        }
    }
}
