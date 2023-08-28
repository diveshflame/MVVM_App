using System;
using System.Collections.Generic;
using System.Globalization;
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
            DateTime defaultDate = DateTime.Today;
            Datepicker1.SelectedDate = defaultDate;
            Datepicker2.SelectedDate = defaultDate;

        }
        public List<string> docType { get; set; }
        private void doctorType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            AddTimingsViewModel docChange = new AddTimingsViewModel();
            Datepicker1.BlackoutDates.Clear();
            Datepicker2.BlackoutDates.Clear();
            DateTime defaultDate = DateTime.Today;
            Datepicker1.SelectedDate = null;
            Datepicker2.SelectedDate = null;

            DisableWeekends();
            DisablePastDates();
            string s = "";
            if (doctorType1.SelectedItem != null)
            {
                s = doctorType1.SelectedItem.ToString();
            }

            List<DateTime> Blackout_Date = docChange.Blackout(s);
            foreach (DateTime dat in Blackout_Date)
            {
                
                Datepicker1.BlackoutDates.Add(new CalendarDateRange(dat));
                Datepicker2.BlackoutDates.Add(new CalendarDateRange(dat));
            }


        }

        private void ConsultType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTimingsViewModel selectCon = new AddTimingsViewModel();
            if (ConsultType1.SelectedItem != null)
            {
                docType = selectCon.selectDoc(ConsultType1.SelectedItem.ToString());
            }
            doctorType1.ItemsSource = docType;

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
