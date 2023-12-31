﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MVVM_App.Models;
using MVVM_App.Repositories;
using FontAwesome.WPF;
using System.Windows.Input;
using System.ComponentModel;
using MVVM_App.views;
using FontAwesome.Sharp;

namespace MVVM_App.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private UserAccountModel _userAccount;
        private IUserRepository _userRepository;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public UserAccountModel UserAccount
        {
            get { return _userAccount; }
            set { _userAccount = value;
            OnPropertyChanged(nameof(UserAccount)); 
            
            }
        }

        public ViewModelBase CurrentChildView {
            get { return _currentChildView; } 
            set { _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
                    } 
        }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public ICommand ShowAddTimingsCommand { get; }
        public ICommand ShowUpdateDoctorCommand { get; }
        public ICommand ShowBookings { get; }

        public ICommand ShowAddConsultCommand { get; }

        public ICommand ShowAddDoctorCommand { get; }

        public ICommand ShowHomeCommand { get; }

        //public ICommand showPieChart { get; }



        public MainViewModel() 
        { 
            _userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            ShowAddTimingsCommand = new ViewModelCommand(ExecuteAddTimingsView);
            ShowUpdateDoctorCommand = new ViewModelCommand(ExecuteUpdateDoctorView);
            ShowAddConsultCommand = new ViewModelCommand(ExecuteAddConsultView);
            ShowAddDoctorCommand = new ViewModelCommand(ExecuteAddDoctorView);
            ShowUpdateDoctorCommand = new ViewModelCommand(ExecuteUpdateDoctorView);
      
            ShowBookings = new ViewModelCommand(ExecuteShowBookings);
            ShowHomeCommand = new ViewModelCommand(ExecuteHome);
            ExecuteShowBookings(null);
            //showPieChart = new ViewModelCommand(Executechart); //executeChart

         /*   LoadCurrentUserData();*/

        }

        private void ExecuteHome(object obj)
        {
            CurrentChildView = new HomePageViewModel();
            Caption = "Home";

        }

        //private void Executechart(object obj)
        //{
        //    CurrentChildView = new chartViewModel();
        //}

        private void ExecuteShowBookings(object obj)
        {
            CurrentChildView = new ViewBookings();
            Caption = "View Bookings";
        }

        private void ExecuteAddDoctorView(object obj)
        {
            CurrentChildView = new AddDoctorViewModel();
            Caption = "Add Doctor";
        }

        private void ExecuteAddConsultView(object obj)
        {
            CurrentChildView = new AddConsultViewModel();
            Caption = "Add Department";
            Icon = IconChar.Book;
        }

        private void ExecuteUpdateDoctorView(object obj)
        {
            CurrentChildView = new UpdateDoctorViewModel();
            Caption = "Update Doctor";
            Icon = IconChar.User;
        }

        private void ExecuteAddTimingsView(object obj)
        {
            CurrentChildView = new AddTimingsViewModel();
            Caption = "Update Timings";
            Icon = IconChar.Clock;

        }

      /*  private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null) 
            {

                UserAccount.Username = user.UserName;
               UserAccount.DisplayName = $"Welcome {user.UserName};)";
                    UserAccount.Profilepic = null;       
          }
        }*/
    }
}
