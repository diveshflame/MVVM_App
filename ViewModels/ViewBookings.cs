using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM_App.Repositories;
using MVVM_App.ViewModels;
using System.Windows.Forms;

namespace MVVM_App.ViewModels
{
    public class ViewBookings: ViewModelBase
    {
        //Fields
        private ObservableCollection<AdminDataGridItem> adminDataGridItems;
        public ObservableCollection<AdminDataGridItem> AdminDataGridItems { get => adminDataGridItems; set { adminDataGridItems = value; OnPropertyChanged(nameof(adminDataGridItems)); } }

        //Commands
        public ICommand ViewAdminBookings { get; }
        public ICommand ViewTodayBooking { get; }
        public ICommand ViewBookingHistory { get; }
        private IAdminBooking adminbooking;

        //constructor
        public ViewBookings() 
        {
            adminbooking = new AdminDataGridRepository();
            ViewAdminBookings = new ViewModelCommand(ExecuteViewAdminBookings);
            ViewBookingHistory = new ViewModelCommand(ExecuteViewBookingHistory);
            ViewTodayBooking = new ViewModelCommand(ExecuteViewTodayBooking);
            AdminDataGridItems = new ObservableCollection<AdminDataGridItem>();
               LoadData();
        }

        private void ExecuteViewTodayBooking(object obj)
        {
            ViewBookings view = new ViewBookings();
            AdminDataGridItems = new ObservableCollection<AdminDataGridItem>();
           
            var dataGridItem = adminbooking.ViewToday();
            foreach (var item in dataGridItem)
            {
                AdminDataGridItems.Add(item);
            }
        }

        private void ExecuteViewBookingHistory(object obj)
        {
             ViewBookings view = new ViewBookings();
            AdminDataGridItems = new ObservableCollection<AdminDataGridItem>();

            var dataGridItem = adminbooking.ViewHistory();
            foreach (var item in dataGridItem)
            {
                AdminDataGridItems.Add(item);
            }
        }

        private void ExecuteViewAdminBookings(object obj)
        {
            ViewBookings view = new ViewBookings();
            AdminDataGridItems = new ObservableCollection<AdminDataGridItem>();
            LoadData();
        }
        private void LoadData()
        {
            var dataGridItem = adminbooking.View();
            foreach (var item in dataGridItem)
            {
                AdminDataGridItems.Add(item);
            }
        }
       
    }
}
