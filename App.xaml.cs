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
                if (loginview.IsVisible == false && loginview.IsLoaded)
                {
                    var mainview = new userDashboard();
                    mainview.Show();
                    loginview.Close();
                }
            };
        }

    }
}
