using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Markup;
using MVVM_App.Models;
using MVVM_App.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class AddTimingsViewModel : ViewModelBase
    {
        public IAddDocRepo repocall = new AddRepo();
        int a = 0;
        public List<string> consultTypes { get; set; }
        public List<string> Doctor { get; set; }
        public List<string> StartTime { get; set; }
        public List<string> EndTime { get; set; }
        public AddTimingsViewModel()
        {
            consultTypes = repocall.getco();

            StartTime = repocall.startT();

            EndTime = repocall.EndT();

            AddTimings = new ViewModelCommand(ExecuteAddTimings);

        }
        public List<string> selectDoc(string text)
        {
            AddRepo addRepo = new AddRepo();

            Doctor = addRepo.selectionchangedoc1(text);
            return Doctor;


        }


        public List<DateTime> Blackout(string? s)
        {
            AddRepo addRepo = new AddRepo();
            List<DateTime> dt = addRepo.black(s);
            return dt;
        }

        private DateTime _selectedDate1;

        public DateTime SelectedDate1
        {
            get { return _selectedDate1; }
            set
            {
                if (_selectedDate1 != value)
                {
                    _selectedDate1 = value;
                    OnPropertyChanged(nameof(SelectedDate1));
                }
            }
        }
        private DateTime _selectedDate2;

        public DateTime SelectedDate2
        {
            get { return _selectedDate2; }
            set
            {
                if (_selectedDate2 != value)
                {
                    _selectedDate2 = value;
                    OnPropertyChanged(nameof(SelectedDate2));
                }
            }
        }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }
        public ICommand AddTimings { get; }

        private string selectedFromTime;

        public string SelectedFromTime { get => selectedFromTime; set { selectedFromTime = value; OnPropertyChanged(nameof(SelectedFromTime)); } }

        private string selectedEndTime;

        public string SelectedEndTime { get => selectedEndTime; set { selectedEndTime = value; OnPropertyChanged(nameof(SelectedEndTime)); } }

        private string selectedDoc;

        public string SelectedDoc { get => selectedDoc; set { selectedDoc = value; OnPropertyChanged(nameof(SelectedDoc)); } }

        private string consultSelected;

        public string ConsultSelected { get => consultSelected; set { consultSelected = value; OnPropertyChanged(nameof(ConsultSelected)); } }


        private void ExecuteAddTimings(object obj)
        {
            int Temp2 = 0;//variables to store checker status
            int Temp = 0;

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string FromTime = "";
            string EndTime = "";
            if (SelectedDate1 != null && SelectedDate2 != null)
            {
                startDate = SelectedDate1;
                endDate = SelectedDate2;
            }
            if (SelectedFromTime != null && SelectedEndTime != null)
            {
                FromTime = SelectedFromTime.ToString();
                EndTime = SelectedEndTime.ToString();
            }
            AddTimingsViewModel selectC = new AddTimingsViewModel();
            if (isValid())
            {
                if (SelectedFromTime != null)
                {
                    string s = SelectedFromTime.ToString();
                    DateTime DateTime1;
                    DateTime.TryParse(s, out DateTime1);
                    string Time = DateTime1.ToString("HH:mm");
                    string datestring = SelectedDate1.ToString("yyyy/MM/dd");
                    DateTime parsedDate = DateTime.ParseExact(datestring, "yyyy/MM/dd", null);
                    date1 = DateTime.Parse(parsedDate.ToString("yyyy/MM/dd ") + Time);
                }
                if (SelectedEndTime != null)
                {
                    string s = SelectedEndTime.ToString();
                    DateTime DateTime2;
                    DateTime.TryParse(s, out DateTime2);
                    string Time = DateTime2.ToString("HH:mm");
                    date2 = DateTime.Parse(SelectedDate1.ToString("yyyy/MM/dd ") + Time);
                }
                AddRepo addRepo = new AddRepo();
                Temp = addRepo.selectionconchanged(date1, date2, SelectedDate1.ToString(), startDate, endDate);
   

                if (IsChecked == false)
                {
                    Temp2 = 1;
                }
                if (Temp == 0)
                {
                AddRepo addRepo1 = new();
                addRepo1.ischecked(date1, date2, SelectedDoc.ToString(), startDate, endDate, FromTime, EndTime, Temp2);
                }
                 SelectedDoc = null;
                 ConsultSelected = null;
                 SelectedFromTime = null;
                 SelectedEndTime = null;
                 IsChecked = false;

         }
        }



        public bool isValid()
        {

            if (selectedDoc == null)
            {
                MessageBox.Show("Please Enter a Doctor", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (ConsultSelected == null)
            {
                MessageBox.Show("Department is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (SelectedDate1 == null)
            {
                MessageBox.Show("A valid From Date should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (SelectedDate2 == null)
            {
                MessageBox.Show("A valid To Date should be selected", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (IsChecked == false && (SelectedFromTime == null || SelectedEndTime == null))
            {
                MessageBox.Show("Enter valid From and To", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (SelectedDate1 > SelectedDate2)
            {
                MessageBox.Show("Enter valid From date and To date", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            TimeSpan selectedTime1 = new TimeSpan();
            TimeSpan selectedTime2 = new TimeSpan();
            if (SelectedFromTime != null && SelectedEndTime != null)
            {
                selectedTime1 = TimeSpan.Parse(SelectedFromTime.ToString());
                selectedTime2 = TimeSpan.Parse(SelectedEndTime.ToString());
            }
            if (selectedTime1 > selectedTime2 && IsChecked == false)
            {
                SelectedFromTime = null;
                SelectedEndTime = null;
                MessageBox.Show("Enter valid From time and To time", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            return true;
        }
    }
}
