using MVVM_App.Models;
using MVVM_App.Repositories;
using MVVM_App.views;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {

        private UserDetails _userDetails;

        public ICommand ChangePasswordCommand { get; private set; }

        private bool _isViewVisible = true;
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }

        private ChangePassRepo _changePassRepo;

        public UserDetails UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged();
            }
        }

        public ChangePasswordViewModel()
        {
            UserDetails=new UserDetails();
            ChangePasswordCommand = new RelayCommand(Update, Canupdate);
            _changePassRepo = new ChangePassRepo();
        }
        private bool Canupdate()
        {
            return true;
        }

        private void Update()
        {
            if(Canupdate())
            {
                try
                {
                   
                    _changePassRepo.ChangeUserPass(UserDetails);
                    MessageBox.Show("Password changed successfully");

                    Window1 loginView = new Window1();
                    loginView.Show();
                           
                    IsViewVisible = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
    }

    
}
