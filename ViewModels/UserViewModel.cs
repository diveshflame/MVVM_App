using MVVM_App.Models;
using MVVM_App.Repositories;
using MVVM_App.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class UserViewModel:ViewModelBase
    {
        private UserAccountModel _userAccount;
        private IUserRepository _userRepository;
        private ViewModelBase _currentChildView;
        private string _caption;

        public UserAccountModel UserAccount
        {
            get { return _userAccount; }
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));

            }
        }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }

        public ICommand ShowUserBookings { get; }

       


        public UserViewModel()
        {
            _userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            ShowUserBookings = new ViewModelCommand(ExecuteShowUserBookings);
           // LoadCurrentUserData();

        }

        private void ExecuteShowUserBookings(object obj)
        {
            CurrentChildView = new UserViewBooking();
            Caption = "View Bookings";
        }

        //private void LoadCurrentUserData()
        //{
        //    var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        //    if (user != null)
        //    {

        //        UserAccount.Username = user.UserName;
        //        UserAccount.DisplayName = $"Welcome {user.UserName};)";
        //        UserAccount.Profilepic = null;
        //    }


        //}

    }
}
