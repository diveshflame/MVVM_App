﻿using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Repositories;
using WpfApp4.ViewModels;

namespace MVVM_App.ViewModels
{
    public class ViewBookings: ViewModelBase
    {
        //Fields
        private ObservableCollection<AdminDataGridItem> adminDataGridItems;
        public ObservableCollection<AdminDataGridItem> AdminDataGridItems { get => adminDataGridItems; set { adminDataGridItems = value; OnPropertyChanged(nameof(adminDataGridItems)); } }

        //Commands
        public ICommand ViewAdminBookings { get; }
        private IAdminBooking adminbooking;

        //constructor
        public ViewBookings() 
        {
            adminbooking = new AdminDataGridRepository();
            ViewAdminBookings = new ViewModelCommand(ExecuteViewAdminBookings);
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
