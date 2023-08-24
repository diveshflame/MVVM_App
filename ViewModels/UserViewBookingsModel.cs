using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class UserViewBookingsModel : ViewModelBase
    {
        //Fields
        private ObservableCollection<DataGridItem> userDatagridItems;

        private ObservableCollection<DataGridItem> userNewDatagridItems;
        private bool _isDataGridVisible = true;
        private bool _isDataGridVisible2 = true;
        private bool _isbuttonvisible = true;
        public bool IsDataGridVisible { get => _isDataGridVisible; set { _isDataGridVisible = value; OnPropertyChanged(nameof(IsDataGridVisible)); } }

        public bool Isbuttonvisible { get => _isbuttonvisible; set { _isbuttonvisible = value; OnPropertyChanged(nameof(Isbuttonvisible));} }

        public bool IsDataGridVisible2 { get => _isDataGridVisible2; set { _isDataGridVisible2 = value; OnPropertyChanged(nameof(IsDataGridVisible2));}
}
        public ObservableCollection<DataGridItem> UserDatagridItems { get => userDatagridItems; set { userDatagridItems = value; OnPropertyChanged(nameof(UserDatagridItems)); } }
        public ObservableCollection<DataGridItem> UserNewDatagridItems { get => userNewDatagridItems; set { userNewDatagridItems = value; OnPropertyChanged(nameof(UserNewDatagridItems)); } }

        //Commands
        public ICommand ViewUserBookings { get;  }
        public ICommand ViewUserTodayBooking { get; }
        public ICommand ViewUserBookingHistory { get; }
         
        public ICommand DeleteUserBookings { get; }

        private IAdminBooking adminbooking;



        //constructor
        public UserViewBookingsModel()
        {
            adminbooking = new DataGridRepository();
            ViewUserBookings = new ViewModelCommand(ExecuteViewUserBookings);
            ViewUserTodayBooking = new ViewModelCommand(ExecuteViewUserTodayBooking);
            ViewUserBookingHistory = new ViewModelCommand(ExecuteViewUserBookingHistory);
            IsDataGridVisible = true;
            IsDataGridVisible2 = false;
            LoadData();
        }

     

        private void ExecuteViewUserBookingHistory(object obj)
        {
            IsDataGridVisible = false;
            IsDataGridVisible2 = true;
            UserViewBookingsModel view = new UserViewBookingsModel();
            UserNewDatagridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewUsersHistory();
            foreach (var item in dataGridItem)
            {
                UserNewDatagridItems.Add(item);
            }
        }

        private void ExecuteViewUserTodayBooking(object obj)
        {
            IsDataGridVisible = false;
            IsDataGridVisible2 = true;
            UserViewBookingsModel view = new UserViewBookingsModel();
            UserNewDatagridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewUsersTodayBooking();
            foreach (var item in dataGridItem)
            {
                UserNewDatagridItems.Add(item);
            }
        }

        private void ExecuteViewUserBookings(object obj)
        {
            IsDataGridVisible = true;
            IsDataGridVisible2 = false;
            UserViewBookingsModel view = new UserViewBookingsModel();
            LoadData();
        }
        public void LoadData()
        {
          
            UserDatagridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewUserBookings();
            foreach (var item in dataGridItem)
            {
                //DateTime starttime = item.StartTime;
                //var Differencedate = starttime - DateTime.Now;
                //if (Differencedate.Days < 2)
                //{
                //    item.Isbuttonvisible = false;
                //}
                UserDatagridItems.Add(item);
            }
            
          
        }

        public void DeleteAppointment(int BookingId, int DoctorId, DateTime startTime, DateTime endTime)
        {
            UserViewBookingsModel view = new UserViewBookingsModel();
            UserDatagridItems = new ObservableCollection<DataGridItem>();
            DataGridItem dataGridItems = new DataGridItem();
            dataGridItems.Booking_Id = BookingId;
            dataGridItems.Doctor_Id = DoctorId;
            dataGridItems.StartTime = startTime; 
            dataGridItems.EndTime = endTime;

         
            bool isvalid = adminbooking.DeleteUserBooking(BookingId, DoctorId,startTime,endTime);
            if(isvalid)
            {
             UserDatagridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewUserBookings();
            foreach (var item in dataGridItem)
            {
                UserDatagridItems.Add(item);
            }
        }
      }


}
}




    
