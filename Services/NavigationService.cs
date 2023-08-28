using MVVM_App.Services;
using MVVM_App.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_App.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateToRegistration()
        {
            RegistrationPage registrationWindow = new RegistrationPage();
            registrationWindow.Show();
        }

        public void NavigateToLogin()
        {
            Window1 loginWindow = new Window1();
            loginWindow.Show();
        }

        public void CloseCurrentWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
        }
    }
}


//This navigation service has not been appllied yet