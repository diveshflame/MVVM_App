using MVVM_App.views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVM_App.views;

namespace MVVM_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {
            var loginview = new Window1();
            loginview.Show();
            

            loginview.IsVisibleChanged += (s, ev) =>
            {
                if (loginview.IsVisible == false )
                {
                    var mainview = new Window2();
                    mainview.Show();
                    loginview.Close();
                }
             
            };


            var registerView = new RegistrationPage();
            registerView.Show();

            registerView.IsVisibleChanged += (s, ev) =>
            {
                if (registerView.IsVisible == false)
                {
                    var mainview = new Window1();
                    mainview.Show();
                    registerView.Close();
                }
            };


            var changepasswordView = new ChangePassword();
            changepasswordView.IsVisibleChanged += (s, ev) =>
            {
                if (changepasswordView.IsVisible == false)
                {
                    var mainview = new Window1();
                    mainview.Show();
                    changepasswordView.Close();
                }
            };
        }

    }
}
