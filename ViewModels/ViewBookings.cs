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
        private ObservableCollection<DataGridItem> dataGridItems;
        public ObservableCollection<DataGridItem> DataGridItems { get => dataGridItems; set { dataGridItems = value; OnPropertyChanged(nameof(DataGridItems)); } }

        //Commands
        public string searchContent { get; set; }   

    
        public ICommand ViewAdminBookings { get; }
        public ICommand ViewTodayBooking { get; }
        public ICommand ViewBookingHistory { get; }

        public ICommand SearchCommand { get; }

        private IAdminBooking adminbooking;

        //constructor
        public ViewBookings() 
        {
            adminbooking = new DataGridRepository();
            ViewAdminBookings = new ViewModelCommand(ExecuteViewAdminBookings);
            ViewBookingHistory = new ViewModelCommand(ExecuteViewBookingHistory);
            ViewTodayBooking = new ViewModelCommand(ExecuteViewTodayBooking);
            searchContent = "Enter your text here..";
            SearchCommand = new ViewModelCommand(ExecuteSearch);
            DataGridItems = new ObservableCollection<DataGridItem>();
            LoadData();
        }

        public void ExecuteSearch(object obj)
        {
            DataGridRepository data = new DataGridRepository();
            DataGridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem=data.Search(searchContent);
            foreach (var item in dataGridItem)
            {
                DataGridItems.Add(item);
            }
        }

   

        private void ExecuteViewTodayBooking(object obj)
        {
            ViewBookings view = new ViewBookings();
            DataGridItems = new ObservableCollection<DataGridItem>();
            var dataGridItem = adminbooking.ViewToday();
            foreach (var item in dataGridItem)
            {
                DataGridItems.Add(item);
            }
        }

        private void ExecuteViewBookingHistory(object obj)
        {
            ViewBookings view = new ViewBookings();
            DataGridItems = new ObservableCollection<DataGridItem>();

            var dataGridItem = adminbooking.ViewHistory();
            foreach (var item in dataGridItem)
            {
                DataGridItems.Add(item);
            }
        }

        private void ExecuteViewAdminBookings(object obj)
        {
            ViewBookings view = new ViewBookings();
            DataGridItems = new ObservableCollection<DataGridItem>();
            LoadData();
        }
        private void LoadData()
        {
            var dataGridItem = adminbooking.View();
            foreach (var item in dataGridItem)
            {
                DataGridItems.Add(item);
            }
        }
       
    }
}
