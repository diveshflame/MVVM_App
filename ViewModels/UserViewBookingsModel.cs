using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class UserViewBookingsModel:ViewModelBase
    {
        //Fields
        private ObservableCollection<DataGridItem> userDatagridItems;

        public ObservableCollection<DataGridItem> UserDatagridItems { get => userDatagridItems; set { userDatagridItems = value; OnPropertyChanged(nameof(UserDatagridItems)); } }

        //Commands
        public ICommand ViewUserBookings { get;  }
        public ICommand ViewUserTodayBooking { get; }
        public ICommand ViewUserBookingHistory { get; }

        private IAdminBooking adminbooking;


        //constructor
        public UserViewBookingsModel()
        {
            adminbooking = new DataGridRepository();
            ViewUserBookings = new ViewModelCommand(ExecuteViewUserBookings);
            ViewUserTodayBooking = new ViewModelCommand(ExecuteViewUserTodayBooking);
            ViewUserBookingHistory = new ViewModelCommand(ExecuteViewUserBookingHistory);
           
        }

        private void ExecuteViewUserBookingHistory(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteViewUserTodayBooking(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteViewUserBookings(object obj)
        {
          
            UserViewBookingsModel view = new UserViewBookingsModel();
            UserDatagridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewUserBookings();
            foreach (var item in dataGridItem)
            {
                UserDatagridItems.Add(item);
            }
        }

     

    }
}