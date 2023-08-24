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
using MVVM_App.ViewModels;

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddTimings : UserControl
    {
        public AddTimings()
        {
            InitializeComponent();
            DisableWeekends();  //Greying out Past Dates and weekends
            DisablePastDates();
           
        }
        public List<string> consultType { get; set; }
        private void doctorType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTimingsViewModel selectDoc = new AddTimingsViewModel();
            consultType = selectDoc.selectDoc(doctorType1.SelectedItem.ToString());
            ConsultType1.ItemsSource = consultType; 
        }

        private void ConsultType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true)
            {
                Fromtime1.IsEnabled = false;
                Endtime1.IsEnabled = false;
            }
            else
            {
                Fromtime1.IsEnabled = true;
                Endtime1.IsEnabled = true;
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == false)
            {
                Fromtime1.IsEnabled = true;
                Endtime1.IsEnabled = true;
            }
            else
            {

                Fromtime1.IsEnabled = false;
                Endtime1.IsEnabled = false;

            }
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            int Temp2 = 0;//variables to store checker status
            int Temp = 0;
           
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            DateTime startDate=DateTime.Now;
             DateTime endDate=DateTime.Now; 
            string FromTime="";
            string EndTime="";
            if (Datepicker1.SelectedDate != null && Datepicker2.SelectedDate != null)
            {
                startDate = Datepicker1.SelectedDate.Value;
                endDate = Datepicker2.SelectedDate.Value;
            }
            if (Fromtime1.SelectedItem != null && Endtime1.SelectedItem != null)
            {
                FromTime = Fromtime1.SelectedItem.ToString();
              EndTime= Endtime1.SelectedItem.ToString();
            }
            AddTimingsViewModel selectC = new AddTimingsViewModel();
            if (isValid())
            {
                if (Fromtime1.SelectedItem != null)
                {
                    string s = Fromtime1.SelectedItem.ToString();
                    DateTime DateTime1;
                    DateTime.TryParse(s, out DateTime1);
                    string Time = DateTime1.ToString("HH:mm");
                    string datestring = Datepicker1.SelectedDate.Value.ToString("yyyy/MM/dd");
                    DateTime parsedDate = DateTime.ParseExact(datestring, "yyyy/MM/dd", null);
                    date1 = DateTime.Parse(parsedDate.ToString("yyyy/MM/dd ") + Time);
                }
                if (Endtime1.SelectedItem != null)
                {
                    string s = Endtime1.SelectedItem.ToString();
                    DateTime DateTime2;
                    DateTime.TryParse(s, out DateTime2);
                    string Time = DateTime2.ToString("HH:mm");
                    date2 = DateTime.Parse(Datepicker1.SelectedDate.Value.ToString("yyyy/MM/dd ") + Time);
                }

             Temp= selectC.Selectcon(date1, date2, doctorType1.SelectedItem.ToString(), startDate, endDate);

                if (checker1.IsChecked == false)
                {
                    Temp2 = 1;
                }
                if (Temp == 0)
                {
                    selectC.check(date1, date2, doctorType1.SelectedItem.ToString(), startDate, endDate, FromTime, EndTime, Temp2);
                }
                
            }
        }

        public bool isValid()
        {

            if (doctorType1.SelectedItem == null)
            {
                MessageBox.Show("Please Enter a Doctor", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ConsultType1.SelectedItem == null)
            {
                MessageBox.Show("Consult Type is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Datepicker1.SelectedDate == null)
            {
                MessageBox.Show("A valid From Date should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Datepicker2.SelectedDate == null)
            {
                MessageBox.Show("A valid To Date should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (checker1.IsChecked == false && (Fromtime1.SelectedItem == null || Endtime1.SelectedItem == null))
            {
                MessageBox.Show("Enter valid From and To", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Datepicker1.SelectedDate > Datepicker2.SelectedDate)
            {
                MessageBox.Show("Enter valid From date and To date", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            TimeSpan selectedTime1= new TimeSpan();
            TimeSpan selectedTime2= new TimeSpan();
            if (Fromtime1.SelectedItem != null && Endtime1.SelectedItem != null)
            {
                selectedTime1 = TimeSpan.Parse(Fromtime1.SelectedItem.ToString());
                selectedTime2 = TimeSpan.Parse(Endtime1.SelectedItem.ToString());
            }
            if (selectedTime1 > selectedTime2 && checker1.IsChecked == false)
            {
                Fromtime1.SelectedItem = null;
                Endtime1.SelectedItem = null;
                MessageBox.Show("Enter valid From time and To time", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            return true;
        }
        private void DisableWeekends()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddYears(1); // Set an end date (1 year from today in this example)
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }
            while (startDate <= endDate)
            {
                Datepicker1.BlackoutDates.Add(new CalendarDateRange(startDate.AddDays(5), startDate.AddDays(6)));
                Datepicker2.BlackoutDates.Add(new CalendarDateRange(startDate.AddDays(5), startDate.AddDays(6)));// Friday and Saturday
                startDate = startDate.AddDays(7);
            }
        }
        private void DisablePastDates()
        {
            Datepicker1.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            Datepicker2.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }
    }
}
