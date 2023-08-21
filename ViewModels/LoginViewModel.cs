using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM_App.ViewModels;
using MVVM_App.Repositories;
using MVVM_App.Models;
using System.Security.Principal;

namespace MVVM_App.ViewModels
{
    public class LoginViewModel : ViewModelBase 
    {

        private string _userName;   
        private SecureString _password;
        private string _errormessage;
        private bool _isViewVisible = true;
        private IUserRepository userRepository;

        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(nameof(UserName)); }  }
        public SecureString Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string Errormessage { get => _errormessage; set { _errormessage = value; OnPropertyChanged(nameof(Errormessage)); } }
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }

        public ICommand LoginCommand { get; } 
        public ICommand RecoverPasswordCommand { get; }

        public ICommand ShowPasswordCommand { get; }

        public ICommand RememberPasswordCommand { get; }

        public LoginViewModel() 
        {
            userRepository=new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand,CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p=> ExecuteRecoverPasswordCommand("",""));
        }

        private void ExecuteRecoverPasswordCommand(string username,string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if(string.IsNullOrWhiteSpace(UserName) || UserName.Length<3 || Password == null || Password.Length<3)
            {
                validData = false;
            }
            else { 
            validData=true;
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser( new NetworkCredential(UserName,Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserName), null);
                IsViewVisible = false;   

            }
            else
            {
                Errormessage = "Invalid Username or Password";
            }
        }
    }
}
