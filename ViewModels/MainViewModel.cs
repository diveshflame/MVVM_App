using System;
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

namespace MVVM_App.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private UserAccountModel _userAccount;
        private IUserRepository _userRepository;
        private ViewModelBase _currentChildView;
        private string _caption;
     
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
        public ICommand ShowAddDoctorCommand { get; }
        public ICommand ShowUpdateDoctorCommand { get; }

        public ICommand ShowConsultType { get; }



        public MainViewModel() 
        { 
            _userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            ShowAddDoctorCommand = new ViewModelCommand(ExecuteAddDoctorView);
            ShowUpdateDoctorCommand = new ViewModelCommand(ExecuteUpdateDoctorView);
            ShowConsultType = new ViewModelCommand(ExecuteaddConsultView);

            LoadCurrentUserData();
        
        }

        private void ExecuteaddConsultView(object obj)
        {
            CurrentChildView = new AddConsult();
            Caption = "Add Consult";
        }

        private void ExecuteUpdateDoctorView(object obj)
        {
            CurrentChildView = new UpdateDoctor();
            Caption = "Update Doctor";
         
        }

        private void ExecuteAddDoctorView(object obj)
        {
            CurrentChildView = new AddDoctor();
            Caption = "Add Doctor";
         
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null) 
            {

                UserAccount.Username = user.UserName;
                UserAccount.DisplayName = $"Welcome {user.UserName};)";
                     UserAccount.Profilepic = null;
                
            
            
            }
           

        }
    }
}
